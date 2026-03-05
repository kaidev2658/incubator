# API Summary: Tizen.UI.Widget

Source: `/var/folders/hz/_qhnnrpx00nbls9m60h83lym0000gp/T/dotnet-assembly-inspector/5944a7cb787d4f88bbc77ee4f36e6fad/lib/net8.0-tizen10.0/Tizen.UI.Widget.dll`
Generated (UTC): 2026-03-04T14:04:50.4084750+00:00

## Namespace `Tizen.UI.Widget`

### class `DaliNativeWidget`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(String[] args)`
  - `protected Void Dispose(Boolean disposing)`
  - `public Void Exit()`
  - `public Action<IntPtr> get_AppControlHandler()`
  - `public Action<Int32> get_BatteryLowHandler()`
  - `public Action get_CreatedHandler()`
  - `public static DaliNativeWidget get_Current()`
  - `public Action<Int32> get_DeviceOrientationChangedHandler()`
  - `public Action<String> get_LanguageChangedHandler()`
  - `public Action<Int32> get_MemoryLowHandler()`
  - `public Action get_PausedHandler()`
  - `public Action<String> get_RegionChangedHandler()`
  - `public Action get_ResumedHandler()`
  - `public Action get_TerminatedHandler()`
  - `public IntPtr GetWindowHandle()`
  - `private Void OnAppControl(IntPtr data, IntPtr appcontrol)`
  - `private Void OnBatteryLow(Int32 batteryStatus)`
  - `private Void OnCreated(IntPtr data)`
  - `private Void OnDeviceOrientationChanged(Int32 degree)`
  - `private Void OnLanguageChanged(IntPtr data)`
  - `private Void OnMemoryLow(Int32 memoryStatus)`
  - `private Void OnPaused(IntPtr data)`
  - `private Void OnRegionChanged(IntPtr data)`
  - `private Void OnResumed(IntPtr data)`
  - `private Void OnTerminated(IntPtr data)`
  - `private static IntPtr OnWidgetCreateFunction(IntPtr& widgetPtr)`
  - `public Void RegisterWidgetCreatingFunction()`
  - `internal Void RegisterWidgetCreatingFunction(String widgetName)`
  - `public Void RegisterWidgetInfo(Dictionary<Type, String> widgetInfo)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void RunLoop()`
  - `public Void set_AppControlHandler(Action<IntPtr> value)`
  - `public Void set_BatteryLowHandler(Action<Int32> value)`
  - `public Void set_CreatedHandler(Action value)`
  - `private static Void set_Current(DaliNativeWidget value)`
  - `public Void set_DeviceOrientationChangedHandler(Action<Int32> value)`
  - `public Void set_LanguageChangedHandler(Action<String> value)`
  - `public Void set_MemoryLowHandler(Action<Int32> value)`
  - `public Void set_PausedHandler(Action value)`
  - `public Void set_RegionChangedHandler(Action<String> value)`
  - `public Void set_ResumedHandler(Action value)`
  - `public Void set_TerminatedHandler(Action value)`
  - `public Action<IntPtr> AppControlHandler { get; set; }`
  - `public Action<Int32> BatteryLowHandler { get; set; }`
  - `public Action CreatedHandler { get; set; }`
  - `public DaliNativeWidget Current { get; set; }`
  - `public Action<Int32> DeviceOrientationChangedHandler { get; set; }`
  - `public Action<String> LanguageChangedHandler { get; set; }`
  - `public Action<Int32> MemoryLowHandler { get; set; }`
  - `public Action PausedHandler { get; set; }`
  - `public Action<String> RegionChangedHandler { get; set; }`
  - `public Action ResumedHandler { get; set; }`
  - `public Action TerminatedHandler { get; set; }`

### class `Interop`

- Base: `System.Object`
- Members:

### class `MockUIApplication`

- Base: `Tizen.UI.UIApplication`
- Members:
  - `public .ctor(CoreApplication app)`
  - `private Void HandleLocalChanged(Object sender, LocaleChangedEventArgs e)`
  - `private Void HandleRegionFormatChanged(Object sender, RegionFormatChangedEventArgs e)`

### class `UIWidgetCoreBackend`

- Base: `System.Object`
- Interfaces: `System.IDisposable`, `Tizen.Applications.CoreBackend.ICoreBackend`
- Members:
  - `public .ctor()`
  - `public Void AddEventHandler(EventType evType, Action handler)`
  - `public Void AddEventHandler(EventType evType, Action<TEventArgs> handler)`
  - `public Void Dispose()`
  - `public Void Exit()`
  - `private Void OnAppControl(IntPtr appcontrol)`
  - `private Void OnBatteryLow(Int32 status)`
  - `private Void OnCreated()`
  - `private Void OnDeviceOrientationChanged(Int32 degree)`
  - `private Void OnLanguageChanged(String language)`
  - `private Void OnMemoryLow(Int32 status)`
  - `private Void OnPaused()`
  - `private Void OnRegionChanged(String region)`
  - `private Void OnResumed()`
  - `private Void OnTerminated()`
  - `public Void RegisterWidgetInfo(Dictionary<Type, String> widgetInfo)`
  - `public Void Run(String[] args)`

### class `Widget`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor()`
  - `private .ctor(IntPtr widgetPtr)`
  - `private .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private Void ConnectDirector()`
  - `private Void DisconnectDirector()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected Void OnCreate(String contentInfo, Window window)`
  - `private Void OnDirectorCreated(String contentInfo, IntPtr window)`
  - `private Void OnDirectorPause()`
  - `private Void OnDirectorResize(IntPtr window)`
  - `private Void OnDirectorResume()`
  - `private Void OnDirectorSignalConnected(IntPtr slotObserver, IntPtr callback)`
  - `private Void OnDirectorSignalDisconnected(IntPtr slotObserver, IntPtr callback)`
  - `private Void OnDirectorTerminate(String contentInfo, Boolean suspend)`
  - `private Void OnDirectorUpdate(String contentInfo, Boolean force)`
  - `protected Void OnPause()`
  - `protected Void OnResize(Window window)`
  - `protected Void OnResume()`
  - `protected Void OnTerminate(String contentInfo, Boolean suspend)`
  - `protected Void OnUpdate(String contentInfo, Boolean force)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### class `WidgetApplication`

- Base: `Tizen.Applications.CoreApplication`
- Members:
  - `public .ctor(Dictionary<Type, String> widgetTypes)`
  - `public static WidgetApplication get_Current()`
  - `protected Void OnPreCreate()`
  - `public Void Run(String[] args)`
  - `private static Void set_Current(WidgetApplication value)`
  - `public WidgetApplication Current { get; set; }`

