# API Summary: Tizen.UI.Tools

Source: `/Users/clawdev/workspace/github/incubator/artifacts/dotnet-assembly-inspector/input/extracted/tizen-ui/Tizen.UI.Tools.1.0.0-rc.4/lib/net8.0-tizen10.0/Tizen.UI.Tools.dll`
Generated (UTC): 2026-03-04T11:15:19.9655160+00:00

## Namespace `Tizen.UI.Tools`

### class `DumpViewTree`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public static String Dump()`
  - `public static Void Dump(Window window)`
  - `public static Void Dump(Layer view, Window parent)`
  - `public static Void Dump(View view, NObject parent)`
  - `public static String DumpInstance()`
  - `private static View GetBody(ContentView view)`
  - `private static ConcurrentDictionary<UInt32, WeakReference<View>> GetInstanceTable()`
  - `public static String ToHex(Color color)`

### class `Inspector`

- Base: `System.Object`
- Members:
  - `public static Void Start(Int32 port)`
  - `public static Void Stop()`

### class `WebServer`

- Base: `System.Object`
- Members:
  - `public .ctor(Int32 port)`
  - `private String GetMimeType(String filename)`
  - `private Void Process(HttpListenerContext context)`
  - `public Void Start()`
  - `public Void Stop()`

