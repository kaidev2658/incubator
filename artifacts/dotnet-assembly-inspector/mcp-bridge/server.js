#!/usr/bin/env node

const fs = require("node:fs/promises");
const os = require("node:os");
const path = require("node:path");
const { spawn } = require("node:child_process");

const TOOL_DEFINITIONS = [
  {
    name: "inspect_assembly",
    description: "Inspect one .dll and return apiIndex + apiSummaryMarkdown.",
    inputSchema: {
      type: "object",
      properties: {
        assemblyPath: { type: "string" },
        dependencySearchPaths: {
          type: "array",
          items: { type: "string" }
        }
      },
      required: ["assemblyPath"],
      additionalProperties: false
    }
  },
  {
    name: "inspect_nuget_package",
    description: "Inspect one .nupkg and return per-assembly API results by TFM.",
    inputSchema: {
      type: "object",
      properties: {
        nupkgPath: { type: "string" },
        tfm: { type: "string" },
        allTfms: { type: "boolean" }
      },
      required: ["nupkgPath"],
      additionalProperties: false
    }
  },
  {
    name: "find_extension_methods",
    description: "Find extension methods from a .dll by contains filters.",
    inputSchema: {
      type: "object",
      properties: {
        assemblyPath: { type: "string" },
        targetTypeContains: { type: "string" },
        declaringNamespaceContains: { type: "string" },
        methodNameContains: { type: "string" },
        dependencySearchPaths: {
          type: "array",
          items: { type: "string" }
        }
      },
      required: ["assemblyPath"],
      additionalProperties: false
    }
  }
];

const args = parseArgs(process.argv.slice(2));
const dotnetBin = args.dotnet || process.env.DOTNET_BIN || "/usr/local/share/dotnet/dotnet";
const projectPath = path.resolve(args.project || "src/AssemblyInspector.Cli");
const configuration = args.configuration || "Release";
const includeNoBuild = !args["no-no-build"];

let inboundBuffer = Buffer.alloc(0);
let processing = Promise.resolve();

process.stdin.on("data", (chunk) => {
  inboundBuffer = Buffer.concat([inboundBuffer, chunk]);
  processFrames();
});

process.stdin.on("end", () => {
  process.exit(0);
});

function parseArgs(argv) {
  const parsed = {};
  for (let i = 0; i < argv.length; i += 1) {
    const token = argv[i];
    if (!token.startsWith("--")) {
      continue;
    }

    const key = token.slice(2);
    const next = argv[i + 1];
    if (!next || next.startsWith("--")) {
      parsed[key] = true;
      continue;
    }

    parsed[key] = next;
    i += 1;
  }
  return parsed;
}

function processFrames() {
  while (true) {
    const headerEnd = inboundBuffer.indexOf("\r\n\r\n");
    if (headerEnd === -1) {
      return;
    }

    const headerText = inboundBuffer.slice(0, headerEnd).toString("utf8");
    const headers = parseHeaders(headerText);
    const contentLength = Number.parseInt(headers["content-length"] || "", 10);
    if (!Number.isFinite(contentLength) || contentLength < 0) {
      inboundBuffer = Buffer.alloc(0);
      return;
    }

    const totalLength = headerEnd + 4 + contentLength;
    if (inboundBuffer.length < totalLength) {
      return;
    }

    const body = inboundBuffer.slice(headerEnd + 4, totalLength).toString("utf8");
    inboundBuffer = inboundBuffer.slice(totalLength);

    let message;
    try {
      message = JSON.parse(body);
    } catch {
      continue;
    }

    processing = processing
      .then(() => handleMessage(message))
      .catch((error) => {
        process.stderr.write(`[mcp-bridge] unexpected error: ${error.stack || error}\n`);
      });
  }
}

function parseHeaders(headerText) {
  const result = {};
  const lines = headerText.split("\r\n");
  for (const line of lines) {
    const index = line.indexOf(":");
    if (index === -1) {
      continue;
    }

    const key = line.slice(0, index).trim().toLowerCase();
    const value = line.slice(index + 1).trim();
    result[key] = value;
  }

  return result;
}

async function handleMessage(message) {
  if (!message || typeof message !== "object") {
    return;
  }

  if (typeof message.method !== "string") {
    return;
  }

  const hasId = Object.prototype.hasOwnProperty.call(message, "id");
  if (!hasId) {
    return;
  }

  try {
    if (message.method === "initialize") {
      writeMessage({
        jsonrpc: "2.0",
        id: message.id,
        result: {
          protocolVersion: "2024-11-05",
          capabilities: {
            tools: {}
          },
          serverInfo: {
            name: "dotnet-assembly-inspector-bridge",
            version: "0.1.0"
          }
        }
      });
      return;
    }

    if (message.method === "tools/list") {
      writeMessage({
        jsonrpc: "2.0",
        id: message.id,
        result: { tools: TOOL_DEFINITIONS }
      });
      return;
    }

    if (message.method === "tools/call") {
      const toolName = message.params?.name;
      const toolArgs = message.params?.arguments || {};
      if (!TOOL_DEFINITIONS.find((tool) => tool.name === toolName)) {
        writeMessage({
          jsonrpc: "2.0",
          id: message.id,
          error: {
            code: -32602,
            message: `Unsupported tool: ${String(toolName)}`
          }
        });
        return;
      }

      const cliResponse = await executeCliTool(toolName, toolArgs);
      writeMessage({
        jsonrpc: "2.0",
        id: message.id,
        result: {
          structuredContent: cliResponse,
          content: [
            {
              type: "text",
              text: JSON.stringify(cliResponse, null, 2)
            }
          ]
        }
      });
      return;
    }

    if (message.method === "ping") {
      writeMessage({
        jsonrpc: "2.0",
        id: message.id,
        result: {}
      });
      return;
    }

    writeMessage({
      jsonrpc: "2.0",
      id: message.id,
      error: {
        code: -32601,
        message: `Method not found: ${message.method}`
      }
    });
  } catch (error) {
    writeMessage({
      jsonrpc: "2.0",
      id: message.id,
      error: {
        code: -32000,
        message: error instanceof Error ? error.message : String(error)
      }
    });
  }
}

function writeMessage(message) {
  const payload = JSON.stringify(message);
  const header = `Content-Length: ${Buffer.byteLength(payload, "utf8")}\r\n\r\n`;
  process.stdout.write(header + payload);
}

async function executeCliTool(toolName, requestObject) {
  const tempDir = await fs.mkdtemp(path.join(os.tmpdir(), "assembly-inspector-mcp-"));
  const requestPath = path.join(tempDir, "request.json");

  try {
    await fs.writeFile(requestPath, JSON.stringify(requestObject, null, 2), "utf8");

    const commandArgs = [
      "run",
      "--project",
      projectPath,
      "-c",
      configuration
    ];
    if (includeNoBuild) {
      commandArgs.push("--no-build");
    }

    commandArgs.push(
      "--",
      "--mcp-tool",
      toolName,
      "--request",
      requestPath
    );

    const execution = await spawnAndCapture(dotnetBin, commandArgs, {
      cwd: process.cwd(),
      env: {
        ...process.env,
        DOTNET_ROLL_FORWARD: process.env.DOTNET_ROLL_FORWARD || "Major"
      }
    });

    const stdoutText = execution.stdout.trim();
    if (!stdoutText) {
      throw new Error(`CLI returned empty response for tool ${toolName}. stderr: ${execution.stderr.trim()}`);
    }

    try {
      return JSON.parse(stdoutText);
    } catch {
      throw new Error(
        `Failed to parse CLI JSON response for ${toolName}. stdout: ${stdoutText}\nstderr: ${execution.stderr.trim()}`
      );
    }
  } finally {
    await fs.rm(tempDir, { recursive: true, force: true });
  }
}

function spawnAndCapture(command, commandArgs, options) {
  return new Promise((resolve, reject) => {
    const child = spawn(command, commandArgs, options);
    let stdout = "";
    let stderr = "";

    child.stdout.on("data", (chunk) => {
      stdout += chunk.toString("utf8");
    });
    child.stderr.on("data", (chunk) => {
      stderr += chunk.toString("utf8");
    });
    child.on("error", (error) => {
      reject(error);
    });
    child.on("close", (code) => {
      if (code === 0) {
        resolve({ stdout, stderr });
        return;
      }

      reject(
        new Error(
          [
            `Command failed: ${command} ${commandArgs.join(" ")}`,
            `Exit code: ${code}`,
            `stdout: ${stdout.trim()}`,
            `stderr: ${stderr.trim()}`
          ].join("\n")
        )
      );
    });
  });
}
