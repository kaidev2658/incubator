# API Summary: Tizen.UI.WindowBorder

Source: `/var/folders/hz/_qhnnrpx00nbls9m60h83lym0000gp/T/dotnet-assembly-inspector/84085e148f1b4e62b16880dd98bed7cc/lib/net8.0-tizen10.0/Tizen.UI.WindowBorder.dll`
Generated (UTC): 2026-03-04T12:10:11.1742220+00:00

## Namespace `Tizen.UI.WindowBorder`

### class `BorderView`

- Base: `Tizen.UI.ContentView`
- Interfaces: `Tizen.UI.WindowBorder.IWindowBorderProvider`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public .ctor(ViewTemplate header)`
  - `public .ctor(ViewTemplate header, ViewTemplate footer)`
  - `private Void <CreateViewModel>b__56_0(WindowResizeDirection direction)`
  - `private Void <CreateViewModel>b__56_1(Boolean maximize)`
  - `private Void <CreateViewModel>b__56_2()`
  - `private Void <CreateViewModel>b__56_3()`
  - `private Void <CreateViewModel>b__56_5()`
  - `private Void <CreateViewModel>b__56_7()`
  - `private Color <OnTouchEvent>b__52_0(Task t)`
  - `private BorderViewModel CreateViewModel()`
  - `private WindowResizeDirection DetectResizeArea(Point point)`
  - `private Void EnterOverlayBorder()`
  - `private Void ExitOverlayBorder()`
  - `public Color get_BorderActiveColor()`
  - `public Thickness get_BorderArea()`
  - `public Color get_BorderColor()`
  - `public Single get_DefaultBorderWidth()`
  - `private Color get_EffectiveBorderColor()`
  - `public Boolean get_EnableOverlayBorder()`
  - `public ViewTemplate get_FooterTemplate()`
  - `public ViewTemplate get_HeaderTemplate()`
  - `protected Window get_TargetWindow()`
  - `protected BorderViewModel get_ViewModel()`
  - `public Void HideOverlayBorder()`
  - `protected Void Initialize()`
  - `protected Void OnTargetWindowResized()`
  - `private Void OnTouchEvent(Object sender, TouchEventArgs e)`
  - `private Void OnWindowResized(Object sender, EventArgs e)`
  - `public Void set_BorderActiveColor(Color value)`
  - `public Void set_BorderArea(Thickness value)`
  - `public Void set_BorderColor(Color value)`
  - `public Void set_DefaultBorderWidth(Single value)`
  - `public Void set_EnableOverlayBorder(Boolean value)`
  - `public Void ShowOverlayBorder()`
  - `private View Tizen.UI.WindowBorder.IWindowBorderProvider.get_BorderView()`
  - `private Void Tizen.UI.WindowBorder.IWindowBorderProvider.SetTargetWindow(Window window)`
  - `private Void UpdateBorderWidth()`
  - `public Color BorderActiveColor { get; set; }`
  - `public Thickness BorderArea { get; set; }`
  - `public Color BorderColor { get; set; }`
  - `public Single DefaultBorderWidth { get; set; }`
  - `private Color EffectiveBorderColor { get; }`
  - `public Boolean EnableOverlayBorder { get; set; }`
  - `public ViewTemplate FooterTemplate { get; }`
  - `public ViewTemplate HeaderTemplate { get; }`
  - `protected Window TargetWindow { get; }`
  - `private View Tizen.UI.WindowBorder.IWindowBorderProvider.BorderView { get; }`
  - `protected BorderViewModel ViewModel { get; }`

### interface `IWindowBorderProvider`

- Members:
  - `public Thickness get_BorderArea()`
  - `public View get_BorderView()`
  - `public Void SetTargetWindow(Window window)`
  - `public Thickness BorderArea { get; }`
  - `public View BorderView { get; }`

### class `WindowExtensions`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Void ClearBorderView(Window window)`
  - `public static Layer GetBorderLayer(Window window)`
  - `public static Void SetBorderView(Window window, IWindowBorderProvider provider)`

