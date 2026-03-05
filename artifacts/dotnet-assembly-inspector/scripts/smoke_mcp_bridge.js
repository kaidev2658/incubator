#!/usr/bin/env node

const path = require("node:path");
const { spawn } = require("node:child_process");

const repoRoot = path.resolve(__dirname, "..");
const bridgePath = path.join(repoRoot, "mcp-bridge", "server.js");
const sampleAssemblyPath = process.argv[2] || path.join(repoRoot, "input", "AssemblyInspector.Cli.dll");

let outboundId = 1;
const pending = new Map();
let inboundBuffer = Buffer.alloc(0);

const child = spawn("node", [bridgePath], {
  cwd: repoRoot,
  stdio: ["pipe", "pipe", "pipe"],
  env: {
    ...process.env,
    DOTNET_ROLL_FORWARD: process.env.DOTNET_ROLL_FORWARD || "Major"
  }
});

child.stdout.on("data", (chunk) => {
  inboundBuffer = Buffer.concat([inboundBuffer, chunk]);
  processFrames();
});

child.stderr.on("data", (chunk) => {
  process.stderr.write(`[bridge-stderr] ${chunk.toString("utf8")}`);
});

child.on("exit", (code, signal) => {
  if (pending.size > 0) {
    for (const [, reject] of pending.values()) {
      reject(new Error(`Bridge exited before response. code=${code} signal=${signal}`));
    }
    pending.clear();
  }
});

function processFrames() {
  while (true) {
    const headerEnd = inboundBuffer.indexOf("\r\n\r\n");
    if (headerEnd === -1) {
      return;
    }

    const headerText = inboundBuffer.slice(0, headerEnd).toString("utf8");
    const contentLengthLine = headerText
      .split("\r\n")
      .find((line) => line.toLowerCase().startsWith("content-length:"));
    if (!contentLengthLine) {
      inboundBuffer = Buffer.alloc(0);
      return;
    }

    const contentLength = Number.parseInt(contentLengthLine.split(":")[1].trim(), 10);
    const totalLength = headerEnd + 4 + contentLength;
    if (inboundBuffer.length < totalLength) {
      return;
    }

    const payload = inboundBuffer.slice(headerEnd + 4, totalLength).toString("utf8");
    inboundBuffer = inboundBuffer.slice(totalLength);

    const message = JSON.parse(payload);
    if (!Object.prototype.hasOwnProperty.call(message, "id")) {
      continue;
    }

    const callback = pending.get(message.id);
    if (!callback) {
      continue;
    }

    pending.delete(message.id);
    const [resolve, reject] = callback;
    if (message.error) {
      reject(new Error(`MCP error: ${message.error.message}`));
      continue;
    }

    resolve(message.result);
  }
}

function sendRequest(method, params) {
  return new Promise((resolve, reject) => {
    const id = outboundId;
    outboundId += 1;
    pending.set(id, [resolve, reject]);

    const body = JSON.stringify({
      jsonrpc: "2.0",
      id,
      method,
      params
    });
    const header = `Content-Length: ${Buffer.byteLength(body, "utf8")}\r\n\r\n`;
    child.stdin.write(header + body);
  });
}

async function main() {
  const initializeResult = await sendRequest("initialize", {
    protocolVersion: "2024-11-05",
    capabilities: {},
    clientInfo: {
      name: "smoke-test",
      version: "0.1.0"
    }
  });

  if (!initializeResult?.serverInfo?.name) {
    throw new Error("initialize response missing serverInfo.name");
  }

  const toolsResult = await sendRequest("tools/list", {});
  const toolNames = (toolsResult.tools || []).map((tool) => tool.name);
  if (!toolNames.includes("inspect_assembly")) {
    throw new Error("tools/list response does not include inspect_assembly");
  }

  const callResult = await sendRequest("tools/call", {
    name: "inspect_assembly",
    arguments: {
      assemblyPath: sampleAssemblyPath
    }
  });

  const structured = callResult?.structuredContent || {};
  const apiIndex = structured.apiIndex || structured.ApiIndex || {};
  const assemblyName = apiIndex.assemblyName || apiIndex.AssemblyName;
  if (!assemblyName) {
    throw new Error("tools/call response missing structuredContent.apiIndex.assemblyName");
  }

  process.stdout.write(
    [
      "MCP bridge smoke test passed.",
      `server=${initializeResult.serverInfo.name}`,
      `toolCount=${toolNames.length}`,
      `assembly=${assemblyName}`
    ].join("\n") + "\n"
  );
}

main()
  .then(() => {
    child.kill("SIGTERM");
    process.exit(0);
  })
  .catch((error) => {
    child.kill("SIGTERM");
    process.stderr.write(`Smoke test failed: ${error.message}\n`);
    process.exit(1);
  });
