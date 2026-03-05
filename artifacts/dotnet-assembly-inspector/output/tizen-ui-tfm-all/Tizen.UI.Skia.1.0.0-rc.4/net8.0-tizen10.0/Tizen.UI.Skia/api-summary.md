# API Summary: Tizen.UI.Skia

Source: `/var/folders/hz/_qhnnrpx00nbls9m60h83lym0000gp/T/dotnet-assembly-inspector/f7668b26c954400dbc9d76979771b25d/lib/net8.0-tizen10.0/Tizen.UI.Skia.dll`
Generated (UTC): 2026-03-04T14:04:50.3479180+00:00

## Namespace `Tizen.UI.Skia`

### class `CustomRenderingView`

- Base: `Tizen.UI.ImageView`
- Members:
  - `protected .ctor()`
  - `public event EventHandler<SKPaintSurfaceEventArgs> PaintSurface`
  - `private Void <Invalidate>b__7_0()`
  - `public Void add_PaintSurface(EventHandler<SKPaintSurfaceEventArgs> value)`
  - `public Void Invalidate()`
  - `protected Void OnDrawFrame()`
  - `private Void OnRelayout(Object sender, EventArgs e)`
  - `protected Void OnResized()`
  - `public Void remove_PaintSurface(EventHandler<SKPaintSurfaceEventArgs> value)`
  - `protected Void SendPaintSurface(SKPaintSurfaceEventArgs e)`

### class `SKCanvasView`

- Base: `Tizen.UI.Skia.CustomRenderingView`
- Members:
  - `public .ctor()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Boolean get_IgnorePixelScaling()`
  - `protected Void OnDrawFrame()`
  - `protected Void OnResized()`
  - `protected Void SendMeasureInvalidatedIfNeed()`
  - `public Void set_IgnorePixelScaling(Boolean value)`
  - `public Boolean IgnorePixelScaling { get; set; }`

### class `SKPaintSurfaceEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor(SKSurface surface, SKImageInfo info)`
  - `public .ctor(SKSurface surface, SKImageInfo info, SKImageInfo rawInfo)`
  - `public SKImageInfo get_Info()`
  - `public SKImageInfo get_RawInfo()`
  - `public SKSurface get_Surface()`
  - `public SKImageInfo Info { get; }`
  - `public SKImageInfo RawInfo { get; }`
  - `public SKSurface Surface { get; }`

