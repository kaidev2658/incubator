# API Summary: Tizen.UI.Primitives2D

Source: `/var/folders/hz/_qhnnrpx00nbls9m60h83lym0000gp/T/dotnet-assembly-inspector/ab0df32562b848859432f41142b2667c/lib/net8.0-tizen10.0/Tizen.UI.Primitives2D.dll`
Generated (UTC): 2026-03-04T12:10:11.0704150+00:00

## Namespace `Tizen.UI.Primitives2D`

### class `ArcTo`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor(Single x, Single y, Single radius, Single startAngle, Single sweep, Boolean closed)`
  - `public Boolean get_Closed()`
  - `public Single get_Radius()`
  - `public Single get_StartAngle()`
  - `public Single get_Sweep()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`
  - `public Boolean Closed { get; }`
  - `public Single Radius { get; }`
  - `public Single StartAngle { get; }`
  - `public Single Sweep { get; }`
  - `public Single X { get; }`
  - `public Single Y { get; }`

### class `BezierTo`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor(Single endPointX, Single endPointY, Single controlPoint1X, Single controlPoint1Y, Single controlPoint2X, Single controlPoint2Y)`
  - `public Single get_ControlPoint1X()`
  - `public Single get_ControlPoint1Y()`
  - `public Single get_ControlPoint2X()`
  - `public Single get_ControlPoint2Y()`
  - `public Single get_EndPointX()`
  - `public Single get_EndPointY()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`
  - `public Single ControlPoint1X { get; }`
  - `public Single ControlPoint1Y { get; }`
  - `public Single ControlPoint2X { get; }`
  - `public Single ControlPoint2Y { get; }`
  - `public Single EndPointX { get; }`
  - `public Single EndPointY { get; }`

### class `Close`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`

### class `CustomShape`

- Base: `Tizen.UI.Primitives2D.Shape`
- Members:
  - `public .ctor(IEnumerable<IPath> path)`
  - `protected Void Dispose(Boolean disposing)`
  - `public IEnumerable<IPath> get_Path()`
  - `private Void ResetPath()`
  - `public Void set_Path(IEnumerable<IPath> value)`
  - `private Void SetPath(IEnumerable<IPath> path)`
  - `public IEnumerable<IPath> Path { get; set; }`

### class `Drawable`

- Base: `Tizen.UI.NObject`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void ClipPath(Drawable clip)`
  - `public Rect get_BoundingBox()`
  - `public Void Rotate(Single degree)`
  - `public Void Scale(Single factor)`
  - `public Void Transform(Single[] matrix)`
  - `public Void Translate(Single x, Single y)`
  - `public Rect BoundingBox { get; }`

### class `DrawableGroup`

- Base: `Tizen.UI.Primitives2D.Drawable`
- Members:
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void Add(Drawable drawable)`
  - `public Void Clear()`
  - `public Boolean Contains(Drawable drawable)`
  - `protected Void Dispose(Boolean disposing)`
  - `public IReadOnlyCollection<Drawable> get_Drawables()`
  - `public Void Remove(Drawable drawable)`
  - `public IReadOnlyCollection<Drawable> Drawables { get; }`

### class `DrawCircle`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor(Single centerX, Single centerY, Single radius)`
  - `public Single get_CenterX()`
  - `public Single get_CenterY()`
  - `public Single get_Radius()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`
  - `public Single CenterX { get; }`
  - `public Single CenterY { get; }`
  - `public Single Radius { get; }`

### class `DrawOval`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor(Single centerX, Single centerY, Single radiusX, Single radiusY)`
  - `public Single get_CenterX()`
  - `public Single get_CenterY()`
  - `public Single get_RadiusX()`
  - `public Single get_RadiusY()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`
  - `public Single CenterX { get; }`
  - `public Single CenterY { get; }`
  - `public Single RadiusX { get; }`
  - `public Single RadiusY { get; }`

### class `DrawRect`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor(Single x, Single y, Single width, Single height)`
  - `public Single get_Height()`
  - `public Single get_Width()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`
  - `public Single Height { get; }`
  - `public Single Width { get; }`
  - `public Single X { get; }`
  - `public Single Y { get; }`

### class `DrawRoundRect`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor(Single x, Single y, Single width, Single height, Single radiusX, Single radiusY)`
  - `public Single get_Height()`
  - `public Single get_RadiusX()`
  - `public Single get_RadiusY()`
  - `public Single get_Width()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`
  - `public Single Height { get; }`
  - `public Single RadiusX { get; }`
  - `public Single RadiusY { get; }`
  - `public Single Width { get; }`
  - `public Single X { get; }`
  - `public Single Y { get; }`

### class `Ellipse`

- Base: `Tizen.UI.Primitives2D.Shape`
- Members:
  - `public .ctor(Single centerX, Single centerY, Single radiusX, Single radiusY)`
  - `private Void DrawEllipse(Boolean reset)`
  - `public Single get_CenterX()`
  - `public Single get_CenterY()`
  - `public Single get_RadiusX()`
  - `public Single get_RadiusY()`
  - `public Void set_CenterX(Single value)`
  - `public Void set_CenterY(Single value)`
  - `public Void set_RadiusX(Single value)`
  - `public Void set_RadiusY(Single value)`
  - `public Single CenterX { get; set; }`
  - `public Single CenterY { get; set; }`
  - `public Single RadiusX { get; set; }`
  - `public Single RadiusY { get; set; }`

### enum `FillRule`

- Base: `System.Enum`
- Members:

### class `GradientPaint`

- Base: `Tizen.UI.NObject`
- Interfaces: `Tizen.UI.Primitives2D.IPaint`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void Fill(Shape shape)`
  - `public FillRule get_FillRule()`
  - `public GradientStop[] get_GradientStops()`
  - `public SpreadType get_Spread()`
  - `public StrokeCap get_StrokeCap()`
  - `public Single[] get_StrokeDashArray()`
  - `public StrokeJoin get_StrokeJoin()`
  - `public Single get_StrokeWidth()`
  - `public Void set_FillRule(FillRule value)`
  - `public Void set_GradientStops(GradientStop[] value)`
  - `public Void set_Spread(SpreadType value)`
  - `public Void set_StrokeCap(StrokeCap value)`
  - `public Void set_StrokeDashArray(Single[] value)`
  - `public Void set_StrokeJoin(StrokeJoin value)`
  - `public Void set_StrokeWidth(Single value)`
  - `public Void Stroke(Shape shape)`
  - `public FillRule FillRule { get; set; }`
  - `public GradientStop[] GradientStops { get; set; }`
  - `public SpreadType Spread { get; set; }`
  - `public StrokeCap StrokeCap { get; set; }`
  - `public Single[] StrokeDashArray { get; set; }`
  - `public StrokeJoin StrokeJoin { get; set; }`
  - `public Single StrokeWidth { get; set; }`

### class `ImageDrawable`

- Base: `Tizen.UI.Primitives2D.Drawable`
- Members:
  - `public .ctor(String url, Single width, Single height)`
  - `public Single get_Height()`
  - `public String get_Url()`
  - `public Single get_Width()`
  - `private Void LoadImage(String url)`
  - `public Void set_Height(Single value)`
  - `public Void set_Url(String value)`
  - `public Void set_Width(Single value)`
  - `private Void SetSize(Single width, Single height)`
  - `public Single Height { get; set; }`
  - `public String Url { get; set; }`
  - `public Single Width { get; set; }`

### class `Interop`

- Base: `System.Object`
- Members:

### interface `IPaint`

- Members:
  - `public Void Fill(Shape shape)`
  - `public FillRule get_FillRule()`
  - `public StrokeCap get_StrokeCap()`
  - `public Single[] get_StrokeDashArray()`
  - `public StrokeJoin get_StrokeJoin()`
  - `public Single get_StrokeWidth()`
  - `public Void set_FillRule(FillRule value)`
  - `public Void set_StrokeCap(StrokeCap value)`
  - `public Void set_StrokeDashArray(Single[] value)`
  - `public Void set_StrokeJoin(StrokeJoin value)`
  - `public Void set_StrokeWidth(Single value)`
  - `public Void Stroke(Shape shape)`
  - `public FillRule FillRule { get; set; }`
  - `public StrokeCap StrokeCap { get; set; }`
  - `public Single[] StrokeDashArray { get; set; }`
  - `public StrokeJoin StrokeJoin { get; set; }`
  - `public Single StrokeWidth { get; set; }`

### interface `IPath`

- Members:
  - `public Void Execute(Shape shape)`

### class `LinearGradientPaint`

- Base: `Tizen.UI.Primitives2D.GradientPaint`
- Members:
  - `public .ctor()`
  - `public .ctor(Single startPointX, Single startPointY, Single endPointX, Single endPointY)`
  - `public Point get_EndPoint()`
  - `public Point get_StartPoint()`
  - `public Void set_EndPoint(Point value)`
  - `public Void set_StartPoint(Point value)`
  - `private Void SetBounds(Point startPoint, Point endPoint)`
  - `public Point EndPoint { get; set; }`
  - `public Point StartPoint { get; set; }`

### class `LineTo`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor(Single x, Single y)`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`
  - `public Single X { get; }`
  - `public Single Y { get; }`

### class `MoveTo`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPath`
- Members:
  - `public .ctor(Single x, Single y)`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `private Void Tizen.UI.Primitives2D.IPath.Execute(Shape shape)`
  - `public Single X { get; }`
  - `public Single Y { get; }`

### class `Path`

- Base: `System.Object`
- Members:
  - `public static IPath ArcTo(Single x, Single y, Single radius, Single startAngle, Single sweep, Boolean closed)`
  - `public static IPath BezierTo(Single endPointX, Single endPointY, Single controlPoint1X, Single controlPoint1Y, Single controlPoint2X, Single controlPoint2Y)`
  - `public static IPath Close()`
  - `public static IPath DrawCircle(Single centerX, Single centerY, Single radius)`
  - `public static IPath DrawOval(Single centerX, Single centerY, Single radiusX, Single radiusY)`
  - `public static IPath DrawRoundRect(Single x, Single y, Single width, Single height, Single radiusX, Single radiusY)`
  - `public static IPath LineTo(Single x, Single y)`
  - `public static IPath MoveTo(Single x, Single y)`

### class `RadialGradientPaint`

- Base: `Tizen.UI.Primitives2D.GradientPaint`
- Members:
  - `public .ctor()`
  - `public .ctor(Single centerX, Single centerY, Single radius)`
  - `public Point get_Center()`
  - `public Single get_Radius()`
  - `public Void set_Center(Point value)`
  - `public Void set_Radius(Single value)`
  - `private Void SetBounds(Point center, Single radius)`
  - `public Point Center { get; set; }`
  - `public Single Radius { get; set; }`

### class `Rectangle`

- Base: `Tizen.UI.Primitives2D.Shape`
- Members:
  - `public .ctor(Single x, Single y, Single width, Single height)`
  - `private Void DrawRectangle(Boolean reset)`
  - `public Single get_Height()`
  - `public Single get_Width()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Void set_Height(Single value)`
  - `public Void set_Width(Single value)`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public Single Height { get; set; }`
  - `public Single Width { get; set; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`

### class `RoundRectangle`

- Base: `Tizen.UI.Primitives2D.Shape`
- Members:
  - `public .ctor(Single x, Single y, Single width, Single height, Single radiusX, Single radiusY)`
  - `private Void DrawRoundRectangle(Boolean reset)`
  - `public Single get_Height()`
  - `public Single get_RadiusX()`
  - `public Single get_RadiusY()`
  - `public Single get_Width()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Void set_Height(Single value)`
  - `public Void set_RadiusX(Single value)`
  - `public Void set_RadiusY(Single value)`
  - `public Void set_Width(Single value)`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public Single Height { get; set; }`
  - `public Single RadiusX { get; set; }`
  - `public Single RadiusY { get; set; }`
  - `public Single Width { get; set; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`

### class `Shape`

- Base: `Tizen.UI.Primitives2D.Drawable`
- Members:
  - `protected .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void Fill(IPaint paint)`
  - `public Void Stroke(IPaint paint)`

### class `SolidPaint`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Primitives2D.IPaint`
- Members:
  - `public .ctor()`
  - `public .ctor(Color color)`
  - `public Void Fill(Shape shape)`
  - `public Color get_Color()`
  - `public FillRule get_FillRule()`
  - `public StrokeCap get_StrokeCap()`
  - `public Single[] get_StrokeDashArray()`
  - `public StrokeJoin get_StrokeJoin()`
  - `public Single get_StrokeWidth()`
  - `public Void set_Color(Color value)`
  - `public Void set_FillRule(FillRule value)`
  - `public Void set_StrokeCap(StrokeCap value)`
  - `public Void set_StrokeDashArray(Single[] value)`
  - `public Void set_StrokeJoin(StrokeJoin value)`
  - `public Void set_StrokeWidth(Single value)`
  - `public Void Stroke(Shape shape)`
  - `public Color Color { get; set; }`
  - `public FillRule FillRule { get; set; }`
  - `public StrokeCap StrokeCap { get; set; }`
  - `public Single[] StrokeDashArray { get; set; }`
  - `public StrokeJoin StrokeJoin { get; set; }`
  - `public Single StrokeWidth { get; set; }`

### enum `SpreadType`

- Base: `System.Enum`
- Members:

### enum `StrokeCap`

- Base: `System.Enum`
- Members:

### enum `StrokeJoin`

- Base: `System.Enum`
- Members:

### class `VectorView`

- Base: `Tizen.UI.View`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void Add(Drawable drawable)`
  - `public Void Clear()`
  - `public Boolean Contains(Drawable drawable)`
  - `protected Void Dispose(Boolean disposing)`
  - `public IReadOnlyCollection<Drawable> get_Drawables()`
  - `private Void OnRelayout(Object sender, EventArgs e)`
  - `public Void Remove(Drawable drawable)`
  - `public IReadOnlyCollection<Drawable> Drawables { get; }`

