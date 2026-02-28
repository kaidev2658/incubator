using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Renderer;
using TizenA2uiRenderer.Runtime;
using TizenA2uiRenderer.Transport;

var transport = new TransportAdapter();
var controller = new SurfaceController();
IRendererBridge renderer = new NullRendererBridge();
var runtimeAdapter = new RendererBridgeRuntimeAdapter(renderer);

using var pipeline = new A2uiRuntimePipeline(transport, controller, runtimeAdapter);

Console.WriteLine("Tizen A2UI Renderer skeleton initialized.");
pipeline.AddMessage(new NormalMessage("v0.10", NormalMessageType.CreateSurface, "main"));
