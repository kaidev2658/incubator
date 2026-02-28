using TizenA2uiRenderer.Controller;
using TizenA2uiRenderer.Model;
using TizenA2uiRenderer.Renderer;
using TizenA2uiRenderer.Transport;

var transport = new TransportAdapter();
var controller = new SurfaceController();
IRendererBridge renderer = new NullRendererBridge();

controller.SurfaceUpdated += update => renderer.Render(update.SurfaceId, update.Definition, update.DataModel);
controller.SurfaceDeleted += surfaceId => renderer.Remove(surfaceId);
transport.OnMessage(controller.HandleMessage);

Console.WriteLine("Tizen A2UI Renderer skeleton initialized.");
transport.AddMessage(new NormalMessage("v0.10", NormalMessageType.CreateSurface, "main"));
