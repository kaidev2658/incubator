# API Summary: Tizen.UI

Source: `/var/folders/hz/_qhnnrpx00nbls9m60h83lym0000gp/T/dotnet-assembly-inspector/66c078ad88be4b2fb3fc43feb40e5bca/lib/net8.0-tizen10.0/Tizen.UI.dll`
Generated (UTC): 2026-03-04T14:04:47.0314180+00:00

## Namespace `Tizen.UI`

### enum `AccessibilityAction`

- Base: `System.Enum`
- Members:

### struct `AccessibilityActionInfo`

- Base: `System.ValueType`
- Members:

### class `AccessibilityActionReceivedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public AccessibilityAction get_ActionType()`
  - `public Boolean get_Handled()`
  - `public View get_Target()`
  - `public Void set_ActionType(AccessibilityAction value)`
  - `public Void set_Handled(Boolean value)`
  - `public Void set_Target(View value)`
  - `public AccessibilityAction ActionType { get; set; }`
  - `public Boolean Handled { get; set; }`
  - `public View Target { get; set; }`

### class `AccessibilityData`

- Base: `System.Object`
- Members:
  - `public .ctor(View owner)`
  - `public Void AddActionReceivedHandler(EventHandler<AccessibilityActionReceivedEventArgs> value)`
  - `public Void AddDescriptionRequestedHandler(EventHandler<AccessibilityDescriptionRequestedEventArgs> value)`
  - `public Void AddHighlightChangedHandler(EventHandler<AccessibilityHighlightChangedEventArgs> value)`
  - `public Void AddNameRequestedHandler(EventHandler<AccessibilityNameRequestedEventArgs> value)`
  - `private Boolean OnAccessibilityActionReceived(IntPtr data)`
  - `private Void OnAccessibilityHighlighted(Boolean data)`
  - `private Void OnDescriptionRequested(IntPtr data)`
  - `private Void OnNameRequested(IntPtr data)`
  - `public Void RemoveActionReceivedHandler(EventHandler<AccessibilityActionReceivedEventArgs> value)`
  - `public Void RemoveDescriptionRequestedHandler(EventHandler<AccessibilityDescriptionRequestedEventArgs> value)`
  - `public Void RemoveHighlightChangedHandler(EventHandler<AccessibilityHighlightChangedEventArgs> value)`
  - `public Void RemoveNameRequestedHandler(EventHandler<AccessibilityNameRequestedEventArgs> value)`

### class `AccessibilityDescriptionRequestedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public String get_Description()`
  - `public Void set_Description(String value)`
  - `public String Description { get; set; }`

### class `AccessibilityExtensions`

- Base: `System.Object`
- Members:
  - `public static TView AccessibilityDescription(TView view, String description)`
  - `public static TView AccessibilityHidden(TView view, Boolean isHidden)`
  - `public static TView AccessibilityHighlightable(TView view, Boolean isHighlightable)`
  - `public static TView AccessibilityName(TView view, String name)`
  - `public static TView AccessibilityRole(TView view, AccessibilityRole role)`
  - `public static TView AutomationId(TView view, String id)`
  - `public static Boolean GetAccessibilityIsModal(View view)`
  - `public static Boolean GetAccessibilityIsScrollable(View view)`
  - `public static AccessibilityStates GetAccessibilityStates(View view)`
  - `public static String GetAccessibilityValue(View view)`
  - `public static Void SetAccessibilityAttributes(View view, IDictionary<String, String> attributes)`
  - `public static Void SetAccessibilityIsModal(View view, Boolean modal)`
  - `public static Void SetAccessibilityIsScrollable(View view, Boolean scrollable)`
  - `public static Void SetAccessibilityStates(View view, AccessibilityStates states)`
  - `public static Void SetAccessibilityValue(View view, String value)`
  - `public static Void UpdateAccessibilityStates(View view, AccessibilityState state, Boolean beAdded)`

### class `AccessibilityHighlightChangedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsHighlighted()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_IsHighlighted(Boolean value)`
  - `public Boolean IsHighlighted { get; set; }`

### class `AccessibilityManager`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public event EventHandler AccessibilityStateChanged`
  - `public event EventHandler ScreenReaderStateChanged`
  - `public static Void add_AccessibilityStateChanged(EventHandler value)`
  - `public static Void add_ScreenReaderStateChanged(EventHandler value)`
  - `public static Task<AnnouncementState> AnnounceAsync(String sentence, Boolean discardable)`
  - `public static Void CancelAnnouncement()`
  - `public static Boolean get_IsAccessibilityEnabled()`
  - `public static Boolean get_IsScreenReaderEnabled()`
  - `public static View GetHighlighted()`
  - `public static Void remove_AccessibilityStateChanged(EventHandler value)`
  - `public static Void remove_ScreenReaderStateChanged(EventHandler value)`
  - `public static Void SendChangedNameEvent(View view)`
  - `public static Void SendShowingEvent(View view, Boolean isShowing)`
  - `public static Boolean SetHighlight(View view)`
  - `public static Void SetHighlightIndicator(View indicator)`
  - `public static Void SetReadingInfo(View view, AccessibilityReadingInfo readingInfo)`
  - `public Boolean IsAccessibilityEnabled { get; }`
  - `public Boolean IsScreenReaderEnabled { get; }`

### class `AccessibilityNameRequestedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public String get_Name()`
  - `public Void set_Name(String value)`
  - `public String Name { get; set; }`

### enum `AccessibilityReadingInfo`

- Base: `System.Enum`
- Members:

### enum `AccessibilityRelation`

- Base: `System.Enum`
- Members:

### enum `AccessibilityRole`

- Base: `System.Enum`
- Members:

### enum `AccessibilityState`

- Base: `System.Enum`
- Members:

### struct `AccessibilityStates`

- Base: `System.ValueType`
- Members:
  - `private .ctor(Int32 states)`
  - `public Boolean get_Item(AccessibilityState state)`
  - `public static Int32 op_Explicit(AccessibilityStates state)`
  - `public static AccessibilityStates op_Explicit(Int32 states)`
  - `public Void set_Item(AccessibilityState state, Boolean value)`
  - `public Boolean Item { get; set; }`

### enum `ActionButtonTitle`

- Base: `System.Enum`
- Members:

### class `AlphaFunction`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `private static .cctor()`
  - `public .ctor(Func<Single, Single> userAlpha)`
  - `public .ctor(BuiltinAlphaFunctions builtin)`
  - `public .ctor(Point point1, Point point2)`
  - `private .ctor(UserAlphaFunction userAlpha)`
  - `private .ctor(IntPtr handle)`
  - `private static IntPtr CreateHandle(Point point1, Point point2)`
  - `public Void Dispose()`
  - `private Void Dispose(Boolean disposing)`
  - `protected Void Finalize()`
  - `public IntPtr get_Handle()`
  - `internal Boolean get_IsUserAlphaFunction()`
  - `private static UserAlphaFunction GetAlphaFunction(Func<Single, Single> userAlpha)`
  - `public IntPtr Handle { get; }`
  - `internal Boolean IsUserAlphaFunction { get; }`

### class `AnchorClickedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public String get_Href()`
  - `public Void set_Href(String value)`
  - `public String Href { get; set; }`

### class `AnimatablePropertyValue`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `protected .ctor()`
  - `public static AnimatablePropertyValue CreateBackgroundColorValue(Color backgroundColor)`
  - `public static AnimatablePropertyValue CreateCornerRadiusValue(CornerRadius cornerRadius)`
  - `public static AnimatablePropertyValue CreateCustomValue(String propertyName, PropertyValueHandle value)`
  - `public static AnimatablePropertyValue CreateCustomValue(String propertyName, Object value)`
  - `public static AnimatablePropertyValue CreateOpacityValue(Single opacity)`
  - `public static AnimatablePropertyValue CreatePositionValue(Point position)`
  - `public static AnimatablePropertyValue CreatePositionValue(Single x, Single y)`
  - `protected static AnimatablePropertyHandle CreatePropertyHandle(View view, String propertyName)`
  - `public static AnimatablePropertyValue CreateRotationValue(Single rotationX, Single rotationY, Single rotationZ)`
  - `public static AnimatablePropertyValue CreateScaleValue(Size scale)`
  - `public static AnimatablePropertyValue CreateScaleValue(Single scaleX, Single scaleY)`
  - `public static AnimatablePropertyValue CreateSizeValue(Size size)`
  - `public static AnimatablePropertyValue CreateSizeValue(Single width, Single height)`
  - `protected static AnimatablePropertyHandle CreateVisualPropertyHandle(View view, Int32 propertyIndex, Int32 visualIndex)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `public PropertyValueHandle get_Value()`
  - `public IEnumerable<ValueTuple<AnimatablePropertyHandle, PropertyValueHandle>> GetTargetProperties(View view)`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `public PropertyValueHandle Value { get; }`

### class `Animation`

- Base: `Tizen.UI.NObject`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private event EventHandler _finished`
  - `private event EventHandler _progressReached`
  - `public event EventHandler Finished`
  - `public event EventHandler ProgressReached`
  - `private Void <OnFinished>b__44_0()`
  - `private Void add__finished(EventHandler value)`
  - `private Void add__progressReached(EventHandler value)`
  - `public Void add_Finished(EventHandler value)`
  - `public Void add_ProgressReached(EventHandler value)`
  - `private static Void AddToPlayingObject(Animation animation)`
  - `public Void AnimateBy(View target, AnimatablePropertyValue props, Int32 delayMs, Int32 durationMs, AlphaFunction alpha)`
  - `public Void AnimateTo(View target, AnimatablePropertyValue props, Int32 delayMs, Int32 durationMs, AlphaFunction alpha)`
  - `public Void Clear()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Single get_CurrentProgress()`
  - `public Int32 get_Duration()`
  - `public AnimationEndAction get_EndAction()`
  - `public Boolean get_IsLoop()`
  - `public Single get_ProgressReachedTarget()`
  - `public AnimationState get_State()`
  - `private Void OnFinished(Object sender, EventArgs e)`
  - `private Void OnFinished(IntPtr ptr)`
  - `private Void OnProgress(IntPtr ptr)`
  - `public Void Pause()`
  - `public Void Play()`
  - `public Void Play(Int32 delayTime)`
  - `public Void Play(Single progress)`
  - `public Task PlayAsync()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void remove__finished(EventHandler value)`
  - `private Void remove__progressReached(EventHandler value)`
  - `public Void remove_Finished(EventHandler value)`
  - `public Void remove_ProgressReached(EventHandler value)`
  - `private static Void RemoveFromPlayingObject(Animation animation)`
  - `public Void set_Duration(Int32 value)`
  - `public Void set_EndAction(AnimationEndAction value)`
  - `public Void set_IsLoop(Boolean value)`
  - `public Void set_ProgressReachedTarget(Single value)`
  - `public Void Stop()`
  - `public Single CurrentProgress { get; }`
  - `public Int32 Duration { get; set; }`
  - `public AnimationEndAction EndAction { get; set; }`
  - `public Boolean IsLoop { get; set; }`
  - `public Single ProgressReachedTarget { get; set; }`
  - `public AnimationState State { get; }`

### enum `AnimationEndAction`

- Base: `System.Enum`
- Members:

### class `AnimationExtensions`

- Base: `System.Object`
- Members:
  - `public static Task AnimateTo(View view, AnimatablePropertyValue value, Int32 duration)`
  - `public static Task MoveTo(View view, Single x, Single y, Int32 duration)`
  - `public static Task RotateTo(View view, Single rotate, Int32 duration)`

### enum `AnimationRepeatMode`

- Base: `System.Enum`
- Members:

### enum `AnimationState`

- Base: `System.Enum`
- Members:

### enum `AnimationStopBehavior`

- Base: `System.Enum`
- Members:

### enum `AnnouncementState`

- Base: `System.Enum`
- Members:

### enum `AutoCapital`

- Base: `System.Enum`
- Members:

### struct `AutoFontSize`

- Base: `System.ValueType`
- Members:
  - `public Boolean get_Enabled()`
  - `public Single get_MaximumSize()`
  - `public Single get_MinimumSize()`
  - `public Single get_StepSize()`
  - `public Void set_Enabled(Boolean value)`
  - `public Void set_MaximumSize(Single value)`
  - `public Void set_MinimumSize(Single value)`
  - `public Void set_StepSize(Single value)`
  - `public Boolean Enabled { get; set; }`
  - `public Single MaximumSize { get; set; }`
  - `public Single MinimumSize { get; set; }`
  - `public Single StepSize { get; set; }`

### class `BackdropBlurEffect`

- Base: `Tizen.UI.RenderEffect`
- Members:
  - `public .ctor(Single radius)`
  - `internal SafeHandle CreateNativeHandle()`

### class `Background`

- Base: `System.Object`
- Members:
  - `protected .ctor()`
  - `public Void BuildPropertyMap(PropertyMapHandle map)`

### class `BackgroundPropertyValue`

- Base: `Tizen.UI.AnimatablePropertyValue`
- Members:
  - `public .ctor(Color color)`
  - `public PropertyValueHandle get_Value()`
  - `public IEnumerable<ValueTuple<AnimatablePropertyHandle, PropertyValueHandle>> GetTargetProperties(View view)`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `public PropertyValueHandle Value { get; }`

### class `BackgroundTokenPropertyChangedEventArgs`

- Base: `Tizen.UI.TokenPropertyChangedEventArgs`
- Members:
  - `public .ctor()`
  - `public static BackgroundTokenPropertyChangedEventArgs Create(View view, String name, Background background)`
  - `public Background get_Background()`
  - `public Void set_Background(Background value)`
  - `public Background Background { get; set; }`

### class `BindingExtensions`

- Base: `System.Object`
- Members:
  - `public static TView Bind(TView view, BindingProperty<TView, TProperty> property, String path)`
  - `public static TView BindBackgroundColor(TView view, String path)`
  - `public static TText BindFontSize(TText view, String path)`
  - `public static TView BindHeight(TView view, String path)`
  - `public static BindingSession<TViewModel> BindingSession(View view)`
  - `public static T BindingSession(T view, BindingSession<TViewModel> session)`
  - `public static T BindLocalizedResource(T view, Action<T, String> setter, String path)`
  - `public static TText BindLocalizedText(TText view, String key, ResourceManager resourceManager)`
  - `public static T BindLocalizedText(T view, Action<T, String> setter, String key, ResourceManager resourceManager)`
  - `public static TImageView BindMultipliedColor(TImageView imageView, String path)`
  - `public static TImageView BindResourceUrl(TImageView imageView, String path)`
  - `public static TText BindText(TText view, String path)`
  - `public static TText BindTextColor(TText view, String path)`
  - `public static TText BindTextTwoWay(TText view, String path)`
  - `public static TView BindWidth(TView view, String path)`
  - `public static TView BindX(TView view, String path)`
  - `public static TView BindY(TView view, String path)`
  - `public static TView SetBinding(TView view, BindingSession<T> vm, Action<T, TView> set, String path)`
  - `public static View SetBinding(View view, BindingSession<T> vm, Action<T> set, String path)`
  - `public static View SetBinding(View view, BindingSession<TViewModel> session, String targetPath, String srcPath)`
  - `public static TView SetBinding(TView view, BindingSession<TViewModel> session, BindingProperty<TView, TProperty> property, String path)`
  - `public static View SetTwoWayBinding(View view, BindingSession<TViewModel> session, TwoWayBindingProperty<View, TProperty> property, String path)`
  - `public static TView SetTwoWayBinding(TView view, BindingSession<TViewModel> session, TwoWayBindingProperty<TView, TProperty> property, String path)`
  - `public static TView TwoWayBind(TView view, TwoWayBindingProperty<TView, TProperty> property, String path)`
  - `public static TView ViewModel(TView view, Object viewModel)`

### class `BindingProperty`2`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Action<TView, TValue> get_Setter()`
  - `public Void set_Setter(Action<TView, TValue> value)`
  - `public Action<TView, TValue> Setter { get; set; }`

### class `BindingSession`1`

- Base: `System.Object`
- Interfaces: `System.ComponentModel.INotifyPropertyChanged`, `System.IDisposable`
- Members:
  - `public .ctor()`
  - `public event PropertyChangedEventHandler PropertyChanged`
  - `public Void add_PropertyChanged(PropertyChangedEventHandler value)`
  - `public Void AddBinding(Action<TViewModel> setter, String path)`
  - `public Void AddTwoWayBinding(Action<Action> register, Action<Action> unregister, Action<TViewModel> setter, Func<T> getter, String path)`
  - `public Void ClearBinding()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Void Dispose()`
  - `public TViewModel get_ViewModel()`
  - `public Object GetValue(String name)`
  - `private Void OnPropertyChanged(Object sender, PropertyChangedEventArgs e)`
  - `public Void remove_PropertyChanged(PropertyChangedEventHandler value)`
  - `public Void set_ViewModel(TViewModel value)`
  - `public Void SetValue(Object obj, String name)`
  - `public Void UpdateViewModel(TViewModel vm)`
  - `public Void UpdateViewModel()`
  - `public TViewModel ViewModel { get; set; }`

### enum `BrokenImageType`

- Base: `System.Enum`
- Members:

### enum `BuiltinAlphaFunctions`

- Base: `System.Enum`
- Members:

### enum `ClippingMode`

- Base: `System.Enum`
- Members:

### struct `Color`

- Base: `System.ValueType`
- Interfaces: `System.IEquatable`1<Tizen.UI.Color>`, `Tizen.UI.IToken`
- Members:
  - `private static .cctor()`
  - `public .ctor(Single r, Single g, Single b, Single a)`
  - `private .ctor(Single w, Single x, Single y, Single z, Mode mode)`
  - `private .ctor(Int32 r, Int32 g, Int32 b)`
  - `private .ctor(Int32 r, Int32 g, Int32 b, Int32 a)`
  - `private .ctor(String id, Single alpha)`
  - `public .ctor(Single r, Single g, Single b)`
  - `public .ctor(Single value)`
  - `public .ctor(UInt32 value, Single alpha)`
  - `public Color AddLuminosity(Single delta)`
  - `internal Color CalculateTokenAlpha(Color color)`
  - `private static Void ConvertToHsl(Single r, Single g, Single b, Mode mode, Single& h, Single& s, Single& l)`
  - `private static Void ConvertToRgb(Single hue, Single saturation, Single luminosity, Mode mode, Single& r, Single& g, Single& b)`
  - `public Boolean Equals(Object obj)`
  - `public Boolean Equals(Color color)`
  - `private static Boolean EqualsInner(Color color1, Color color2)`
  - `public static Color FromHex(String hex)`
  - `public static Color FromHsla(Single h, Single s, Single l, Single a)`
  - `public static Color FromHsv(Single h, Single s, Single v)`
  - `public static Color FromHsv(Int32 h, Int32 s, Int32 v)`
  - `public static Color FromHsva(Single h, Single s, Single v, Single a)`
  - `public static Color FromHsva(Int32 h, Int32 s, Int32 v, Int32 a)`
  - `public static Color FromRgb(Int32 r, Int32 g, Int32 b)`
  - `public static Color FromRgb(Single r, Single g, Single b)`
  - `public static Color FromRgba(Int32 r, Int32 g, Int32 b, Int32 a)`
  - `public static Color FromRgba(Single r, Single g, Single b, Single a)`
  - `public static Color FromUint(UInt32 argb)`
  - `public Single get_A()`
  - `public Single get_B()`
  - `public static Color get_Default()`
  - `public Single get_G()`
  - `public Single get_Hue()`
  - `public String get_Id()`
  - `public Boolean get_IsDefault()`
  - `public Boolean get_IsToken()`
  - `public Single get_Luminosity()`
  - `public Single get_R()`
  - `public Single get_Saturation()`
  - `public Int32 GetHashCode()`
  - `public Color MultiplyAlpha(Single alpha)`
  - `public static Boolean op_Equality(Color color1, Color color2)`
  - `public static Boolean op_Inequality(Color color1, Color color2)`
  - `public String ToHex()`
  - `private static UInt32 ToHex(Char c)`
  - `private static UInt32 ToHexD(Char c)`
  - `public static Color Token(String id)`
  - `public String ToString()`
  - `public Color WithAlpha(Single alpha)`
  - `public Color WithHue(Single hue)`
  - `public Color WithLuminosity(Single luminosity)`
  - `public Color WithSaturation(Single saturation)`
  - `public Single A { get; }`
  - `public Single B { get; }`
  - `public Color Default { get; }`
  - `public Single G { get; }`
  - `public Single Hue { get; }`
  - `public String Id { get; }`
  - `public Boolean IsDefault { get; }`
  - `public Boolean IsToken { get; }`
  - `public Single Luminosity { get; }`
  - `public Single R { get; }`
  - `public Single Saturation { get; }`

### class `ColorBackground`

- Base: `Tizen.UI.Background`
- Members:
  - `public .ctor()`
  - `public Void BuildPropertyMap(PropertyMapHandle map)`
  - `public Color get_Color()`
  - `public Void set_Color(Color value)`
  - `public Color Color { get; set; }`

### class `ColorTokenPropertyChangedEventArgs`

- Base: `Tizen.UI.TokenPropertyChangedEventArgs`
- Members:
  - `public .ctor()`
  - `public static ColorTokenPropertyChangedEventArgs Create(View view, String name, Color color)`
  - `public Color get_Color()`
  - `public Void set_Color(Color value)`
  - `public Color Color { get; set; }`

### class `ContentView`

- Base: `Tizen.UI.View`
- Interfaces: `Tizen.UI.IParentObject`
- Members:
  - `public .ctor()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected View get_Body()`
  - `private Boolean get_IsInvalidatedByBody()`
  - `public Size Measure(Single availableWidth, Single availableHeight)`
  - `private Void OnContentMeasureInvalidated(Object sender, EventArgs e)`
  - `protected Void OnEnabled(Boolean enabled)`
  - `protected Void OnMeasureInvalidatedOverride()`
  - `protected Void set_Body(View value)`
  - `private Void Tizen.UI.IParentObject.Remove(View view)`
  - `protected View Body { get; set; }`
  - `private Boolean IsInvalidatedByBody { get; }`

### struct `CornerRadius`

- Base: `System.ValueType`
- Members:
  - `private static .cctor()`
  - `public .ctor(Single uniformRadius)`
  - `public .ctor(Single topLeft, Single topRight, Single bottomLeft, Single bottomRight)`
  - `public Void Deconstruct(Single& topLeft, Single& topRight, Single& bottomLeft, Single& bottomRight)`
  - `private Boolean Equals(CornerRadius other)`
  - `public Boolean Equals(Object obj)`
  - `public Single get_BottomLeft()`
  - `public Single get_BottomRight()`
  - `public Boolean get_IsNaN()`
  - `public Boolean get_IsRelative()`
  - `public Boolean get_IsZero()`
  - `public Single get_TopLeft()`
  - `public Single get_TopRight()`
  - `public Int32 GetHashCode()`
  - `public static Boolean op_Equality(CornerRadius left, CornerRadius right)`
  - `public static CornerRadius op_Implicit(Single uniformRadius)`
  - `public static Boolean op_Inequality(CornerRadius left, CornerRadius right)`
  - `public String ToString()`
  - `public Single BottomLeft { get; }`
  - `public Single BottomRight { get; }`
  - `public Boolean IsNaN { get; }`
  - `public Boolean IsRelative { get; }`
  - `public Boolean IsZero { get; }`
  - `public Single TopLeft { get; }`
  - `public Single TopRight { get; }`

### class `CornerRadiusPropertyValue`

- Base: `Tizen.UI.AnimatablePropertyValue`
- Members:
  - `public .ctor(CornerRadius cornerRadius)`
  - `public CornerRadius get_CornerRadius()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `public CornerRadius CornerRadius { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `CustomPropertyValue`

- Base: `Tizen.UI.AnimatablePropertyValue`
- Members:
  - `public .ctor(String propertyName, PropertyValueHandle value)`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `private Void UpdateCompatibleValueType(View view, AnimatablePropertyHandle propertyHandle)`
  - `public PropertyValueHandle Value { get; }`

### class `DisplayMetrics`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public event EventHandler ScaleUpdated`
  - `public static Void add_ScaleUpdated(EventHandler value)`
  - `public static Int32 FromScaled(Single scaled)`
  - `public static Int32 FromScaled(Int32 scaled)`
  - `public static Int32 get_DPI()`
  - `public static Int32 get_PPU()`
  - `public static Single get_Scale()`
  - `public static Int32 get_ScreenHeight()`
  - `public static Int32 get_ScreenWidth()`
  - `public static Int32 PointToPixel(Single point)`
  - `public static Int32 PointToPixel(Int32 point)`
  - `public static Void remove_ScaleUpdated(EventHandler value)`
  - `private static Void ScaleFactorUpdated()`
  - `public static Void set_PPU(Int32 value)`
  - `public static Void set_Scale(Single value)`
  - `public static Void SetDensityBasedScaleFactor(Single userScale, Int32 baseDPI)`
  - `public static Void SetScaleFactors(Int32 ppu, Single scale)`
  - `public static Int32 ToDp(Single pixel)`
  - `public static Int32 ToDp(Int32 pixel)`
  - `public static Size ToDp(Size size)`
  - `public static Point ToDp(Point point)`
  - `public static Int32 ToPixel(Single fromUnit)`
  - `public static Int32 ToPixel(Int32 fromUnit)`
  - `public static Size ToPixel(Size size)`
  - `public static Point ToPixel(Point point)`
  - `public static Single ToPoint(Single pixel)`
  - `public static Int32 ToScaled(Single pixel)`
  - `public static Int32 ToScaled(Int32 pixel)`
  - `public Int32 DPI { get; }`
  - `public Int32 PPU { get; set; }`
  - `public Single Scale { get; set; }`
  - `public Int32 ScreenHeight { get; }`
  - `public Int32 ScreenWidth { get; }`

### class `DpExtensions`

- Base: `System.Object`
- Members:
  - `public static UnitValue Dp(Single value)`
  - `public static UnitValue Dp(Int32 value)`
  - `public static UnitValue Pt(Single value)`
  - `public static UnitValue Px(Int32 value)`
  - `public static UnitValue Spx(Int32 value)`
  - `public static UnitValue Spx(Single value)`

### class `DragEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public .ctor(Point touchPosition, Point displacement)`
  - `public Point get_Displacement()`
  - `public Point get_TouchPosition()`
  - `public Void set_Displacement(Point value)`
  - `public Void set_TouchPosition(Point value)`
  - `public Point Displacement { get; set; }`
  - `public Point TouchPosition { get; set; }`

### class `EditableTextBindings`1`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static TwoWayBindingProperty<T, String> get_TextProperty()`
  - `public TwoWayBindingProperty<T, String> TextProperty { get; }`

### class `EnumExtensions`

- Base: `System.Object`
- Members:
  - `public static T GetValueByDescription(String description)`
  - `public static TextAlignment HorizontalNameToEnum(String name)`
  - `public static TextAlignment VerticalNameToEnum(String name)`

### class `EventHandlerHelper`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static TEventHandler Get(Action action)`
  - `public static TEventHandler Set(TEventHandler handler, Action action)`

### class `ExceptionHelper`

- Base: `System.Object`
- Members:
  - `public static Void ThrowIfNullOrEmpty(String argument, String paramName)`
  - `public static Void ThrowIfNullOrWhiteSpace(String argument, String paramName)`

### enum `FittingMode`

- Base: `System.Enum`
- Members:

### class `FocusChangedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public View get_FocusedView()`
  - `public View get_PreviousFocusedView()`
  - `public Void set_FocusedView(View value)`
  - `public Void set_PreviousFocusedView(View value)`
  - `public View FocusedView { get; set; }`
  - `public View PreviousFocusedView { get; set; }`

### enum `FocusDirection`

- Base: `System.Enum`
- Members:

### class `FocusManager`

- Base: `Tizen.UI.NObject`
- Members:
  - `private static .cctor()`
  - `protected .ctor()`
  - `private event EventHandler<FocusChangedEventArgs> _focusChanged`
  - `public event EventHandler<FocusChangedEventArgs> FocusChanged`
  - `private Void add__focusChanged(EventHandler<FocusChangedEventArgs> value)`
  - `public Void add_FocusChanged(EventHandler<FocusChangedEventArgs> value)`
  - `public Void ClearFocus()`
  - `public Void EnableDefaultFocusAlgorithm(Boolean enable)`
  - `public Void EnableFocusIndicator(Boolean enable)`
  - `public static FocusManager get_Instance()`
  - `public View GetFocused()`
  - `public View GetNearestFocusableView(View rootView, View currentView, FocusDirection direction)`
  - `public View GetNearestFocusableViewOverride(View rootView, View currentView, FocusDirection direction)`
  - `private Void OnFocusChanged(IntPtr prev, IntPtr next)`
  - `private Void OnFocusChanged(Object sender, FocusChangedEventArgs e)`
  - `private Void remove__focusChanged(EventHandler<FocusChangedEventArgs> value)`
  - `public Void remove_FocusChanged(EventHandler<FocusChangedEventArgs> value)`
  - `public Void SetCustomFocusAlgorithm(IFocusAlgorithm focusAlgorithm)`
  - `public Boolean SetFocus(View view)`
  - `public Void SetFocusIndicator(View indicator)`
  - `public FocusManager Instance { get; }`

### class `FontClient`

- Base: `Tizen.UI.NObject`
- Members:
  - `private static .cctor()`
  - `private .ctor()`
  - `public Boolean AddCustomFontDirectory(String path)`
  - `public static Void FontPreLoad(IEnumerable<String> fontPathList, IEnumerable<String> memoryFontPathList, Boolean useThread, Boolean syncCreation)`
  - `public static FontClient get_Instance()`
  - `public static Void PreCache(IEnumerable<String> fallbackFamilyList, IEnumerable<String> extraFamilyList, String localeFamily, Boolean useThread, Boolean syncCreation)`
  - `public FontClient Instance { get; }`

### struct `FontInfo`

- Base: `System.ValueType`
- Interfaces: `System.IEquatable`1<Tizen.UI.FontInfo>`
- Members:
  - `public Boolean Equals(Object obj)`
  - `public Boolean Equals(FontInfo other)`
  - `public String get_Family()`
  - `public String get_Path()`
  - `public FontStyle get_Style()`
  - `public Int32 GetHashCode()`
  - `public static Boolean op_Equality(FontInfo lhsFontInfo, FontInfo rhsFontInfo)`
  - `public static Boolean op_Inequality(FontInfo lhsFontInfo, FontInfo rhsFontInfo)`
  - `public Void set_Family(String value)`
  - `public Void set_Path(String value)`
  - `public Void set_Style(FontStyle value)`
  - `public String Family { get; set; }`
  - `public String Path { get; set; }`
  - `public FontStyle Style { get; set; }`

### enum `FontSlant`

- Base: `System.Enum`
- Members:

### struct `FontStyle`

- Base: `System.ValueType`
- Interfaces: `System.IEquatable`1<Tizen.UI.FontStyle>`
- Members:
  - `public Boolean Equals(Object obj)`
  - `public Boolean Equals(FontStyle other)`
  - `public FontSlant get_Slant()`
  - `public FontWeight get_Weight()`
  - `public FontWidth get_Width()`
  - `public Int32 GetHashCode()`
  - `public static Boolean op_Equality(FontStyle lhsFontStyle, FontStyle rhsFontStyle)`
  - `public static Boolean op_Inequality(FontStyle lhsFontStyle, FontStyle rhsFontStyle)`
  - `public Void set_Slant(FontSlant value)`
  - `public Void set_Weight(FontWeight value)`
  - `public Void set_Width(FontWidth value)`
  - `public FontSlant Slant { get; set; }`
  - `public FontWeight Weight { get; set; }`
  - `public FontWidth Width { get; set; }`

### enum `FontWeight`

- Base: `System.Enum`
- Members:

### enum `FontWidth`

- Base: `System.Enum`
- Members:

### class `FrameUpdateCallback`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `public .ctor()`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected Void Finalize()`
  - `public IntPtr get_Handle()`
  - `private IntPtr get_ProxyHandle()`
  - `public Color GetColor(UInt32 id)`
  - `public Point GetPosition(UInt32 id)`
  - `public Size GetScale(UInt32 id)`
  - `public Size GetSize(UInt32 id)`
  - `private Vector3Handle GetVector3Cache()`
  - `private Vector4Handle GetVector4Cache()`
  - `private Void OnDirectorOnUpdate(IntPtr proxy, Single elapsedSeconds)`
  - `protected Void OnUpdate(Single elapsedSeconds)`
  - `private Void set_ProxyHandle(IntPtr value)`
  - `public Boolean SetColor(UInt32 id, Color color)`
  - `public Boolean SetPosition(UInt32 id, Point position)`
  - `public Boolean SetScale(UInt32 id, Size scale)`
  - `public Boolean SetSize(UInt32 id, Size size)`
  - `public IntPtr Handle { get; }`
  - `private IntPtr ProxyHandle { get; set; }`

### class `GaussianBlurEffect`

- Base: `Tizen.UI.RenderEffect`
- Members:
  - `public .ctor(Single radius)`
  - `internal SafeHandle CreateNativeHandle()`

### class `Gesture`

- Base: `Tizen.UI.NObject`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public GestureSource get_Source()`
  - `public GestureSourceData get_SourceData()`
  - `public GestureState get_State()`
  - `public UInt32 get_Time()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public GestureSource Source { get; }`
  - `public GestureSourceData SourceData { get; }`
  - `public GestureState State { get; }`
  - `public UInt32 Time { get; }`

### class `GestureDetectedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `protected .ctor(View view)`
  - `public Boolean get_Handled()`
  - `public View get_View()`
  - `public Void set_Handled(Boolean value)`
  - `public Boolean Handled { get; set; }`
  - `public View View { get; }`

### class `GestureDetector`

- Base: `Tizen.UI.NObject`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void Attach(View view)`
  - `public Void CancelOtherGestureDetectors()`
  - `public Void Detach(View view)`
  - `public Void DetachAll()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Boolean HandleEvent(View target, TouchEvent touch)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### enum `GestureSource`

- Base: `System.Enum`
- Members:

### enum `GestureSourceData`

- Base: `System.Enum`
- Members:

### enum `GestureState`

- Base: `System.Enum`
- Members:

### class `GradientBackground`

- Base: `Tizen.UI.Background`
- Members:
  - `protected .ctor()`
  - `public Void BuildPropertyMap(PropertyMapHandle map)`
  - `public IList<GradientStop> get_GradientStops()`
  - `public Nullable<GradientVisualSpreadMethod> get_GradientVisualSpreadMethod()`
  - `public Void set_GradientVisualSpreadMethod(Nullable<GradientVisualSpreadMethod> value)`
  - `public IList<GradientStop> GradientStops { get; }`
  - `public Nullable<GradientVisualSpreadMethod> GradientVisualSpreadMethod { get; set; }`

### struct `GradientStop`

- Base: `System.ValueType`
- Members:
  - `public .ctor(Color color, Single offset)`
  - `public Color get_Color()`
  - `public Single get_Offset()`
  - `public Void set_Color(Color value)`
  - `public Void set_Offset(Single value)`
  - `public Color Color { get; set; }`
  - `public Single Offset { get; set; }`

### enum `GradientVisualSpreadMethod`

- Base: `System.Enum`
- Members:

### enum `HiddenInputMode`

- Base: `System.Enum`
- Members:

### struct `HiddenInputSetting`

- Base: `System.ValueType`
- Members:
  - `public Nullable<Int32> get_Count()`
  - `public Nullable<Char> get_HiddenCharacter()`
  - `public HiddenInputMode get_Mode()`
  - `public Nullable<Int32> get_ShowLastCharacterDuration()`
  - `public Void set_Count(Nullable<Int32> value)`
  - `public Void set_HiddenCharacter(Nullable<Char> value)`
  - `public Void set_Mode(HiddenInputMode value)`
  - `public Void set_ShowLastCharacterDuration(Nullable<Int32> value)`
  - `public Nullable<Int32> Count { get; set; }`
  - `public Nullable<Char> HiddenCharacter { get; set; }`
  - `public HiddenInputMode Mode { get; set; }`
  - `public Nullable<Int32> ShowLastCharacterDuration { get; set; }`

### class `Hover`

- Base: `System.Object`
- Members:
  - `internal .ctor(IntPtr handle, Int32 index)`
  - `public Int32 get_DeviceId()`
  - `public View get_HitView()`
  - `public Point get_Position()`
  - `public Point get_ScreenPosition()`
  - `public TouchState get_State()`
  - `public Int32 DeviceId { get; }`
  - `public View HitView { get; }`
  - `public Point Position { get; }`
  - `public Point ScreenPosition { get; }`
  - `public TouchState State { get; }`

### class `HoverEvent`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Int32 get_Count()`
  - `public Hover get_Item(Int32 i)`
  - `public UInt32 get_Time()`
  - `public Int32 Count { get; }`
  - `public Hover Item { get; }`
  - `public UInt32 Time { get; }`

### class `HoverEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public Boolean get_Handled()`
  - `public HoverEvent get_Hover()`
  - `public Void set_Handled(Boolean value)`
  - `public Void set_Hover(HoverEvent value)`
  - `public Boolean Handled { get; set; }`
  - `public HoverEvent Hover { get; set; }`

### class `HtmlWebViewSource`

- Base: `Tizen.UI.WebViewSource`
- Members:
  - `public .ctor()`
  - `public String get_BaseUrl()`
  - `public String get_Html()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_BaseUrl(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Html(String value)`
  - `public String BaseUrl { get; set; }`
  - `public String Html { get; set; }`

### interface `IBindableView`

- Members:
  - `public Object get_BindingContext()`
  - `public Void set_BindingContext(Object value)`
  - `public Object BindingContext { get; set; }`

### interface `IDescendantFocusObserver`

- Members:
  - `public Void OnDescendantFocused(View descendant)`

### interface `IDescendantUnfocusObserver`

- Members:
  - `public Void OnDescendantUnfocused(View descendant)`

### interface `IFocusAlgorithm`

- Members:
  - `public View GetNextFocusableView(View current, View proposed, FocusDirection direction, String deviceName)`

### interface `IFontSizeScale`

- Members:
  - `public Single get_FontSizeScale()`
  - `public Void set_FontSizeScale(Single value)`
  - `public Single FontSizeScale { get; set; }`

### interface `IImage`

- Members:
  - `public Thickness get_Border()`
  - `public Boolean get_CropToMask()`
  - `public FittingMode get_FittingMode()`
  - `public Boolean get_IsNinePatchImage()`
  - `public String get_ResourceUrl()`
  - `public Void set_Border(Thickness value)`
  - `public Void set_CropToMask(Boolean value)`
  - `public Void set_FittingMode(FittingMode value)`
  - `public Void set_IsNinePatchImage(Boolean value)`
  - `public Void set_ResourceUrl(String value)`
  - `public Thickness Border { get; set; }`
  - `public Boolean CropToMask { get; set; }`
  - `public FittingMode FittingMode { get; set; }`
  - `public Boolean IsNinePatchImage { get; set; }`
  - `public String ResourceUrl { get; set; }`

### class `ImageBackground`

- Base: `Tizen.UI.Background`
- Members:
  - `public .ctor()`
  - `public Void BuildPropertyMap(PropertyMapHandle map)`
  - `public Nullable<Rect> get_PixelArea()`
  - `public Nullable<ImageReleasePolicy> get_ReleasePolicy()`
  - `public Nullable<Boolean> get_SynchronousLoading()`
  - `public String get_Url()`
  - `public ImageWrapMode get_WrapModeU()`
  - `public ImageWrapMode get_WrapModeV()`
  - `public Void set_PixelArea(Nullable<Rect> value)`
  - `public Void set_ReleasePolicy(Nullable<ImageReleasePolicy> value)`
  - `public Void set_SynchronousLoading(Nullable<Boolean> value)`
  - `public Void set_Url(String value)`
  - `public Void set_WrapModeU(ImageWrapMode value)`
  - `public Void set_WrapModeV(ImageWrapMode value)`
  - `public Nullable<Rect> PixelArea { get; set; }`
  - `public Nullable<ImageReleasePolicy> ReleasePolicy { get; set; }`
  - `public Nullable<Boolean> SynchronousLoading { get; set; }`
  - `public String Url { get; set; }`
  - `public ImageWrapMode WrapModeU { get; set; }`
  - `public ImageWrapMode WrapModeV { get; set; }`

### class `ImageBackgroundExtensions`

- Base: `System.Object`
- Members:
  - `public static TView Url(TView view, String url)`

### class `ImageExtensions`

- Base: `System.Object`
- Members:
  - `public static TView Border(TView view, Thickness value)`
  - `public static TView CropToMask(TView view, Boolean value)`
  - `public static TView FittingMode(TView view, FittingMode value)`
  - `public static T ImagePadding(T view, Thickness padding)`
  - `public static T ImagePadding(T view, Single left, Single top, Single right, Single bottom)`
  - `public static T ImagePadding(T view, Single uniformSize)`
  - `public static TView ResourceUrl(TView view, String resourceUrl)`

### class `ImageLoader`

- Base: `System.Object`
- Members:
  - `public static Size GetOriginalImageSize(String filename, Boolean orientationCorretion)`
  - `public static PixelBuffer LoadImage(Stream stream)`
  - `public static PixelBuffer LoadImageFromFile(String url)`
  - `public static PixelBuffer LoadImageFromFile(String url, Size size, ImageLoaderFittingMode fittingMode, ImageLoaderSamplingMode samplingMode, Boolean orientationCorrection)`

### enum `ImageLoaderFittingMode`

- Base: `System.Enum`
- Members:

### enum `ImageLoaderSamplingMode`

- Base: `System.Enum`
- Members:

### enum `ImageLoadingStatus`

- Base: `System.Enum`
- Members:

### enum `ImageLoadPolicy`

- Base: `System.Enum`
- Members:

### enum `ImageMaskingMode`

- Base: `System.Enum`
- Members:

### enum `ImageReleasePolicy`

- Base: `System.Enum`
- Members:

### class `ImageUrl`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public String ToString()`

### class `ImageView`

- Base: `Tizen.UI.View`
- Interfaces: `Tizen.UI.IImage`, `Tizen.UI.Internal.IResourceReadySignalHandler`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownshandle)`
  - `private event EventHandler _resourcesLoaded`
  - `public event EventHandler ResourcesLoaded`
  - `private Void add__resourcesLoaded(EventHandler value)`
  - `public Void add_ResourcesLoaded(EventHandler value)`
  - `protected Void ClearNeedUpdateMap()`
  - `protected ImageVisualMap CreateImageVisualMap()`
  - `protected Void Dispose(Boolean disposing)`
  - `public String get_AlphaMaskUrl()`
  - `public Thickness get_Border()`
  - `public Boolean get_CropToMask()`
  - `public FittingMode get_FittingMode()`
  - `public Boolean get_ImageLoadWithViewSize()`
  - `public Color get_ImageMultipliedColor()`
  - `public Boolean get_IsNinePatchImage()`
  - `public Boolean get_IsResourceReady()`
  - `public String get_LoadingPlaceholderUrl()`
  - `public ImageLoadingStatus get_LoadingStatus()`
  - `public Boolean get_PreMultipliedAlpha()`
  - `public String get_ResourceUrl()`
  - `public Boolean get_SynchronousLoading()`
  - `protected ImageVisualMap get_VisualMap()`
  - `protected Boolean IsHandlerRegistered()`
  - `public Size Measure(Single availableWidth, Single availableHeight)`
  - `private Void OnLayoutIteration(Object sender, EventArgs e)`
  - `protected Void OnResourceLoad()`
  - `private Void OnResourceReady(IntPtr view)`
  - `private Void OnResourcesLoaded(Object sender, EventArgs e)`
  - `private Void OnUpdateRequired(Object sender, EventArgs e)`
  - `protected Void OnVisualMapUpdateRequired(Object sender, EventArgs e)`
  - `public Void Pause()`
  - `public Void Play()`
  - `public Void Reload()`
  - `private Void remove__resourcesLoaded(EventHandler value)`
  - `public Void remove_ResourcesLoaded(EventHandler value)`
  - `protected Void SendMeasureInvalidatedIfNeed()`
  - `public Void set_AlphaMaskUrl(String value)`
  - `public Void set_Border(Thickness value)`
  - `public Void set_CropToMask(Boolean value)`
  - `public Void set_FittingMode(FittingMode value)`
  - `public Void set_ImageLoadWithViewSize(Boolean value)`
  - `public Void set_ImageMultipliedColor(Color value)`
  - `public Void set_IsNinePatchImage(Boolean value)`
  - `public Void set_LoadingPlaceholderUrl(String value)`
  - `public Void set_PreMultipliedAlpha(Boolean value)`
  - `public Void set_ResourceUrl(String value)`
  - `public Void set_SynchronousLoading(Boolean value)`
  - `private Void set_VisualMap(ImageVisualMap value)`
  - `public Void SetImagePadding(Thickness thickness)`
  - `protected Void SetNeedUpdateMap()`
  - `public Void Stop()`
  - `private Void Tizen.UI.Internal.IResourceReadySignalHandler.OnResourceReadySignal(IntPtr view)`
  - `public Void Update()`
  - `protected Void UpdateBorderProperty()`
  - `protected Void UpdateCornerRadiusProperty(CornerRadius cornerRadius)`
  - `protected Void UpdateImageMap()`
  - `public String AlphaMaskUrl { get; set; }`
  - `public Thickness Border { get; set; }`
  - `public Boolean CropToMask { get; set; }`
  - `public FittingMode FittingMode { get; set; }`
  - `public Boolean ImageLoadWithViewSize { get; set; }`
  - `public Color ImageMultipliedColor { get; set; }`
  - `public Boolean IsNinePatchImage { get; set; }`
  - `public Boolean IsResourceReady { get; }`
  - `public String LoadingPlaceholderUrl { get; set; }`
  - `public ImageLoadingStatus LoadingStatus { get; }`
  - `public Boolean PreMultipliedAlpha { get; set; }`
  - `public String ResourceUrl { get; set; }`
  - `public Boolean SynchronousLoading { get; set; }`
  - `protected ImageVisualMap VisualMap { get; set; }`

### class `ImageViewBindings`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static BindingProperty<ImageView, Color> get_ImageMultipliedColorProperty()`
  - `public static BindingProperty<ImageView, String> get_ResourceUrlProperty()`
  - `public BindingProperty<ImageView, Color> ImageMultipliedColorProperty { get; }`
  - `public BindingProperty<ImageView, String> ResourceUrlProperty { get; }`

### enum `ImageWrapMode`

- Base: `System.Enum`
- Members:

### struct `InnerShadow`

- Base: `System.ValueType`
- Members:
  - `public Single get_BlurRadius()`
  - `public Color get_Color()`
  - `public Thickness get_Inset()`
  - `public Void set_BlurRadius(Single value)`
  - `public Void set_Color(Color value)`
  - `public Void set_Inset(Thickness value)`
  - `public Single BlurRadius { get; set; }`
  - `public Color Color { get; set; }`
  - `public Thickness Inset { get; set; }`

### class `InnerShadowTokenPropertyChangedEventArgs`

- Base: `Tizen.UI.ColorTokenPropertyChangedEventArgs`
- Members:
  - `public .ctor()`
  - `public static InnerShadowTokenPropertyChangedEventArgs Create(View view, String name, InnerShadow shadow)`
  - `public InnerShadow get_Shadow()`
  - `public Void set_Shadow(InnerShadow value)`
  - `public InnerShadow Shadow { get; set; }`

### class `InputMethodContext`

- Base: `Tizen.UI.NObject`
- Members:
  - `internal .ctor(IntPtr cPtr, Boolean cMemoryOwn)`
  - `private event EventHandler _resizedEventHandler`
  - `private event EventHandler _statusChangedEventHandler`
  - `public event EventHandler InputPanelStateChanged`
  - `public event EventHandler Resized`
  - `public Void Activate()`
  - `private Void add__resizedEventHandler(EventHandler value)`
  - `private Void add__statusChangedEventHandler(EventHandler value)`
  - `public Void add_InputPanelStateChanged(EventHandler value)`
  - `public Void add_Resized(EventHandler value)`
  - `public Void AutoEnableInputPanel(Boolean enabled)`
  - `public Void Deactivate()`
  - `public Rect get_InputMethodArea()`
  - `public InputPanelState get_InputPanelState()`
  - `public Boolean get_TextPrediction()`
  - `public Void HideInputPanel()`
  - `public Void NotifyTextInputMultiLine(Boolean multiLine)`
  - `private Void OnResized(Int32 resized)`
  - `private Void OnStatusChanged(Boolean statusChanged)`
  - `private Void remove__resizedEventHandler(EventHandler value)`
  - `private Void remove__statusChangedEventHandler(EventHandler value)`
  - `public Void remove_InputPanelStateChanged(EventHandler value)`
  - `public Void remove_Resized(EventHandler value)`
  - `public Void set_TextPrediction(Boolean value)`
  - `public Void SetInputPanelLanguage(InputPanelLanguage language)`
  - `public Void SetInputPanelPosition(UInt32 x, UInt32 y)`
  - `public Void SetInputPanelUserData(String text)`
  - `public Void SetReturnKeyState(Boolean visible)`
  - `public Void ShowInputPanel()`
  - `public Rect InputMethodArea { get; }`
  - `public InputPanelState InputPanelState { get; }`
  - `public Boolean TextPrediction { get; set; }`

### struct `InputMethodSetting`

- Base: `System.ValueType`
- Members:
  - `public Nullable<ActionButtonTitle> get_ActionButton()`
  - `public Nullable<AutoCapital> get_AutoCapital()`
  - `public Nullable<PanelLayout> get_PanelLayout()`
  - `public Void set_ActionButton(Nullable<ActionButtonTitle> value)`
  - `public Void set_AutoCapital(Nullable<AutoCapital> value)`
  - `public Void set_PanelLayout(Nullable<PanelLayout> value)`
  - `public Nullable<ActionButtonTitle> ActionButton { get; set; }`
  - `public Nullable<AutoCapital> AutoCapital { get; set; }`
  - `public Nullable<PanelLayout> PanelLayout { get; set; }`

### enum `InputPanelLanguage`

- Base: `System.Enum`
- Members:

### enum `InputPanelState`

- Base: `System.Enum`
- Members:

### class `InputView`

- Base: `Tizen.UI.View`
- Interfaces: `Tizen.UI.IFontSizeScale`, `Tizen.UI.IText`, `Tizen.UI.ITextAlignment`, `Tizen.UI.ITextEditable`, `Tizen.UI.ITextPadding`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public event EventHandler<AnchorClickedEventArgs> AnchorClicked`
  - `public event EventHandler MaxLengthReached`
  - `public event EventHandler TextChanged`
  - `public Void add_AnchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `public Void add_MaxLengthReached(EventHandler value)`
  - `public Void add_TextChanged(EventHandler value)`
  - `protected Void Dispose(Boolean disposing)`
  - `public Int32 get_CursorWidth()`
  - `public String get_FontFamily()`
  - `public Single get_FontSize()`
  - `public TextAlignment get_HorizontalAlignment()`
  - `public InputMethodContext get_InputMethodContext()`
  - `public Boolean get_IsMarkupEnabled()`
  - `public Boolean get_IsReadOnly()`
  - `public Int32 get_MaxLength()`
  - `public String get_PlaceholderText()`
  - `public Color get_PlaceholderTextColor()`
  - `public Color get_PrimaryCursorColor()`
  - `public Int32 get_PrimaryCursorPosition()`
  - `public Single get_ScaledFontSize()`
  - `public Boolean get_SystemFontScaleEnabled()`
  - `public String get_Text()`
  - `public Color get_TextColor()`
  - `public TextAlignment get_VerticalAlignment()`
  - `protected Int32 GetCursorWidth()`
  - `protected Boolean GetEnableMarkup()`
  - `protected String GetFontFamily()`
  - `protected Single GetFontSize()`
  - `protected Single GetFontSizeScale()`
  - `protected TextAlignment GetHorizontalAlignment()`
  - `protected IntPtr GetInputMethodContextHandle()`
  - `protected Int32 GetMaxLength()`
  - `protected String GetPlaceholderText()`
  - `protected Color GetPlaceholderTextColor()`
  - `protected Color GetPrimaryCursorColor()`
  - `protected Int32 GetPrimaryCursorPosition()`
  - `protected Boolean GetReadOnly()`
  - `protected String GetText()`
  - `protected Color GetTextColor()`
  - `protected TextAlignment GetVerticalAlignment()`
  - `public Void remove_AnchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `public Void remove_MaxLengthReached(EventHandler value)`
  - `public Void remove_TextChanged(EventHandler value)`
  - `protected Void SendAnchorClicked(String href)`
  - `protected Void SendMaxLengthReached()`
  - `protected Void SendTextChanged()`
  - `public Void set_CursorWidth(Int32 value)`
  - `public Void set_FontFamily(String value)`
  - `public Void set_FontSize(Single value)`
  - `public Void set_HorizontalAlignment(TextAlignment value)`
  - `public Void set_IsMarkupEnabled(Boolean value)`
  - `public Void set_IsReadOnly(Boolean value)`
  - `public Void set_MaxLength(Int32 value)`
  - `public Void set_PlaceholderText(String value)`
  - `public Void set_PlaceholderTextColor(Color value)`
  - `public Void set_PrimaryCursorColor(Color value)`
  - `public Void set_PrimaryCursorPosition(Int32 value)`
  - `public Void set_SystemFontScaleEnabled(Boolean value)`
  - `public Void set_Text(String value)`
  - `public Void set_TextColor(Color value)`
  - `public Void set_VerticalAlignment(TextAlignment value)`
  - `protected Void SetCursorWidth(Int32 value)`
  - `protected Void SetEnableMarkup(Boolean value)`
  - `protected Void SetFontFamily(String value)`
  - `protected Void SetFontSize(Single value)`
  - `protected Void SetFontSizeScale(Single value)`
  - `protected Void SetHorizontalAlignment(TextAlignment value)`
  - `public Void SetInputMethodSetting(InputMethodSetting inputMethod)`
  - `protected Void SetMaxLength(Int32 value)`
  - `protected Void SetPlaceholderText(String value)`
  - `protected Void SetPlaceholderTextColor(Color value)`
  - `protected Void SetPrimaryCursorColor(Color value)`
  - `protected Void SetPrimaryCursorPosition(Int32 value)`
  - `protected Void SetReadOnly(Boolean value)`
  - `protected Void SetText(String value)`
  - `protected Void SetTextColor(Color value)`
  - `public Void SetTextPadding(Thickness thickness)`
  - `protected Void SetVerticalAlignment(TextAlignment value)`
  - `private Single Tizen.UI.IFontSizeScale.get_FontSizeScale()`
  - `private Void Tizen.UI.IFontSizeScale.set_FontSizeScale(Single value)`
  - `private Boolean Tizen.UI.ITextEditable.get_IsEditable()`
  - `private Void Tizen.UI.ITextEditable.set_IsEditable(Boolean value)`
  - `public Int32 CursorWidth { get; set; }`
  - `public String FontFamily { get; set; }`
  - `public Single FontSize { get; set; }`
  - `public TextAlignment HorizontalAlignment { get; set; }`
  - `public InputMethodContext InputMethodContext { get; }`
  - `public Boolean IsMarkupEnabled { get; set; }`
  - `public Boolean IsReadOnly { get; set; }`
  - `public Int32 MaxLength { get; set; }`
  - `public String PlaceholderText { get; set; }`
  - `public Color PlaceholderTextColor { get; set; }`
  - `public Color PrimaryCursorColor { get; set; }`
  - `public Int32 PrimaryCursorPosition { get; set; }`
  - `public Single ScaledFontSize { get; }`
  - `public Boolean SystemFontScaleEnabled { get; set; }`
  - `public String Text { get; set; }`
  - `public Color TextColor { get; set; }`
  - `private Single Tizen.UI.IFontSizeScale.FontSizeScale { get; set; }`
  - `private Boolean Tizen.UI.ITextEditable.IsEditable { get; set; }`
  - `public TextAlignment VerticalAlignment { get; set; }`

### class `InputViewExtensions`

- Base: `System.Object`
- Members:
  - `public static TView CursorWidth(TView view, Int32 width)`
  - `public static TView FontFamily(TView view, String fontFamily)`
  - `public static TView FontSize(TView view, Single fontSize)`
  - `public static TView HorizontalAlignment(TView view, TextAlignment alignment)`
  - `public static TView IsMarkupEnabled(TView view, Boolean enable)`
  - `public static TView IsReadOnly(TView view, Boolean isReadOnly)`
  - `public static TView MaxLength(TView view, Int32 length)`
  - `public static TView PlaceholderText(TView view, String text)`
  - `public static TView PlaceholderTextColor(TView view, Color color)`
  - `public static TView PlaceholderTextColorFromHex(TView view, String color)`
  - `public static TView PrimaryCursorColor(TView view, Color color)`
  - `public static TView Text(TView view, String text)`
  - `public static TView TextColor(TView view, Color textColor)`
  - `public static TView TextColorFromHex(TView view, String textColor)`
  - `public static TView VerticalAlignment(TView view, TextAlignment alignment)`

### class `Interop`

- Base: `System.Object`
- Members:

### interface `IParentObject`

- Members:
  - `public Void Remove(View view)`

### interface `IScrollable`

- Members:
  - `public event EventHandler<DragEventArgs> DragFinished`
  - `public event EventHandler<DragEventArgs> Dragging`
  - `public event EventHandler<DragEventArgs> DragStarted`
  - `public event EventHandler<ScrollEventArgs> ScrollFinished`
  - `public event EventHandler<ScrollEventArgs> Scrolling`
  - `public event EventHandler<ScrollEventArgs> ScrollStarted`
  - `public Void add_DragFinished(EventHandler<DragEventArgs> value)`
  - `public Void add_Dragging(EventHandler<DragEventArgs> value)`
  - `public Void add_DragStarted(EventHandler<DragEventArgs> value)`
  - `public Void add_ScrollFinished(EventHandler<ScrollEventArgs> value)`
  - `public Void add_Scrolling(EventHandler<ScrollEventArgs> value)`
  - `public Void add_ScrollStarted(EventHandler<ScrollEventArgs> value)`
  - `public Boolean get_CanScrollHorizontally()`
  - `public Boolean get_CanScrollVertically()`
  - `public Boolean get_IsScrolledToEnd()`
  - `public Boolean get_IsScrolledToStart()`
  - `public Boolean get_IsScrolling()`
  - `public Rect get_ViewPort()`
  - `public Void remove_DragFinished(EventHandler<DragEventArgs> value)`
  - `public Void remove_Dragging(EventHandler<DragEventArgs> value)`
  - `public Void remove_DragStarted(EventHandler<DragEventArgs> value)`
  - `public Void remove_ScrollFinished(EventHandler<ScrollEventArgs> value)`
  - `public Void remove_Scrolling(EventHandler<ScrollEventArgs> value)`
  - `public Void remove_ScrollStarted(EventHandler<ScrollEventArgs> value)`
  - `public Task ScrollBy(Single dx, Single dy, Boolean animated)`
  - `public Task ScrollTo(Single x, Single y, Boolean animation)`
  - `public Boolean CanScrollHorizontally { get; }`
  - `public Boolean CanScrollVertically { get; }`
  - `public Boolean IsScrolledToEnd { get; }`
  - `public Boolean IsScrolledToStart { get; }`
  - `public Boolean IsScrolling { get; }`
  - `public Rect ViewPort { get; }`

### interface `IScrollBar`

- Interfaces: `System.IDisposable`
- Members:
  - `public ScrollBarVisibility get_HorizontalScrollBarVisibility()`
  - `public View get_Target()`
  - `public ScrollBarVisibility get_VerticalScrollBarVisibility()`
  - `public Void OnAttachedToScrollable(IScrollable scrollable)`
  - `public Void set_HorizontalScrollBarVisibility(ScrollBarVisibility value)`
  - `public Void set_VerticalScrollBarVisibility(ScrollBarVisibility value)`
  - `public Void UpdateBarSize(Size scrollArea, Size viewportSize)`
  - `public Void UpdateScrollPosition(Point position)`
  - `public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }`
  - `public View Target { get; }`
  - `public ScrollBarVisibility VerticalScrollBarVisibility { get; set; }`

### interface `IText`

- Members:
  - `public String get_FontFamily()`
  - `public Single get_FontSize()`
  - `public String get_Text()`
  - `public Color get_TextColor()`
  - `public Void set_FontFamily(String value)`
  - `public Void set_FontSize(Single value)`
  - `public Void set_Text(String value)`
  - `public Void set_TextColor(Color value)`
  - `public String FontFamily { get; set; }`
  - `public Single FontSize { get; set; }`
  - `public String Text { get; set; }`
  - `public Color TextColor { get; set; }`

### interface `ITextAlignment`

- Members:
  - `public TextAlignment get_HorizontalAlignment()`
  - `public TextAlignment get_VerticalAlignment()`
  - `public Void set_HorizontalAlignment(TextAlignment value)`
  - `public Void set_VerticalAlignment(TextAlignment value)`
  - `public TextAlignment HorizontalAlignment { get; set; }`
  - `public TextAlignment VerticalAlignment { get; set; }`

### interface `ITextEditable`

- Members:
  - `public event EventHandler TextChanged`
  - `public Void add_TextChanged(EventHandler value)`
  - `public InputMethodContext get_InputMethodContext()`
  - `public Boolean get_IsEditable()`
  - `public String get_PlaceholderText()`
  - `public Void remove_TextChanged(EventHandler value)`
  - `public Void set_IsEditable(Boolean value)`
  - `public Void set_PlaceholderText(String value)`
  - `public InputMethodContext InputMethodContext { get; }`
  - `public Boolean IsEditable { get; set; }`
  - `public String PlaceholderText { get; set; }`

### interface `ITextFormatting`

- Members:
  - `public Boolean get_IsMultiline()`
  - `public TextOverflow get_TextOverflow()`
  - `public Void set_IsMultiline(Boolean value)`
  - `public Void set_TextOverflow(TextOverflow value)`
  - `public Boolean IsMultiline { get; set; }`
  - `public TextOverflow TextOverflow { get; set; }`

### interface `ITextPadding`

- Members:
  - `public Void SetTextPadding(Thickness padding)`

### interface `IToken`

- Members:
  - `public String get_Id()`
  - `public String Id { get; }`

### interface `ITokenTable`1`

- Members:
  - `public event EventHandler Updated`
  - `public Void add_Updated(EventHandler value)`
  - `public Void Bind(View target, String name, Action<Object, T> setter, IToken token)`
  - `public Void remove_Updated(EventHandler value)`
  - `public Boolean TryGetToken(View target, String propertyName, IToken& token)`
  - `public Boolean TryGetValue(String id, T& result)`
  - `public Void Unbind(View target, String name)`
  - `public Void Update(IDictionary<String, T> table)`

### enum `KeyDeviceClass`

- Base: `System.Enum`
- Members:

### enum `KeyDeviceSubClass`

- Base: `System.Enum`
- Members:

### class `KeyEvent`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean hasOwnership)`
  - `public KeyDeviceClass get_DeviceClass()`
  - `public String get_DeviceName()`
  - `public KeyDeviceSubClass get_DeviceSubClass()`
  - `public Boolean get_IsAltModifier()`
  - `public Boolean get_IsCtrlModifier()`
  - `public Boolean get_IsShiftModifier()`
  - `public Int32 get_KeyCode()`
  - `public Int32 get_KeyModifier()`
  - `public String get_KeyPressedName()`
  - `public String get_LogicalKey()`
  - `public KeyState get_State()`
  - `public UInt32 get_Time()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void set_KeyCode(Int32 value)`
  - `public Void set_KeyModifier(Int32 value)`
  - `public Void set_KeyPressedName(String value)`
  - `public Void set_State(KeyState value)`
  - `public Void set_Time(UInt32 value)`
  - `public KeyDeviceClass DeviceClass { get; }`
  - `public String DeviceName { get; }`
  - `public KeyDeviceSubClass DeviceSubClass { get; }`
  - `public Boolean IsAltModifier { get; }`
  - `public Boolean IsCtrlModifier { get; }`
  - `public Boolean IsShiftModifier { get; }`
  - `public Int32 KeyCode { get; set; }`
  - `public Int32 KeyModifier { get; set; }`
  - `public String KeyPressedName { get; set; }`
  - `public String LogicalKey { get; }`
  - `public KeyState State { get; set; }`
  - `public UInt32 Time { get; set; }`

### class `KeyEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public Boolean get_Handled()`
  - `public KeyEvent get_KeyEvent()`
  - `public Void set_Handled(Boolean value)`
  - `public Void set_KeyEvent(KeyEvent value)`
  - `public Boolean Handled { get; set; }`
  - `public KeyEvent KeyEvent { get; set; }`

### enum `KeyGrabMode`

- Base: `System.Enum`
- Members:

### enum `KeyState`

- Base: `System.Enum`
- Members:

### class `Layer`

- Base: `Tizen.UI.NObject`
- Interfaces: `Tizen.UI.IParentObject`
- Members:
  - `public .ctor()`
  - `internal .ctor(IntPtr layer)`
  - `public Void Add(View item)`
  - `protected Void Dispose(Boolean disposing)`
  - `public IList<View> get_Children()`
  - `public Int32 get_Count()`
  - `public ResizePolicy get_HeightResizePolicy()`
  - `public Boolean get_InheritLayoutDirection()`
  - `public Boolean get_IsVisible()`
  - `public View get_Item(Int32 index)`
  - `public LayoutDirection get_LayoutDirection()`
  - `public String get_Name()`
  - `public ResizePolicy get_WidthResizePolicy()`
  - `private ResizePolicy GetResizePolicy(Int32 dimension)`
  - `private Void OnChildAdded(View view)`
  - `private Void OnChildRemoved(View view)`
  - `private Void OnChildrenChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Boolean Remove(View item)`
  - `public Void set_HeightResizePolicy(ResizePolicy value)`
  - `public Void set_InheritLayoutDirection(Boolean value)`
  - `public Void set_IsVisible(Boolean value)`
  - `public Void set_Item(Int32 index, View value)`
  - `public Void set_LayoutDirection(LayoutDirection value)`
  - `public Void set_Name(String value)`
  - `public Void set_WidthResizePolicy(ResizePolicy value)`
  - `private Void SetResizePolicy(Int32 dimension, ResizePolicy policy)`
  - `public Void SetSize(Single width, Single height)`
  - `private Void Tizen.UI.IParentObject.Remove(View view)`
  - `public IList<View> Children { get; }`
  - `public Int32 Count { get; }`
  - `public ResizePolicy HeightResizePolicy { get; set; }`
  - `public Boolean InheritLayoutDirection { get; set; }`
  - `public Boolean IsVisible { get; set; }`
  - `public View Item { get; set; }`
  - `public LayoutDirection LayoutDirection { get; set; }`
  - `public String Name { get; set; }`
  - `public ResizePolicy WidthResizePolicy { get; set; }`

### enum `LayoutDirection`

- Base: `System.Enum`
- Members:

### class `LinearGradientBackground`

- Base: `Tizen.UI.GradientBackground`
- Members:
  - `public .ctor()`
  - `public Void BuildPropertyMap(PropertyMapHandle map)`
  - `public Point get_EndPoint()`
  - `public Point get_StartPoint()`
  - `public Void set_EndPoint(Point value)`
  - `public Void set_StartPoint(Point value)`
  - `public Point EndPoint { get; set; }`
  - `public Point StartPoint { get; set; }`

### enum `LineBreakMode`

- Base: `System.Enum`
- Members:

### class `Log`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Void Debug(String message, String file, String func, Int32 line)`
  - `public static Void Error(String message, String file, String func, Int32 line)`
  - `public static String get_Tag()`
  - `public static Void Info(String message, String file, String func, Int32 line)`
  - `public static Void set_Tag(String value)`
  - `public static Void Warn(String message, String file, String func, Int32 line)`
  - `public String Tag { get; set; }`

### class `LongPressGesture`

- Base: `Tizen.UI.Gesture`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Int32 get_NumberOfTouches()`
  - `public Point get_Position()`
  - `public Point get_ScreenPosition()`
  - `public Int32 NumberOfTouches { get; }`
  - `public Point Position { get; }`
  - `public Point ScreenPosition { get; }`

### class `LongPressGestureDetectedEventArgs`

- Base: `Tizen.UI.GestureDetectedEventArgs`
- Members:
  - `public .ctor(View view, LongPressGesture gesture)`
  - `public LongPressGesture get_Gesture()`
  - `public LongPressGesture Gesture { get; }`

### class `LongPressGestureDetector`

- Base: `Tizen.UI.GestureDetector`
- Members:
  - `public .ctor()`
  - `private event EventHandler<LongPressGestureDetectedEventArgs> _detected`
  - `public event EventHandler<LongPressGestureDetectedEventArgs> Detected`
  - `private Void add__detected(EventHandler<LongPressGestureDetectedEventArgs> value)`
  - `public Void add_Detected(EventHandler<LongPressGestureDetectedEventArgs> value)`
  - `private Void OnDetected(IntPtr view, IntPtr gesture)`
  - `private Void remove__detected(EventHandler<LongPressGestureDetectedEventArgs> value)`
  - `public Void remove_Detected(EventHandler<LongPressGestureDetectedEventArgs> value)`
  - `public Void SetTouchesRequired(Int32 touches)`
  - `public Void SetTouchesRequired(Int32 min, Int32 max)`

### class `LottieAnimationView`

- Base: `Tizen.UI.ImageView`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `private event EventHandler _finished`
  - `public event EventHandler Finished`
  - `private Void add__finished(EventHandler value)`
  - `public Void add_Finished(EventHandler value)`
  - `private Void ClearImageMapCache()`
  - `protected ImageVisualMap CreateImageVisualMap()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Int32 get_CurrentFrame()`
  - `public Boolean get_Loop()`
  - `public AnimationState get_PlayState()`
  - `public Int32 get_RepeatCount()`
  - `public AnimationRepeatMode get_RepeatMode()`
  - `public AnimationStopBehavior get_StopBehavior()`
  - `public Int32 get_TotalFrame()`
  - `protected LottieVisualMap get_VisualMap()`
  - `public IList<ValueTuple<String, Int32, Int32>> GetContentInfo()`
  - `private Int32 GetCurrentFrame()`
  - `private PropertyMapHandle GetImageMapForRead()`
  - `public IList<ValueTuple<String, Int32, Int32>> GetMarkerInfo()`
  - `private AnimationState GetPlayState()`
  - `private Int32 GetTotalFrame()`
  - `private Void ImageMapCacheClearHandler(Object sender, EventArgs e)`
  - `private Void OnFinished(IntPtr view, Int32 visualIndex, Int32 signalId)`
  - `public Void Pause()`
  - `public Void Play()`
  - `public Void Play(Int32 startFrame, Int32 endFrame)`
  - `public Void Play(String startMarker, String endMarker)`
  - `private Void remove__finished(EventHandler value)`
  - `public Void remove_Finished(EventHandler value)`
  - `private Void RequestClearImageMapCache()`
  - `public Void set_CurrentFrame(Int32 value)`
  - `public Void set_Loop(Boolean value)`
  - `public Void set_RepeatCount(Int32 value)`
  - `public Void set_RepeatMode(AnimationRepeatMode value)`
  - `public Void set_StopBehavior(AnimationStopBehavior value)`
  - `private Void SetCurrentFrame(Int32 frame)`
  - `private Void SetFrameRange(Int32 min, Int32 max)`
  - `private Void SetFrameRange(String min, String max)`
  - `private Void SetRepeatCount(Int32 value)`
  - `private Void SetRepeatMode(AnimationRepeatMode mode)`
  - `private Void SetStopBehavior(AnimationStopBehavior behavior)`
  - `public Void Stop()`
  - `protected Void UpdateImageMap()`
  - `public Int32 CurrentFrame { get; set; }`
  - `public Boolean Loop { get; set; }`
  - `public AnimationState PlayState { get; }`
  - `public Int32 RepeatCount { get; set; }`
  - `public AnimationRepeatMode RepeatMode { get; set; }`
  - `public AnimationStopBehavior StopBehavior { get; set; }`
  - `public Int32 TotalFrame { get; }`
  - `protected LottieVisualMap VisualMap { get; }`

### class `MaskEffect`

- Base: `Tizen.UI.RenderEffect`
- Members:
  - `public .ctor(View sourceView)`
  - `internal SafeHandle CreateNativeHandle()`

### enum `MouseButton`

- Base: `System.Enum`
- Members:

### class `NDalicManualPINVOKE`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public static Int32 HiddeninputPropertyModeGet()`
  - `public static Int32 HiddeninputPropertyShowLastCharacterDurationGet()`
  - `public static Int32 HiddeninputPropertySubstituteCharacterGet()`
  - `public static Int32 HiddeninputPropertySubstituteCountGet()`
  - `public static Int32 TextSelectionPopupPropertyEnableScrollBarGet()`
  - `public static Int32 TextSelectionPopupPropertyLabelMinimumSizeGet()`
  - `public static Int32 TextSelectionPopupPropertyLabelPaddingGet()`
  - `public static Int32 TextSelectionPopupPropertyLabelTextVisualGet()`
  - `public static Int32 TextSelectionPopupPropertyOptionDividerPaddingGet()`
  - `public static Int32 TextSelectionPopupPropertyOptionDividerSizeGet()`
  - `public static Int32 TextSelectionPopupPropertyPopupBackgroundBorderGet()`
  - `public static Int32 TextSelectionPopupPropertyPopupBackgroundGet()`
  - `public static Int32 TextSelectionPopupPropertyPopupDividerColorGet()`
  - `public static Int32 TextSelectionPopupPropertyPopupFadeInDurationGet()`
  - `public static Int32 TextSelectionPopupPropertyPopupFadeOutDurationGet()`
  - `public static Int32 TextSelectionPopupPropertyPopupMaxSizeGet()`
  - `public static Int32 TextSelectionPopupPropertyPopupPressedColorGet()`
  - `public static Int32 TextSelectionPopupPropertyPopupPressedCornerRadiusGet()`

### class `NDalicPINVOKE`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public static IntPtr ApplicationAppControlSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationDeviceOrientationChangedSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationInitSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationLanguageChangedSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationLowBatterySignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationLowMemorySignal(IntPtr jarg1)`
  - `public static Void ApplicationMainLoop(IntPtr jarg1)`
  - `public static IntPtr ApplicationNewManual4(Int32 jarg1, String jarg2, String jarg3, Int32 jarg4)`
  - `public static IntPtr ApplicationNewWithWindowSizePosition(Int32 jarg1, String jarg2, String jarg3, Int32 jarg4, RectangleHandle jarg5)`
  - `public static IntPtr ApplicationPauseSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationRegionChangedSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationResetSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationResumeSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTaskAppControlSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTaskDeviceOrientationChangedSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTaskInitSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTaskLanguageChangedSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTaskLowBatterySignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTaskLowMemorySignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTaskRegionChangedSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTaskTerminateSignal(IntPtr jarg1)`
  - `public static IntPtr ApplicationTerminateSignal(IntPtr jarg1)`
  - `public static Void DeleteBaseHandle(IntPtr jarg1)`
  - `internal static Void ThrowExceptionIfExists()`
  - `internal static Void ThrowExceptionIfExistsDebug()`

### class `NObject`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `private static .cctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected .ctor(IntPtr handle)`
  - `protected Void AddToNativeHolder(Object obj)`
  - `protected T AddToObjectHolder(T obj)`
  - `private protected Void AddToObjectTable()`
  - `private static Void ClearHolder(IntPtr handle)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected Void Finalize()`
  - `public IntPtr get_Handle()`
  - `public Boolean get_HasOwnership()`
  - `public Boolean get_IsDisposed()`
  - `public static NObject GetObjectFromNativeObject(IntPtr nativeObj)`
  - `internal static NObject GetObjectFromNativeObject(ActorHandle handle)`
  - `public static IntPtr op_Implicit(NObject obj)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `protected Void RemoveFromHolder(Object obj)`
  - `private protected Void RemoveFromObjectTable()`
  - `public Void set_HasOwnership(Boolean value)`
  - `public IntPtr Handle { get; }`
  - `public Boolean HasOwnership { get; set; }`
  - `public Boolean IsDisposed { get; }`

### class `NumericExtensions`

- Base: `System.Object`
- Members:
  - `public static Single Clamp(Single self, Single min, Single max)`
  - `public static Int32 Clamp(Int32 self, Int32 min, Int32 max)`

### enum `OffScreenRendering`

- Base: `System.Enum`
- Members:

### class `OpacityPropertyValue`

- Base: `Tizen.UI.AnimatablePropertyValue`
- Members:
  - `public .ctor(Single opacity)`
  - `public Single get_Opacity()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `public Single Opacity { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `OriginPoint`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Point get_BottomCenter()`
  - `public static Point get_BottomLeft()`
  - `public static Point get_BottomRight()`
  - `public static Point get_Center()`
  - `public static Point get_CenterLeft()`
  - `public static Point get_CenterRight()`
  - `public static Point get_TopCenter()`
  - `public static Point get_TopLeft()`
  - `public static Point get_TopRight()`
  - `public Point BottomCenter { get; }`
  - `public Point BottomLeft { get; }`
  - `public Point BottomRight { get; }`
  - `public Point Center { get; }`
  - `public Point CenterLeft { get; }`
  - `public Point CenterRight { get; }`
  - `public Point TopCenter { get; }`
  - `public Point TopLeft { get; }`
  - `public Point TopRight { get; }`

### struct `Outline`

- Base: `System.ValueType`
- Members:
  - `public Nullable<Single> get_BlurRadius()`
  - `public Nullable<Color> get_Color()`
  - `public Nullable<Point> get_Offset()`
  - `public Nullable<Single> get_Width()`
  - `public Void set_BlurRadius(Nullable<Single> value)`
  - `public Void set_Color(Nullable<Color> value)`
  - `public Void set_Offset(Nullable<Point> value)`
  - `public Void set_Width(Nullable<Single> value)`
  - `public Nullable<Single> BlurRadius { get; set; }`
  - `public Nullable<Color> Color { get; set; }`
  - `public Nullable<Point> Offset { get; set; }`
  - `public Nullable<Single> Width { get; set; }`

### enum `PanelLayout`

- Base: `System.Enum`
- Members:

### class `PanGesture`

- Base: `Tizen.UI.Gesture`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Point get_Displacement()`
  - `public Int32 get_NumberOfTouches()`
  - `public Point get_Position()`
  - `public Point get_ScreenDisplacement()`
  - `public Point get_ScreenPosition()`
  - `public Point get_ScreenVelocity()`
  - `public Point get_Velocity()`
  - `public Point Displacement { get; }`
  - `public Int32 NumberOfTouches { get; }`
  - `public Point Position { get; }`
  - `public Point ScreenDisplacement { get; }`
  - `public Point ScreenPosition { get; }`
  - `public Point ScreenVelocity { get; }`
  - `public Point Velocity { get; }`

### class `PanGestureDetectedEventArgs`

- Base: `Tizen.UI.GestureDetectedEventArgs`
- Members:
  - `public .ctor(View view, PanGesture gesture)`
  - `public PanGesture get_Gesture()`
  - `public PanGesture Gesture { get; }`

### class `PanGestureDetector`

- Base: `Tizen.UI.GestureDetector`
- Members:
  - `public .ctor()`
  - `private event EventHandler<PanGestureDetectedEventArgs> _detected`
  - `public event EventHandler<PanGestureDetectedEventArgs> Detected`
  - `private Void add__detected(EventHandler<PanGestureDetectedEventArgs> value)`
  - `public Void add_Detected(EventHandler<PanGestureDetectedEventArgs> value)`
  - `public PanGestureDirection get_Direction()`
  - `public Int32 get_MaximumTouchesRequired()`
  - `public Int32 get_MinimumTouchesRequired()`
  - `private Void OnDetected(IntPtr view, IntPtr gesture)`
  - `private Void remove__detected(EventHandler<PanGestureDetectedEventArgs> value)`
  - `public Void remove_Detected(EventHandler<PanGestureDetectedEventArgs> value)`
  - `public Void set_Direction(PanGestureDirection value)`
  - `public Void set_MaximumTouchesRequired(Int32 value)`
  - `public Void set_MinimumTouchesRequired(Int32 value)`
  - `public PanGestureDirection Direction { get; set; }`
  - `public Int32 MaximumTouchesRequired { get; set; }`
  - `public Int32 MinimumTouchesRequired { get; set; }`

### enum `PanGestureDirection`

- Base: `System.Enum`
- Members:

### class `PinchGesture`

- Base: `Tizen.UI.Gesture`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Point get_Center()`
  - `public Single get_Scale()`
  - `public Point get_ScreenCenter()`
  - `public Single get_Speed()`
  - `public Point Center { get; }`
  - `public Single Scale { get; }`
  - `public Point ScreenCenter { get; }`
  - `public Single Speed { get; }`

### class `PinchGestureDetectedEventArgs`

- Base: `Tizen.UI.GestureDetectedEventArgs`
- Members:
  - `public .ctor(View view, PinchGesture gesture)`
  - `public PinchGesture get_Gesture()`
  - `public PinchGesture Gesture { get; }`

### class `PinchGestureDetector`

- Base: `Tizen.UI.GestureDetector`
- Members:
  - `public .ctor()`
  - `private event EventHandler<PinchGestureDetectedEventArgs> _detected`
  - `public event EventHandler<PinchGestureDetectedEventArgs> Detected`
  - `private Void add__detected(EventHandler<PinchGestureDetectedEventArgs> value)`
  - `public Void add_Detected(EventHandler<PinchGestureDetectedEventArgs> value)`
  - `private Void OnDetected(IntPtr view, IntPtr gesture)`
  - `private Void remove__detected(EventHandler<PinchGestureDetectedEventArgs> value)`
  - `public Void remove_Detected(EventHandler<PinchGestureDetectedEventArgs> value)`

### class `PixelBuffer`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(Int32 width, Int32 height, PixelFormat pixelFormat)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public static PixelData Convert(PixelBuffer pixelBuffer)`
  - `public PixelData Export()`
  - `public Int32 get_Height()`
  - `public PixelFormat get_PixelFormat()`
  - `public Int32 get_Width()`
  - `public IntPtr GetBuffer()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void Resize(Int32 width, Int32 height)`
  - `public Int32 Height { get; }`
  - `public PixelFormat PixelFormat { get; }`
  - `public Int32 Width { get; }`

### class `PixelData`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(Byte[] buffer, Int32 width, Int32 height, PixelFormat pixelFormat)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public ImageUrl GenerateUrl()`
  - `public UInt32 get_Height()`
  - `public PixelFormat get_PixelFormat()`
  - `public UInt32 get_Width()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public UInt32 Height { get; }`
  - `public PixelFormat PixelFormat { get; }`
  - `public UInt32 Width { get; }`

### enum `PixelFormat`

- Base: `System.Enum`
- Members:

### struct `Point`

- Base: `System.ValueType`
- Members:
  - `private static .cctor()`
  - `public .ctor(Single x, Single y)`
  - `public .ctor(Size sz)`
  - `public Void Deconstruct(Single& x, Single& y)`
  - `public Double Distance(Point other)`
  - `public Boolean Equals(Object o)`
  - `public Boolean get_IsEmpty()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Int32 GetHashCode()`
  - `public Point Offset(Single dx, Single dy)`
  - `public static Point op_Addition(Point pt, Size sz)`
  - `public static Point op_Addition(Point p1, Point p2)`
  - `public static Boolean op_Equality(Point ptA, Point ptB)`
  - `public static Size op_Explicit(Point pt)`
  - `public static Boolean op_Inequality(Point ptA, Point ptB)`
  - `public static Point op_Subtraction(Point pt, Size sz)`
  - `public static Point op_Subtraction(Point p1, Point p2)`
  - `public Point Round()`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public String ToString()`
  - `public Boolean IsEmpty { get; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`

### class `PositionPropertyValue`

- Base: `Tizen.UI.AnimatablePropertyValue`
- Members:
  - `public .ctor(Single x, Single y)`
  - `public PropertyValueHandle get_Value()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `public PropertyValueHandle Value { get; }`
  - `public Single X { get; }`
  - `public Single Y { get; }`

### class `PrimitiveExtensions`

- Base: `System.Object`
- Members:
  - `public static Boolean IsExplicitSet(Single value)`

### class `PropertiesExtensions`

- Base: `System.Object`
- Members:
  - `public static List<ValueTuple<AnimatablePropertyHandle, PropertyValueHandle>> AddProperty(List<ValueTuple<AnimatablePropertyHandle, PropertyValueHandle>> properties, AnimatablePropertyHandle property, PropertyValueHandle value)`

### class `PublicKey`

- Base: `System.Object`
- Members:

### class `RadialGradientBackground`

- Base: `Tizen.UI.GradientBackground`
- Members:
  - `public .ctor()`
  - `public Void BuildPropertyMap(PropertyMapHandle map)`
  - `public Point get_Center()`
  - `public Single get_Radius()`
  - `public Void set_Center(Point value)`
  - `public Void set_Radius(Single value)`
  - `public Point Center { get; set; }`
  - `public Single Radius { get; set; }`

### struct `Rect`

- Base: `System.ValueType`
- Members:
  - `private static .cctor()`
  - `public .ctor(Single x, Single y, Single width, Single height)`
  - `public .ctor(Point loc, Size sz)`
  - `public Boolean Contains(Rect rect)`
  - `public Boolean Contains(Point pt)`
  - `public Boolean Contains(Single x, Single y)`
  - `public Void Deconstruct(Single& x, Single& y, Single& width, Single& height)`
  - `public Boolean Equals(Rect other)`
  - `public Boolean Equals(Object obj)`
  - `public static Rect FromLTRB(Single left, Single top, Single right, Single bottom)`
  - `public Single get_Bottom()`
  - `public Point get_Center()`
  - `public Single get_Height()`
  - `public Boolean get_IsEmpty()`
  - `public Single get_Left()`
  - `public Point get_Location()`
  - `public Single get_Right()`
  - `public Size get_Size()`
  - `public Single get_Top()`
  - `public Single get_Width()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Int32 GetHashCode()`
  - `public Rect Inflate(Size sz)`
  - `public Rect Inflate(Single width, Single height)`
  - `public Rect Intersect(Rect r)`
  - `public static Rect Intersect(Rect r1, Rect r2)`
  - `public Boolean IntersectsWith(Rect r)`
  - `public Rect Offset(Single dx, Single dy)`
  - `public Rect Offset(Point dr)`
  - `public static Boolean op_Equality(Rect r1, Rect r2)`
  - `public static Boolean op_Inequality(Rect r1, Rect r2)`
  - `public Rect Round()`
  - `public Void set_Height(Single value)`
  - `public Void set_Location(Point value)`
  - `public Void set_Size(Size value)`
  - `public Void set_Width(Single value)`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public String ToString()`
  - `public Rect Union(Rect r)`
  - `public static Rect Union(Rect r1, Rect r2)`
  - `public Single Bottom { get; }`
  - `public Point Center { get; }`
  - `public Single Height { get; set; }`
  - `public Boolean IsEmpty { get; }`
  - `public Single Left { get; }`
  - `public Point Location { get; set; }`
  - `public Single Right { get; }`
  - `public Size Size { get; set; }`
  - `public Single Top { get; }`
  - `public Single Width { get; set; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`

### class `RenderEffect`

- Base: `System.Object`
- Members:
  - `protected .ctor()`
  - `public static RenderEffect CreateBackdropBlurEffect(Single radius)`
  - `public static RenderEffect CreateGaussianBlurEffect(Single radius)`
  - `public static RenderEffect CreateMaskEffect(View view)`
  - `internal SafeHandle CreateNativeHandle()`

### enum `RenderNotificationKey`

- Base: `System.Enum`
- Members:

### enum `ResizePolicy`

- Base: `System.Enum`
- Members:

### class `ResourceLocalizer`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Void BindingLocalized(T target, Action<T, String> setter, String tag, ResourceManager resourceManager)`
  - `public static Void BindingLocalizedResource(T target, Action<T, String> setter, String tag)`
  - `public static Void ClearBinding(View view)`
  - `public static ResourceManager get_EffectiveResourceManager()`
  - `public static ResourceManager get_ResourceManager()`
  - `public static Void LoadResourceManager(String baseName, Assembly assembly)`
  - `public static String LocalizedResource(String path)`
  - `public static String LocalizedString(String key)`
  - `public static String LocalizedString(String key, ResourceManager resourceManager)`
  - `private static Void OnLocaleChanged(Object sender, LocaleChangedEventArgs e)`
  - `public static Void PopEffectiveResourceManager()`
  - `public static Void PushEffectiveResourceManager(ResourceManager manager)`
  - `public static Void set_ResourceManager(ResourceManager value)`
  - `public static Void SetDefaultCulture(CultureInfo culture)`
  - `public static Void SetLocalizeBypass(Boolean value)`
  - `private static Void UpdateBinding()`
  - `private static Void UpdateResourceManager()`
  - `public ResourceManager EffectiveResourceManager { get; }`
  - `public ResourceManager ResourceManager { get; set; }`

### enum `ReturnType`

- Base: `System.Enum`
- Members:

### class `RotationGesture`

- Base: `Tizen.UI.Gesture`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Point get_Center()`
  - `public Single get_Rotation()`
  - `public Point get_ScreenCenter()`
  - `public Point Center { get; }`
  - `public Single Rotation { get; }`
  - `public Point ScreenCenter { get; }`

### class `RotationGestureDetectedEventArgs`

- Base: `Tizen.UI.GestureDetectedEventArgs`
- Members:
  - `public .ctor(View view, RotationGesture gesture)`
  - `public RotationGesture get_Gesture()`
  - `public RotationGesture Gesture { get; }`

### class `RotationGestureDetector`

- Base: `Tizen.UI.GestureDetector`
- Members:
  - `public .ctor()`
  - `private event EventHandler<RotationGestureDetectedEventArgs> _detected`
  - `public event EventHandler<RotationGestureDetectedEventArgs> Detected`
  - `private Void add__detected(EventHandler<RotationGestureDetectedEventArgs> value)`
  - `public Void add_Detected(EventHandler<RotationGestureDetectedEventArgs> value)`
  - `private Void OnDetected(IntPtr view, IntPtr gesture)`
  - `private Void remove__detected(EventHandler<RotationGestureDetectedEventArgs> value)`
  - `public Void remove_Detected(EventHandler<RotationGestureDetectedEventArgs> value)`

### class `RotationPropertyValue`

- Base: `Tizen.UI.AnimatablePropertyValue`
- Members:
  - `public .ctor(Single rotationX, Single rotationY, Single rotationZ)`
  - `public Single get_RotationX()`
  - `public Single get_RotationY()`
  - `public Single get_RotationZ()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `public Single RotationX { get; }`
  - `public Single RotationY { get; }`
  - `public Single RotationZ { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `ScalePropertyValue`

- Base: `Tizen.UI.AnimatablePropertyValue`
- Members:
  - `public .ctor(Single x, Single y)`
  - `public Size get_Scale()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `public Size Scale { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `ScopedResourceManager`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `public .ctor(ResourceManager manager)`
  - `public Void Dispose()`
  - `protected Void Finalize()`
  - `public static ScopedResourceManager Scoped(ResourceManager resourceManager)`

### class `ScreenShot`

- Base: `Tizen.UI.NObject`
- Members:
  - `private .ctor()`
  - `public static Task<ImageUrl> CaptureAsync(View target)`
  - `public Task<ImageUrl> InternalCaptureAsync(View target)`
  - `public Task<ImageUrl> InternalCaptureAsync(View target, Point position, Size size, Color clearColor)`
  - `private Void OnCaptureResult(IntPtr handle, Int32 state)`

### enum `ScrollBarVisibility`

- Base: `System.Enum`
- Members:

### enum `ScrollDirection`

- Base: `System.Enum`
- Members:

### class `ScrollEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public .ctor(Point scrollPosition, Point displacement)`
  - `public Point get_Displacement()`
  - `public Point get_ScrollPosition()`
  - `public Void set_Displacement(Point value)`
  - `public Void set_ScrollPosition(Point value)`
  - `public Point Displacement { get; set; }`
  - `public Point ScrollPosition { get; set; }`

### enum `ScrollToPosition`

- Base: `System.Enum`
- Members:

### struct `Shadow`

- Base: `System.ValueType`
- Members:
  - `public Single get_BlurRadius()`
  - `public Color get_Color()`
  - `public VisualCutoutPolicy get_CutoutPolicy()`
  - `public Size get_Extends()`
  - `public Point get_Offset()`
  - `public Void set_BlurRadius(Single value)`
  - `public Void set_Color(Color value)`
  - `public Void set_CutoutPolicy(VisualCutoutPolicy value)`
  - `public Void set_Extends(Size value)`
  - `public Void set_Offset(Point value)`
  - `public Single BlurRadius { get; set; }`
  - `public Color Color { get; set; }`
  - `public VisualCutoutPolicy CutoutPolicy { get; set; }`
  - `public Size Extends { get; set; }`
  - `public Point Offset { get; set; }`

### class `ShadowTokenPropertyChangedEventArgs`

- Base: `Tizen.UI.ColorTokenPropertyChangedEventArgs`
- Members:
  - `public .ctor()`
  - `public static ShadowTokenPropertyChangedEventArgs Create(View view, String name, Shadow shadow)`
  - `public Shadow get_Shadow()`
  - `public Void set_Shadow(Shadow value)`
  - `public Shadow Shadow { get; set; }`

### struct `Size`

- Base: `System.ValueType`
- Interfaces: `System.IEquatable`1<Tizen.UI.Size>`
- Members:
  - `public .ctor(Single width, Single height)`
  - `public Void Deconstruct(Single& width, Single& height)`
  - `public Boolean Equals(Size other)`
  - `public Boolean Equals(Object obj)`
  - `public Single get_Height()`
  - `public Boolean get_IsZero()`
  - `public Single get_Width()`
  - `public Int32 GetHashCode()`
  - `public static Size op_Addition(Size s1, Size s2)`
  - `public static Boolean op_Equality(Size s1, Size s2)`
  - `public static Point op_Explicit(Size size)`
  - `public static Boolean op_Inequality(Size s1, Size s2)`
  - `public static Size op_Multiply(Size s1, Single value)`
  - `public static Size op_Subtraction(Size s1, Size s2)`
  - `public Void set_Height(Single value)`
  - `public Void set_Width(Single value)`
  - `public String ToString()`
  - `public Single Height { get; set; }`
  - `public Boolean IsZero { get; }`
  - `public Single Width { get; set; }`

### class `SizePropertyValue`

- Base: `Tizen.UI.AnimatablePropertyValue`
- Members:
  - `public .ctor(Single width, Single height)`
  - `public .ctor(Size size)`
  - `public Size get_Size()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(View view)`
  - `public Size Size { get; }`
  - `public PropertyValueHandle Value { get; }`

### struct `Strikethrough`

- Base: `System.ValueType`
- Members:
  - `private static .cctor()`
  - `public Color get_Color()`
  - `public Single get_Thickness()`
  - `public Void set_Color(Color value)`
  - `public Void set_Thickness(Single value)`
  - `public Color Color { get; set; }`
  - `public Single Thickness { get; set; }`

### class `SystemFontSizeManager`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public event EventHandler FontScaleChanged`
  - `public static Void add_FontScaleChanged(EventHandler value)`
  - `internal static Void Bind(IFontSizeScale element)`
  - `public static Single get_CurrentFontScale()`
  - `public static Boolean get_IsSupportSystemFontSize()`
  - `public static SystemSettingsFontSize get_SystemFontSize()`
  - `public static Single GetScale(SystemSettingsFontSize settingSize)`
  - `private static Void OnSystemFontSizeChanged(Object sender, FontSizeChangedEventArgs e)`
  - `public static Void remove_FontScaleChanged(EventHandler value)`
  - `private static Void set_CurrentFontScale(Single value)`
  - `private static Void set_IsSupportSystemFontSize(Boolean value)`
  - `internal static Void Unbind(IFontSizeScale element)`
  - `public static Void UpdateScaleTable(IDictionary<SystemSettingsFontSize, Single> table)`
  - `private static Void UpdateSubscribers()`
  - `public Single CurrentFontScale { get; set; }`
  - `public Boolean IsSupportSystemFontSize { get; set; }`
  - `public SystemSettingsFontSize SystemFontSize { get; }`

### class `TapGesture`

- Base: `Tizen.UI.Gesture`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Int32 get_NumberOfTaps()`
  - `public Point get_Position()`
  - `public Point get_ScreenPosition()`
  - `public Int32 NumberOfTaps { get; }`
  - `public Point Position { get; }`
  - `public Point ScreenPosition { get; }`

### class `TapGestureDetectedEventArgs`

- Base: `Tizen.UI.GestureDetectedEventArgs`
- Members:
  - `public .ctor(View view, TapGesture tapGesture)`
  - `public TapGesture get_Gesture()`
  - `public TapGesture Gesture { get; }`

### class `TapGestureDetector`

- Base: `Tizen.UI.GestureDetector`
- Members:
  - `public .ctor()`
  - `public .ctor(Int32 tapsRequired)`
  - `private event EventHandler<TapGestureDetectedEventArgs> _detected`
  - `public event EventHandler<TapGestureDetectedEventArgs> Detected`
  - `private Void add__detected(EventHandler<TapGestureDetectedEventArgs> value)`
  - `public Void add_Detected(EventHandler<TapGestureDetectedEventArgs> value)`
  - `public Int32 get_MaximumTapsRequired()`
  - `public Int32 get_MinimumTapsRequired()`
  - `private Void OnDetected(IntPtr view, IntPtr gesture)`
  - `private Void remove__detected(EventHandler<TapGestureDetectedEventArgs> value)`
  - `public Void remove_Detected(EventHandler<TapGestureDetectedEventArgs> value)`
  - `public Void set_MaximumTapsRequired(Int32 value)`
  - `public Void set_MinimumTapsRequired(Int32 value)`
  - `public Int32 MaximumTapsRequired { get; set; }`
  - `public Int32 MinimumTapsRequired { get; set; }`

### class `TbmSurfaceView`

- Base: `Tizen.UI.View`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `private static Geometry CreateQuadGeometry()`
  - `public Void SetSource(NativeImageSource source)`
  - `public Void SetSource(IntPtr tbmSurface)`
  - `public Void SetSource(Texture texture)`

### class `Template`1`

- Base: `System.Object`
- Members:
  - `public .ctor(Type type)`
  - `public .ctor(Func<T> loadTemplate)`
  - `public T CreateContent()`
  - `public Func<T> get_LoadTemplate()`
  - `protected Void SetupContent(T item)`
  - `public Func<T> LoadTemplate { get; }`

### enum `TextAlignment`

- Base: `System.Enum`
- Members:

### enum `TextAutoScrollStopMode`

- Base: `System.Enum`
- Members:

### class `TextBindings`1`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static BindingProperty<T, Single> get_FontSizeProperty()`
  - `public static BindingProperty<T, Color> get_TextColorProperty()`
  - `public static BindingProperty<T, String> get_TextProperty()`
  - `public BindingProperty<T, Single> FontSizeProperty { get; }`
  - `public BindingProperty<T, Color> TextColorProperty { get; }`
  - `public BindingProperty<T, String> TextProperty { get; }`

### class `TextEditor`

- Base: `Tizen.UI.InputView`
- Interfaces: `Tizen.UI.Internal.ITextEditorSignalHandler`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsAbsoluteLineHeight()`
  - `public Single get_LineHeight()`
  - `public TextOverflow get_TextOverflow()`
  - `protected Int32 GetCursorWidth()`
  - `protected Boolean GetEnableMarkup()`
  - `protected String GetFontFamily()`
  - `protected Single GetFontSize()`
  - `protected Single GetFontSizeScale()`
  - `protected TextAlignment GetHorizontalAlignment()`
  - `protected IntPtr GetInputMethodContextHandle()`
  - `protected Int32 GetMaxLength()`
  - `protected String GetPlaceholderText()`
  - `protected Color GetPlaceholderTextColor()`
  - `protected Color GetPrimaryCursorColor()`
  - `protected Int32 GetPrimaryCursorPosition()`
  - `protected Boolean GetReadOnly()`
  - `protected String GetText()`
  - `protected Color GetTextColor()`
  - `protected TextAlignment GetVerticalAlignment()`
  - `protected Void OnAnchorClicked(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `protected Void OnMaxLengthReached(IntPtr view)`
  - `protected Void OnTextChanged(IntPtr view)`
  - `public Void set_IsAbsoluteLineHeight(Boolean value)`
  - `public Void set_LineHeight(Single value)`
  - `public Void set_TextOverflow(TextOverflow value)`
  - `protected Void SetCursorWidth(Int32 value)`
  - `protected Void SetEnableMarkup(Boolean value)`
  - `protected Void SetFontFamily(String value)`
  - `protected Void SetFontSize(Single value)`
  - `protected Void SetFontSizeScale(Single value)`
  - `protected Void SetHorizontalAlignment(TextAlignment value)`
  - `public Void SetInputMethodSetting(InputMethodSetting inputMethod)`
  - `protected Void SetMaxLength(Int32 value)`
  - `protected Void SetPlaceholderText(String value)`
  - `protected Void SetPlaceholderTextColor(Color value)`
  - `protected Void SetPrimaryCursorColor(Color value)`
  - `protected Void SetPrimaryCursorPosition(Int32 value)`
  - `protected Void SetReadOnly(Boolean value)`
  - `protected Void SetText(String value)`
  - `protected Void SetTextColor(Color value)`
  - `protected Void SetVerticalAlignment(TextAlignment value)`
  - `private Void Tizen.UI.Internal.ITextEditorSignalHandler.OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `private Void Tizen.UI.Internal.ITextEditorSignalHandler.OnMaxLengthReachedSignal(IntPtr view)`
  - `private Void Tizen.UI.Internal.ITextEditorSignalHandler.OnTextChangedSignal(IntPtr view)`
  - `private Void UpdateLineHeight()`
  - `public Boolean IsAbsoluteLineHeight { get; set; }`
  - `public Single LineHeight { get; set; }`
  - `public TextOverflow TextOverflow { get; set; }`

### class `TextEditorExtensions`

- Base: `System.Object`
- Members:
  - `public static TView IsAbsoluteLineHeight(TView view, Boolean isAbsoluteLineHeight)`
  - `public static TView LineHeight(TView view, Single height)`

### class `TextExtensions`

- Base: `System.Object`
- Members:
  - `public static TView FontFamily(TView view, String fontFamily)`
  - `public static TView FontSize(TView view, Single fontSize)`
  - `public static TView HorizontalAlignment(TView view, TextAlignment alignment)`
  - `public static TView MultiLine(TView view, Boolean multiLine)`
  - `public static TView Text(TView view, String text)`
  - `public static TView TextCenter(TView view)`
  - `public static TView TextCenterHorizontal(TView view)`
  - `public static TView TextCenterVertical(TView view)`
  - `public static TView TextColor(TView view, Color textColor)`
  - `public static TView TextColorFromHex(TView view, String textColor)`
  - `public static TView TextEnd(TView view)`
  - `public static TView TextEndHorizontal(TView view)`
  - `public static TView TextEndVertical(TView view)`
  - `public static TView TextOverflow(TView view, TextOverflow textOverflow)`
  - `public static T TextPadding(T view, Thickness padding)`
  - `public static T TextPadding(T view, Single left, Single top, Single right, Single bottom)`
  - `public static T TextPadding(T view, Single uniformSize)`
  - `public static TView TextStart(TView view)`
  - `public static TView TextStartHorizontal(TView view)`
  - `public static TView TextStartVertical(TView view)`
  - `public static TView VerticalAlignment(TView view, TextAlignment alignment)`

### class `TextField`

- Base: `Tizen.UI.InputView`
- Interfaces: `Tizen.UI.Internal.ITextFieldSignalHandler`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsPassword()`
  - `public TextOverflow get_TextOverflow()`
  - `protected Int32 GetCursorWidth()`
  - `protected Boolean GetEnableMarkup()`
  - `protected String GetFontFamily()`
  - `protected Single GetFontSize()`
  - `protected Single GetFontSizeScale()`
  - `protected TextAlignment GetHorizontalAlignment()`
  - `protected IntPtr GetInputMethodContextHandle()`
  - `protected Int32 GetMaxLength()`
  - `protected String GetPlaceholderText()`
  - `protected Color GetPlaceholderTextColor()`
  - `protected Color GetPrimaryCursorColor()`
  - `protected Int32 GetPrimaryCursorPosition()`
  - `protected Boolean GetReadOnly()`
  - `protected String GetText()`
  - `protected Color GetTextColor()`
  - `protected TextAlignment GetVerticalAlignment()`
  - `protected Void OnAnchorClicked(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `protected Void OnMaxLengthReached(IntPtr view)`
  - `protected Void OnTextChanged(IntPtr view)`
  - `public Void set_IsPassword(Boolean value)`
  - `public Void set_TextOverflow(TextOverflow value)`
  - `protected Void SetCursorWidth(Int32 value)`
  - `protected Void SetEnableMarkup(Boolean value)`
  - `protected Void SetFontFamily(String value)`
  - `protected Void SetFontSize(Single value)`
  - `protected Void SetFontSizeScale(Single value)`
  - `protected Void SetHiddenInputSetting(HiddenInputSetting setting)`
  - `protected Void SetHorizontalAlignment(TextAlignment value)`
  - `public Void SetInputMethodSetting(InputMethodSetting setting)`
  - `protected Void SetMaxLength(Int32 value)`
  - `protected Void SetPlaceholderText(String value)`
  - `protected Void SetPlaceholderTextColor(Color value)`
  - `protected Void SetPrimaryCursorColor(Color value)`
  - `protected Void SetPrimaryCursorPosition(Int32 value)`
  - `protected Void SetReadOnly(Boolean value)`
  - `protected Void SetText(String value)`
  - `protected Void SetTextColor(Color value)`
  - `protected Void SetVerticalAlignment(TextAlignment value)`
  - `private Void Tizen.UI.Internal.ITextFieldSignalHandler.OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `private Void Tizen.UI.Internal.ITextFieldSignalHandler.OnMaxLengthReachedSignal(IntPtr view)`
  - `private Void Tizen.UI.Internal.ITextFieldSignalHandler.OnTextChangedSignal(IntPtr view)`
  - `public Boolean IsPassword { get; set; }`
  - `public TextOverflow TextOverflow { get; set; }`

### class `TextFieldExtensions`

- Base: `System.Object`
- Members:
  - `public static TView IsPassword(TView view, Boolean isPassword)`
  - `public static TView UnitFontSize(TView view, Single unitFontSize)`

### enum `TextOverflow`

- Base: `System.Enum`
- Members:

### struct `TextShadow`

- Base: `System.ValueType`
- Members:

### class `TextView`

- Base: `Tizen.UI.View`
- Interfaces: `Tizen.UI.IFontSizeScale`, `Tizen.UI.Internal.ILabelSignalHandler`, `Tizen.UI.IText`, `Tizen.UI.ITextAlignment`, `Tizen.UI.ITextFormatting`, `Tizen.UI.ITextPadding`
- Members:
  - `public .ctor()`
  - `private event EventHandler<AnchorClickedEventArgs> _anchorClicked`
  - `private event EventHandler _fontSizeAdjusted`
  - `public event EventHandler<AnchorClickedEventArgs> AnchorClicked`
  - `public event EventHandler FontSizeAdjusted`
  - `private Void add__anchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `private Void add__fontSizeAdjusted(EventHandler value)`
  - `public Void add_AnchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `public Void add_FontSizeAdjusted(EventHandler value)`
  - `public Void ClearOutline()`
  - `public Void ClearTextShadow()`
  - `public Void ClearUnderline()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Single get_AutoAdjustedFontSize()`
  - `private Boolean get_EnableAutoScroll()`
  - `public String get_FontFamily()`
  - `public Single get_FontSize()`
  - `public TextAlignment get_HorizontalAlignment()`
  - `public Boolean get_IsAbsoluteLineHeight()`
  - `public Boolean get_IsMarkupEnabled()`
  - `public Boolean get_IsMultiline()`
  - `public Boolean get_IsTextCutout()`
  - `public Boolean get_IsTextScrolling()`
  - `public LineBreakMode get_LineBreakMode()`
  - `public Int32 get_LineCount()`
  - `public Single get_LineHeight()`
  - `public Single get_ScaledFontSize()`
  - `public Boolean get_SystemFontScaleEnabled()`
  - `public String get_Text()`
  - `public Color get_TextColor()`
  - `public TextOverflow get_TextOverflow()`
  - `public TextAlignment get_VerticalAlignment()`
  - `public Single GetHeightForWidth(Single width)`
  - `public Size Measure(Single availableWidth, Single availableHeight)`
  - `private Void OnAnchorClicked(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `protected Void OnMeasureInvalidatedOverride()`
  - `private Void OnTextFitChanged(IntPtr arg)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void remove__anchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `private Void remove__fontSizeAdjusted(EventHandler value)`
  - `public Void remove_AnchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `public Void remove_FontSizeAdjusted(EventHandler value)`
  - `protected Void SendMeasureInvalidatedIfNeed()`
  - `private Void set_AutoScrollGap(Single value)`
  - `private Void set_AutoScrollLoopCount(Int32 value)`
  - `private Void set_AutoScrollLoopDelay(Single value)`
  - `private Void set_AutoScrollSpeed(Int32 value)`
  - `private Void set_AutoScrollStopMode(TextAutoScrollStopMode value)`
  - `private Void set_EnableAutoScroll(Boolean value)`
  - `public Void set_FontFamily(String value)`
  - `public Void set_FontSize(Single value)`
  - `public Void set_HorizontalAlignment(TextAlignment value)`
  - `public Void set_IsAbsoluteLineHeight(Boolean value)`
  - `public Void set_IsMarkupEnabled(Boolean value)`
  - `public Void set_IsMultiline(Boolean value)`
  - `public Void set_IsTextCutout(Boolean value)`
  - `public Void set_LineBreakMode(LineBreakMode value)`
  - `public Void set_LineHeight(Single value)`
  - `public Void set_SystemFontScaleEnabled(Boolean value)`
  - `public Void set_Text(String value)`
  - `public Void set_TextColor(Color value)`
  - `public Void set_TextOverflow(TextOverflow value)`
  - `public Void set_VerticalAlignment(TextAlignment value)`
  - `public Void SetAutoSize(AutoFontSize size)`
  - `public Void SetMarqueeConfig(Int32 loopCount, Int32 speed, Single gap, Single delay)`
  - `public Void SetOutline(Outline outline)`
  - `public Void SetTextPadding(Thickness thickness)`
  - `public Void SetTextShadow(TextShadow shadow)`
  - `public Void SetUnderline(Underline underline)`
  - `public Void StartTextScroll(Int32 loopCount, Int32 speed, Single gap, Single delay)`
  - `public Void StopTextScroll(Boolean immediate)`
  - `private Single Tizen.UI.IFontSizeScale.get_FontSizeScale()`
  - `private Void Tizen.UI.IFontSizeScale.set_FontSizeScale(Single value)`
  - `private Void Tizen.UI.Internal.ILabelSignalHandler.OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `private Void Tizen.UI.Internal.ILabelSignalHandler.OnTextFitChangedSignal(IntPtr arg)`
  - `private Void UpdateLineHeight()`
  - `public Single AutoAdjustedFontSize { get; }`
  - `private Single AutoScrollGap { set; }`
  - `private Int32 AutoScrollLoopCount { set; }`
  - `private Single AutoScrollLoopDelay { set; }`
  - `private Int32 AutoScrollSpeed { set; }`
  - `private TextAutoScrollStopMode AutoScrollStopMode { set; }`
  - `private Boolean EnableAutoScroll { get; set; }`
  - `public String FontFamily { get; set; }`
  - `public Single FontSize { get; set; }`
  - `public TextAlignment HorizontalAlignment { get; set; }`
  - `public Boolean IsAbsoluteLineHeight { get; set; }`
  - `public Boolean IsMarkupEnabled { get; set; }`
  - `public Boolean IsMultiline { get; set; }`
  - `public Boolean IsTextCutout { get; set; }`
  - `public Boolean IsTextScrolling { get; }`
  - `public LineBreakMode LineBreakMode { get; set; }`
  - `public Int32 LineCount { get; }`
  - `public Single LineHeight { get; set; }`
  - `public Single ScaledFontSize { get; }`
  - `public Boolean SystemFontScaleEnabled { get; set; }`
  - `public String Text { get; set; }`
  - `public Color TextColor { get; set; }`
  - `public TextOverflow TextOverflow { get; set; }`
  - `private Single Tizen.UI.IFontSizeScale.FontSizeScale { get; set; }`
  - `public TextAlignment VerticalAlignment { get; set; }`

### class `TextViewExtensions`

- Base: `System.Object`
- Members:
  - `public static TView EllipsisPosition(TView view, TextOverflow position)`
  - `public static TView IsAbsoluteLineHeight(TView view, Boolean isAbsoluteLineHeight)`
  - `public static TView IsMarkupEnabled(TView view, Boolean enable)`
  - `public static TView LineBreakMode(TView view, LineBreakMode mode)`
  - `public static TView LineHeight(TView view, Single height)`
  - `public static TextView UnitFontSize(TextView textView, Single unitFontSize)`

### struct `Thickness`

- Base: `System.ValueType`
- Members:
  - `private static .cctor()`
  - `public .ctor(Single uniformSize)`
  - `public .ctor(Single horizontalSize, Single verticalSize)`
  - `public .ctor(Single left, Single top, Single right, Single bottom)`
  - `public Void Deconstruct(Single& left, Single& top, Single& right, Single& bottom)`
  - `private Boolean Equals(Thickness other)`
  - `public Boolean Equals(Object obj)`
  - `public Single get_Bottom()`
  - `public Single get_HorizontalThickness()`
  - `public Boolean get_IsEmpty()`
  - `public Boolean get_IsNaN()`
  - `public Single get_Left()`
  - `public Single get_Right()`
  - `public Single get_Top()`
  - `public Single get_VerticalThickness()`
  - `public Int32 GetHashCode()`
  - `public static Thickness op_Addition(Thickness left, Single addend)`
  - `public static Thickness op_Addition(Thickness left, Thickness right)`
  - `public static Boolean op_Equality(Thickness left, Thickness right)`
  - `public static Thickness op_Implicit(Size size)`
  - `public static Thickness op_Implicit(Single uniformSize)`
  - `public static Boolean op_Inequality(Thickness left, Thickness right)`
  - `public static Thickness op_Subtraction(Thickness left, Single addend)`
  - `public Void set_Bottom(Single value)`
  - `public Void set_Left(Single value)`
  - `public Void set_Right(Single value)`
  - `public Void set_Top(Single value)`
  - `public Single Bottom { get; set; }`
  - `public Single HorizontalThickness { get; }`
  - `public Boolean IsEmpty { get; }`
  - `public Boolean IsNaN { get; }`
  - `public Single Left { get; set; }`
  - `public Single Right { get; set; }`
  - `public Single Top { get; set; }`
  - `public Single VerticalThickness { get; }`

### class `Timer`

- Base: `Tizen.UI.NObject`
- Members:
  - `private static .cctor()`
  - `public .ctor(UInt32 millisec)`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Dispose(Boolean disposing)`
  - `public UInt32 get_Interval()`
  - `public Boolean get_IsRunning()`
  - `public Func<Boolean> get_TickHandler()`
  - `private Boolean OnTick()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public static Timer Repeat(UInt32 interval, Func<Boolean> handler)`
  - `public Void Reset()`
  - `public Void set_Interval(UInt32 value)`
  - `public Void set_TickHandler(Func<Boolean> value)`
  - `public Void Start()`
  - `public Void Stop()`
  - `public static Timer Timeout(UInt32 timeout, Action action)`
  - `public UInt32 Interval { get; set; }`
  - `public Boolean IsRunning { get; }`
  - `public Func<Boolean> TickHandler { get; set; }`

### class `TokenExtensions`

- Base: `System.Object`
- Members:
  - `public static Color ToRawColor(Color color)`

### class `TokenManager`

- Base: `System.Object`
- Members:
  - `public static ITokenTable<Color> get_ColorTable()`
  - `public static Void SetTable(TokenType type, ITokenTable<T> table)`
  - `public ITokenTable<Color> ColorTable { get; }`

### class `TokenPropertyChangedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public String get_Name()`
  - `public View get_View()`
  - `public Void set_Name(String value)`
  - `public Void set_View(View value)`
  - `public String Name { get; set; }`
  - `public View View { get; set; }`

### class `TokenPropertyNames`

- Base: `System.Object`
- Members:
  - `private static .cctor()`

### enum `TokenType`

- Base: `System.Enum`
- Members:

### class `Touch`

- Base: `System.Object`
- Members:
  - `internal .ctor(IntPtr handle, Int32 index)`
  - `public Int32 get_DeviceId()`
  - `public Point get_EllipseRadius()`
  - `internal NObject get_HitActor()`
  - `public View get_HitView()`
  - `public MouseButton get_MouseButton()`
  - `public Point get_Position()`
  - `public Single get_Pressure()`
  - `public Single get_Radius()`
  - `public Point get_ScreenPosition()`
  - `public TouchState get_State()`
  - `public Int32 DeviceId { get; }`
  - `public Point EllipseRadius { get; }`
  - `internal NObject HitActor { get; }`
  - `public View HitView { get; }`
  - `public MouseButton MouseButton { get; }`
  - `public Point Position { get; }`
  - `public Single Pressure { get; }`
  - `public Single Radius { get; }`
  - `public Point ScreenPosition { get; }`
  - `public TouchState State { get; }`

### class `TouchEvent`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownshandle)`
  - `public Int32 get_Count()`
  - `public Touch get_Item(Int32 i)`
  - `public UInt32 get_Time()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Int32 Count { get; }`
  - `public Touch Item { get; }`
  - `public UInt32 Time { get; }`

### class `TouchEventArgs`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Boolean get_Handled()`
  - `public TouchEvent get_Touch()`
  - `public Void set_Handled(Boolean value)`
  - `public Void set_Touch(TouchEvent value)`
  - `public Boolean Handled { get; set; }`
  - `public TouchEvent Touch { get; set; }`

### class `TouchPoint`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(Int32 deviceId, TouchState state, Single screenX, Single screenY)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Int32 get_DeviceId()`
  - `public View get_HitView()`
  - `public Point get_Position()`
  - `public Point get_ScreenPosition()`
  - `public TouchState get_State()`
  - `public Void set_DeviceId(Int32 value)`
  - `public Void set_HitView(View value)`
  - `public Void set_Position(Point value)`
  - `public Void set_ScreenPosition(Point value)`
  - `public Void set_State(TouchState value)`
  - `public Int32 DeviceId { get; set; }`
  - `public View HitView { get; set; }`
  - `public Point Position { get; set; }`
  - `public Point ScreenPosition { get; set; }`
  - `public TouchState State { get; set; }`

### enum `TouchState`

- Base: `System.Enum`
- Members:

### class `TwoWayBindingProperty`2`

- Base: `Tizen.UI.BindingProperty`2<TView,TValue>`
- Members:
  - `public .ctor()`
  - `public Action<TView, Action> get_AddObserver()`
  - `public Func<TView, TValue> get_Getter()`
  - `public Action<TView, Action> get_RemoveObserver()`
  - `public Void set_AddObserver(Action<TView, Action> value)`
  - `public Void set_Getter(Func<TView, TValue> value)`
  - `public Void set_RemoveObserver(Action<TView, Action> value)`
  - `public Action<TView, Action> AddObserver { get; set; }`
  - `public Func<TView, TValue> Getter { get; set; }`
  - `public Action<TView, Action> RemoveObserver { get; set; }`

### class `UIApplication`

- Base: `Tizen.Applications.CoreApplication`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public .ctor(WindowMode mode)`
  - `public .ctor(Rect windowBounds)`
  - `public .ctor(WindowMode mode, Rect windowBounds)`
  - `public .ctor(WindowMode mode, CoreTask coreTask)`
  - `public .ctor(WindowMode mode, Rect windowBounds, CoreTask coreTask)`
  - `protected .ctor(ICoreBackend backend)`
  - `public event EventHandler Paused`
  - `public event EventHandler Resumed`
  - `public Void add_Paused(EventHandler value)`
  - `public Void add_Resumed(EventHandler value)`
  - `protected Void Dispose(Boolean disposing)`
  - `private UICoreBackend get__coreBackend()`
  - `public static UIApplication get_Current()`
  - `public Boolean get_IsUIThread()`
  - `public static IReadOnlyList<Window> get_Windows()`
  - `public NObject GetDaliNativeApp()`
  - `protected Void OnPause()`
  - `protected Void OnPreCreate()`
  - `protected Void OnResume()`
  - `public Void PostToUI(Action action)`
  - `public Void remove_Paused(EventHandler value)`
  - `public Void remove_Resumed(EventHandler value)`
  - `public Void Run(String[] args)`
  - `private static Void set_Current(UIApplication value)`
  - `public static Void UseCompatibilityTouchMode()`
  - `public static Void UseTouchGestureIntegration()`
  - `private UICoreBackend _coreBackend { get; }`
  - `public UIApplication Current { get; set; }`
  - `public Boolean IsUIThread { get; }`
  - `public IReadOnlyList<Window> Windows { get; }`

### class `UISettings`

- Base: `System.Object`
- Members:
  - `private static StyleManagerHandle get_StyleManagerHandle()`
  - `public static Void SetBrokenImageUrl(String url)`
  - `public static Void SetBrokenImageUrl(BrokenImageType type, String url)`
  - `public static Void SetDoubleTapTimeout(Int32 ms)`
  - `public static Void SetKeyRepeat(Single rate, Single delay)`
  - `public static Void SetLongPressMinimumHoldingTime(Int32 ms)`
  - `public static Void SetPinchMinimumDistance(Single distance)`
  - `public static Void SetTapRecognizerTime(Int32 ms)`
  - `public static Void UsePreCompiledShader()`
  - `private StyleManagerHandle StyleManagerHandle { get; }`

### struct `Underline`

- Base: `System.ValueType`
- Members:
  - `public Nullable<Color> get_Color()`
  - `public Nullable<Single> get_DashGap()`
  - `public Nullable<Single> get_DashWidth()`
  - `public Nullable<UnderlineStyle> get_Style()`
  - `public Nullable<Single> get_Thickness()`
  - `public Void set_Color(Nullable<Color> value)`
  - `public Void set_DashGap(Nullable<Single> value)`
  - `public Void set_DashWidth(Nullable<Single> value)`
  - `public Void set_Style(Nullable<UnderlineStyle> value)`
  - `public Void set_Thickness(Nullable<Single> value)`
  - `public Nullable<Color> Color { get; set; }`
  - `public Nullable<Single> DashGap { get; set; }`
  - `public Nullable<Single> DashWidth { get; set; }`
  - `public Nullable<UnderlineStyle> Style { get; set; }`
  - `public Nullable<Single> Thickness { get; set; }`

### enum `UnderlineStyle`

- Base: `System.Enum`
- Members:

### struct `UnitValue`

- Base: `System.ValueType`
- Members:
  - `public .ctor(Int32 pixel)`
  - `public .ctor(Single pixel)`
  - `public Single get_Dp()`
  - `public Int32 get_Pixel()`
  - `public static UnitValue op_Addition(UnitValue a, UnitValue b)`
  - `public static UnitValue op_Addition(Single a, UnitValue b)`
  - `public static UnitValue op_Addition(UnitValue a, Single b)`
  - `public static UnitValue op_Division(UnitValue a, UnitValue b)`
  - `public static UnitValue op_Division(Single a, UnitValue b)`
  - `public static UnitValue op_Division(UnitValue a, Single b)`
  - `public static Single op_Implicit(UnitValue obj)`
  - `public static UnitValue op_Multiply(UnitValue a, UnitValue b)`
  - `public static UnitValue op_Multiply(Single a, UnitValue b)`
  - `public static UnitValue op_Multiply(UnitValue a, Single b)`
  - `public static UnitValue op_Subtraction(UnitValue a, UnitValue b)`
  - `public static UnitValue op_Subtraction(Single a, UnitValue b)`
  - `public static UnitValue op_Subtraction(UnitValue a, Single b)`
  - `public static UnitValue op_UnaryNegation(UnitValue a)`
  - `public static UnitValue op_UnaryPlus(UnitValue a)`
  - `public Single Dp { get; }`
  - `public Int32 Pixel { get; }`

### class `UrlWebViewSource`

- Base: `Tizen.UI.WebViewSource`
- Members:
  - `public .ctor()`
  - `public String get_Url()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Url(String value)`
  - `public String Url { get; set; }`

### class `VideoOverlayView`

- Base: `Tizen.UI.View`
- Interfaces: `Tizen.UI.Internal.IResourceReadySignalHandler`
- Members:
  - `public .ctor()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Rect get_DisplayArea()`
  - `private Boolean IsUpdateAvailable()`
  - `private Void OnAttachedToWindow(Object sender, EventArgs e)`
  - `private Void OnDetachedFromWindow(Object sender, EventArgs e)`
  - `private Void OnResourceReady(IntPtr view)`
  - `private Void OnUpdateDisplayArea()`
  - `private Void OnVisibilityChanged(Object sender, VisibilityChangedEventArgs e)`
  - `private Void OnWindowResized(Object sender, EventArgs e)`
  - `private Void SetBlendMode()`
  - `private Void SetOrientation(WindowOrientation orientation)`
  - `public Void SetPlayer(Player player)`
  - `private Void Tizen.UI.Internal.IResourceReadySignalHandler.OnResourceReadySignal(IntPtr view)`
  - `private Void UpdateGeometry(Rect displayArea)`
  - `public Rect DisplayArea { get; }`

### class `View`

- Base: `Tizen.UI.NObject`
- Interfaces: `Tizen.UI.Internal.IViewSignalHandler`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private event EventHandler _attachedToWindow`
  - `private event EventHandler _detachedFromWindow`
  - `private event EventHandler _focused`
  - `private event EventHandler<HoverEventArgs> _hoverEvent`
  - `private event EventHandler<TouchEventArgs> _interceptTouchEvent`
  - `private event EventHandler<WheelEventArgs> _interceptWheelEvent`
  - `private event EventHandler<KeyEventArgs> _keyEvent`
  - `private event EventHandler _layoutDirectionChanged`
  - `private event EventHandler _relayout`
  - `private event EventHandler<TouchEventArgs> _touchEvent`
  - `private event EventHandler _unfocused`
  - `private event EventHandler<VisibilityChangedEventArgs> _visibilityChanged`
  - `private event EventHandler<WheelEventArgs> _wheelEvent`
  - `public event EventHandler<AccessibilityActionReceivedEventArgs> AccessibilityActionReceived`
  - `public event EventHandler<AccessibilityDescriptionRequestedEventArgs> AccessibilityDescriptionRequested`
  - `public event EventHandler<AccessibilityHighlightChangedEventArgs> AccessibilityHighlightChanged`
  - `public event EventHandler<AccessibilityNameRequestedEventArgs> AccessibilityNameRequested`
  - `public event EventHandler AttachedToWindow`
  - `public event EventHandler DetachedFromWindow`
  - `public event EventHandler Disposing`
  - `public event EventHandler EnabledChanged`
  - `public event EventHandler Focused`
  - `public event EventHandler<HoverEventArgs> HoverEvent`
  - `public event EventHandler<TouchEventArgs> InterceptTouchEvent`
  - `public event EventHandler<WheelEventArgs> InterceptWheelEvent`
  - `public event EventHandler<KeyEventArgs> KeyEvent`
  - `public event EventHandler LayoutDirectionChanged`
  - `public event EventHandler MeasureInvalidated`
  - `public event EventHandler Relayout`
  - `public event EventHandler<TokenPropertyChangedEventArgs> TokenPropertyChanged`
  - `public event EventHandler<TouchEventArgs> TouchEvent`
  - `public event EventHandler Unfocused`
  - `public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged`
  - `public event EventHandler<WheelEventArgs> WheelEvent`
  - `internal static Boolean <OnVisibilityChanged>g__AggregatedParentVisibility|361_0(View view)`
  - `private Void add__attachedToWindow(EventHandler value)`
  - `private Void add__detachedFromWindow(EventHandler value)`
  - `private Void add__focused(EventHandler value)`
  - `private Void add__hoverEvent(EventHandler<HoverEventArgs> value)`
  - `private Void add__interceptTouchEvent(EventHandler<TouchEventArgs> value)`
  - `private Void add__interceptWheelEvent(EventHandler<WheelEventArgs> value)`
  - `private Void add__keyEvent(EventHandler<KeyEventArgs> value)`
  - `private Void add__layoutDirectionChanged(EventHandler value)`
  - `private Void add__relayout(EventHandler value)`
  - `private Void add__touchEvent(EventHandler<TouchEventArgs> value)`
  - `private Void add__unfocused(EventHandler value)`
  - `private Void add__visibilityChanged(EventHandler<VisibilityChangedEventArgs> value)`
  - `private Void add__wheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void add_AccessibilityActionReceived(EventHandler<AccessibilityActionReceivedEventArgs> value)`
  - `public Void add_AccessibilityDescriptionRequested(EventHandler<AccessibilityDescriptionRequestedEventArgs> value)`
  - `public Void add_AccessibilityHighlightChanged(EventHandler<AccessibilityHighlightChangedEventArgs> value)`
  - `public Void add_AccessibilityNameRequested(EventHandler<AccessibilityNameRequestedEventArgs> value)`
  - `public Void add_AttachedToWindow(EventHandler value)`
  - `public Void add_DetachedFromWindow(EventHandler value)`
  - `public Void add_Disposing(EventHandler value)`
  - `public Void add_EnabledChanged(EventHandler value)`
  - `public Void add_Focused(EventHandler value)`
  - `public Void add_HoverEvent(EventHandler<HoverEventArgs> value)`
  - `public Void add_InterceptTouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void add_InterceptWheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void add_KeyEvent(EventHandler<KeyEventArgs> value)`
  - `public Void add_LayoutDirectionChanged(EventHandler value)`
  - `public Void add_MeasureInvalidated(EventHandler value)`
  - `public Void add_Relayout(EventHandler value)`
  - `public static Void add_TokenPropertyChanged(EventHandler<TokenPropertyChangedEventArgs> value)`
  - `public Void add_TouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void add_Unfocused(EventHandler value)`
  - `public Void add_VisibilityChanged(EventHandler<VisibilityChangedEventArgs> value)`
  - `public Void add_WheelEvent(EventHandler<WheelEventArgs> value)`
  - `internal Void AddGestureDetector(GestureDetector detector)`
  - `public Void AddRenderNotification(RenderNotificationKey key, Action action)`
  - `public Void ClearAttached()`
  - `public Void ClearBackground()`
  - `public Void ClearInnerShadow()`
  - `public Void ClearShadow()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected Void DoAction(Int32 visualIdx, Int32 actionId)`
  - `protected Void DoAction(Int32 visualIdx, Int32 actionId, PropertyValueHandle value)`
  - `private AccessibilityData EnsureAccessibilityData()`
  - `private Void EnsureBackgroundVisual()`
  - `public static View FindViewById(UInt32 id)`
  - `public String get_AccessibilityDescription()`
  - `public Boolean get_AccessibilityHidden()`
  - `public Boolean get_AccessibilityHighlightable()`
  - `public String get_AccessibilityName()`
  - `public AccessibilityRole get_AccessibilityRole()`
  - `public String get_AccessibilityValue()`
  - `private Boolean get_AllowOnlyOwnTouch()`
  - `public String get_AutomationId()`
  - `public Color get_BackgroundColor()`
  - `public Color get_BorderlineColor()`
  - `public Single get_BorderlineOffset()`
  - `public Single get_BorderlineWidth()`
  - `public Rect get_Bounds()`
  - `private Boolean get_CaptureTouchEnabled()`
  - `public ClippingMode get_ClippingMode()`
  - `public CornerRadius get_CornerRadius()`
  - `public Single get_CurrentOpacity()`
  - `public Point get_CurrentPosition()`
  - `public Size get_CurrentScale()`
  - `public Point get_CurrentScreenPosition()`
  - `public Size get_CurrentSize()`
  - `public Single get_DesiredHeight()`
  - `public Single get_DesiredWidth()`
  - `public Boolean get_DisallowInterceptTouchEvent()`
  - `public View get_DownFocusableView()`
  - `public Boolean get_Focusable()`
  - `public Boolean get_FocusableChildren()`
  - `public Boolean get_FocusableInTouch()`
  - `protected static Boolean get_HasTokenHandler()`
  - `public Single get_Height()`
  - `public ResizePolicy get_HeightResizePolicy()`
  - `public UInt32 get_ID()`
  - `public Boolean get_InheritLayoutDirection()`
  - `public Boolean get_IsEnabled()`
  - `public Boolean get_IsOnWindow()`
  - `public Boolean get_IsVisible()`
  - `public View get_Item(String name)`
  - `public LayoutDirection get_LayoutDirection()`
  - `private Boolean get_LeaveRequired()`
  - `public View get_LeftFocusableView()`
  - `public Color get_MultipliedColor()`
  - `public String get_Name()`
  - `public Size get_NaturalSize()`
  - `public OffScreenRendering get_OffScreenRendering()`
  - `public Single get_Opacity()`
  - `public View get_Parent()`
  - `public NObject get_ParentObject()`
  - `public Point get_ParentOrigin()`
  - `public Single get_ParentOriginX()`
  - `public Single get_ParentOriginY()`
  - `public Point get_PivotPoint()`
  - `public Single get_PivotPointX()`
  - `public Single get_PivotPointY()`
  - `public Boolean get_PositionUsesPivotPoint()`
  - `public View get_RightFocusableView()`
  - `public Single get_Rotation()`
  - `public Single get_RotationX()`
  - `public Single get_RotationY()`
  - `public Single get_ScaleX()`
  - `public Single get_ScaleY()`
  - `public Point get_ScreenPosition()`
  - `public Boolean get_Sensitive()`
  - `public Thickness get_TouchEdgeInsets()`
  - `public View get_UpFocusableView()`
  - `public Single get_Width()`
  - `public ResizePolicy get_WidthResizePolicy()`
  - `public Size get_WorldScale()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public T GetAttached()`
  - `public View GetDescendant(String name)`
  - `public View GetDescendant(UInt32 id)`
  - `public Layer GetLayer()`
  - `protected Boolean HasBackgroundVisual()`
  - `public Boolean HasFocus()`
  - `public Void Lower()`
  - `public Void LowerBelow(View target)`
  - `public Void LowerToBottom()`
  - `public Size Measure(Single availableWidth, Single availableHeight)`
  - `private Void OnAggregatedVisibilityChanged(IntPtr data, Boolean visibility)`
  - `private Void OnAttached(IntPtr view)`
  - `private Void OnDetached(IntPtr view)`
  - `protected Void OnEnabled(Boolean enabled)`
  - `private Void OnFocused(IntPtr view)`
  - `protected Boolean OnHitTest(TouchEvent touch)`
  - `private Boolean OnHitTest(IntPtr view, IntPtr evt)`
  - `private Boolean OnHoverEvent(IntPtr view, IntPtr evt)`
  - `private Boolean OnInterceptTouchEvent(IntPtr view, IntPtr evt)`
  - `private Boolean OnInterceptWheelEvent(IntPtr view, IntPtr evt)`
  - `private Boolean OnKeyEvent(IntPtr view, IntPtr keyEvt)`
  - `private Void OnLayoutDirectionChanged(IntPtr data, Int32 direction)`
  - `protected Void OnMeasureInvalidatedOverride()`
  - `private Void OnRelayout(IntPtr view)`
  - `private Boolean OnTouchEvent(IntPtr view, IntPtr evt)`
  - `private Void OnUnfocused(IntPtr view)`
  - `private Void OnVisibilityChanged(IntPtr data, Boolean visibility, Int32 type)`
  - `private Boolean OnWheelEvent(IntPtr view, IntPtr evt)`
  - `private static Void PropagateDisallowInterceptTouchEvent(View parent, Boolean value)`
  - `public Void Raise()`
  - `public Void RaiseAbove(View target)`
  - `public Void RaiseToTop()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void remove__attachedToWindow(EventHandler value)`
  - `private Void remove__detachedFromWindow(EventHandler value)`
  - `private Void remove__focused(EventHandler value)`
  - `private Void remove__hoverEvent(EventHandler<HoverEventArgs> value)`
  - `private Void remove__interceptTouchEvent(EventHandler<TouchEventArgs> value)`
  - `private Void remove__interceptWheelEvent(EventHandler<WheelEventArgs> value)`
  - `private Void remove__keyEvent(EventHandler<KeyEventArgs> value)`
  - `private Void remove__layoutDirectionChanged(EventHandler value)`
  - `private Void remove__relayout(EventHandler value)`
  - `private Void remove__touchEvent(EventHandler<TouchEventArgs> value)`
  - `private Void remove__unfocused(EventHandler value)`
  - `private Void remove__visibilityChanged(EventHandler<VisibilityChangedEventArgs> value)`
  - `private Void remove__wheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void remove_AccessibilityActionReceived(EventHandler<AccessibilityActionReceivedEventArgs> value)`
  - `public Void remove_AccessibilityDescriptionRequested(EventHandler<AccessibilityDescriptionRequestedEventArgs> value)`
  - `public Void remove_AccessibilityHighlightChanged(EventHandler<AccessibilityHighlightChangedEventArgs> value)`
  - `public Void remove_AccessibilityNameRequested(EventHandler<AccessibilityNameRequestedEventArgs> value)`
  - `public Void remove_AttachedToWindow(EventHandler value)`
  - `public Void remove_DetachedFromWindow(EventHandler value)`
  - `public Void remove_Disposing(EventHandler value)`
  - `public Void remove_EnabledChanged(EventHandler value)`
  - `public Void remove_Focused(EventHandler value)`
  - `public Void remove_HoverEvent(EventHandler<HoverEventArgs> value)`
  - `public Void remove_InterceptTouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void remove_InterceptWheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void remove_KeyEvent(EventHandler<KeyEventArgs> value)`
  - `public Void remove_LayoutDirectionChanged(EventHandler value)`
  - `public Void remove_MeasureInvalidated(EventHandler value)`
  - `public Void remove_Relayout(EventHandler value)`
  - `public static Void remove_TokenPropertyChanged(EventHandler<TokenPropertyChangedEventArgs> value)`
  - `public Void remove_TouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void remove_Unfocused(EventHandler value)`
  - `public Void remove_VisibilityChanged(EventHandler<VisibilityChangedEventArgs> value)`
  - `public Void remove_WheelEvent(EventHandler<WheelEventArgs> value)`
  - `internal Void RemoveGestureDetector(GestureDetector detector)`
  - `public Void RemoveRenderNotification(RenderNotificationKey key, Action action)`
  - `public Void RemoveRenderNotification(RenderNotificationKey key)`
  - `public Void SendMeasureInvalidated()`
  - `protected static Void SendTokenPropertyChanged(TokenPropertyChangedEventArgs args)`
  - `public Void set_AccessibilityDescription(String value)`
  - `public Void set_AccessibilityHidden(Boolean value)`
  - `public Void set_AccessibilityHighlightable(Boolean value)`
  - `public Void set_AccessibilityName(String value)`
  - `public Void set_AccessibilityRole(AccessibilityRole value)`
  - `public Void set_AccessibilityValue(String value)`
  - `private Void set_AllowOnlyOwnTouch(Boolean value)`
  - `public Void set_AutomationId(String value)`
  - `public Void set_BackgroundColor(Color value)`
  - `public Void set_BorderlineColor(Color value)`
  - `public Void set_BorderlineOffset(Single value)`
  - `public Void set_BorderlineWidth(Single value)`
  - `private Void set_CaptureTouchEnabled(Boolean value)`
  - `public Void set_ClippingMode(ClippingMode value)`
  - `public Void set_CornerRadius(CornerRadius value)`
  - `public Void set_DesiredHeight(Single value)`
  - `public Void set_DesiredWidth(Single value)`
  - `public Void set_DisallowInterceptTouchEvent(Boolean value)`
  - `public Void set_DownFocusableView(View value)`
  - `public Void set_Focusable(Boolean value)`
  - `public Void set_FocusableChildren(Boolean value)`
  - `public Void set_FocusableInTouch(Boolean value)`
  - `public Void set_Height(Single value)`
  - `public Void set_HeightResizePolicy(ResizePolicy value)`
  - `public Void set_InheritLayoutDirection(Boolean value)`
  - `public Void set_IsEnabled(Boolean value)`
  - `public Void set_IsVisible(Boolean value)`
  - `public Void set_LayoutDirection(LayoutDirection value)`
  - `private Void set_LeaveRequired(Boolean value)`
  - `public Void set_LeftFocusableView(View value)`
  - `public Void set_MultipliedColor(Color value)`
  - `public Void set_Name(String value)`
  - `public Void set_OffScreenRendering(OffScreenRendering value)`
  - `public Void set_Opacity(Single value)`
  - `public Void set_ParentOrigin(Point value)`
  - `public Void set_ParentOriginX(Single value)`
  - `public Void set_ParentOriginY(Single value)`
  - `public Void set_PivotPoint(Point value)`
  - `public Void set_PivotPointX(Single value)`
  - `public Void set_PivotPointY(Single value)`
  - `public Void set_PositionUsesPivotPoint(Boolean value)`
  - `public Void set_RightFocusableView(View value)`
  - `public Void set_Rotation(Single value)`
  - `public Void set_RotationX(Single value)`
  - `public Void set_RotationY(Single value)`
  - `public Void set_ScaleX(Single value)`
  - `public Void set_ScaleY(Single value)`
  - `public Void set_Sensitive(Boolean value)`
  - `public Void set_TouchEdgeInsets(Thickness value)`
  - `public Void set_UpFocusableView(View value)`
  - `public Void set_Width(Single value)`
  - `public Void set_WidthResizePolicy(ResizePolicy value)`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public Void SetAccessibilityRelation(AccessibilityRelation relation, View view)`
  - `public Void SetAttached(T value)`
  - `public Void SetBackground(Background background)`
  - `public Void SetInnerShadow(InnerShadow shadow)`
  - `protected Void SetOffscreenRendering(OffScreenRendering mode)`
  - `public Void SetRenderEffect(RenderEffect effect)`
  - `public Void SetResizePolicyFactor(Size factor)`
  - `public Void SetShadow(Shadow shadow)`
  - `private Void Tizen.UI.Internal.IViewSignalHandler.OnAggregatedVisibilityChangedSignal(IntPtr data, Boolean visibility)`
  - `private Void Tizen.UI.Internal.IViewSignalHandler.OnFocusedSignal(IntPtr view)`
  - `private Boolean Tizen.UI.Internal.IViewSignalHandler.OnHitTestSignal(IntPtr view, IntPtr evt)`
  - `private Boolean Tizen.UI.Internal.IViewSignalHandler.OnHoverEventSignal(IntPtr view, IntPtr evt)`
  - `private Boolean Tizen.UI.Internal.IViewSignalHandler.OnInterceptTouchEventSignal(IntPtr view, IntPtr evt)`
  - `private Boolean Tizen.UI.Internal.IViewSignalHandler.OnInterceptWheelEventSignal(IntPtr view, IntPtr evt)`
  - `private Boolean Tizen.UI.Internal.IViewSignalHandler.OnKeyEventSignal(IntPtr view, IntPtr keyEvt)`
  - `private Void Tizen.UI.Internal.IViewSignalHandler.OnLayoutDirectionChangedSignal(IntPtr data, Int32 direction)`
  - `private Void Tizen.UI.Internal.IViewSignalHandler.OnOffSceneSignal(IntPtr view)`
  - `private Void Tizen.UI.Internal.IViewSignalHandler.OnRelayoutSignal(IntPtr view)`
  - `private Void Tizen.UI.Internal.IViewSignalHandler.OnSceneSignal(IntPtr view)`
  - `private Boolean Tizen.UI.Internal.IViewSignalHandler.OnTouchEventSignal(IntPtr view, IntPtr evt)`
  - `private Void Tizen.UI.Internal.IViewSignalHandler.OnUnfocusedSignal(IntPtr view)`
  - `private Void Tizen.UI.Internal.IViewSignalHandler.OnVisibilityChangedSignal(IntPtr data, Boolean visibility, Int32 type)`
  - `private Boolean Tizen.UI.Internal.IViewSignalHandler.OnWheelEventSignal(IntPtr view, IntPtr evt)`
  - `private static Void UpdateBackgroundExtra(PropertyMapHandle map, Color borderlineColor, Single borderlineWidth, Single borderlineOffset, CornerRadius cornerRadius)`
  - `protected Void UpdateBackgroundProperty(String propertyName)`
  - `protected Void UpdateBorderProperty()`
  - `protected Void UpdateCornerRadiusProperty(CornerRadius cornerRadius)`
  - `private Void UpdateRotation()`
  - `public String AccessibilityDescription { get; set; }`
  - `public Boolean AccessibilityHidden { get; set; }`
  - `public Boolean AccessibilityHighlightable { get; set; }`
  - `public String AccessibilityName { get; set; }`
  - `public AccessibilityRole AccessibilityRole { get; set; }`
  - `public String AccessibilityValue { get; set; }`
  - `private Boolean AllowOnlyOwnTouch { get; set; }`
  - `public String AutomationId { get; set; }`
  - `public Color BackgroundColor { get; set; }`
  - `public Color BorderlineColor { get; set; }`
  - `public Single BorderlineOffset { get; set; }`
  - `public Single BorderlineWidth { get; set; }`
  - `public Rect Bounds { get; }`
  - `private Boolean CaptureTouchEnabled { get; set; }`
  - `public ClippingMode ClippingMode { get; set; }`
  - `public CornerRadius CornerRadius { get; set; }`
  - `public Single CurrentOpacity { get; }`
  - `public Point CurrentPosition { get; }`
  - `public Size CurrentScale { get; }`
  - `public Point CurrentScreenPosition { get; }`
  - `public Size CurrentSize { get; }`
  - `public Single DesiredHeight { get; set; }`
  - `public Single DesiredWidth { get; set; }`
  - `public Boolean DisallowInterceptTouchEvent { get; set; }`
  - `public View DownFocusableView { get; set; }`
  - `public Boolean Focusable { get; set; }`
  - `public Boolean FocusableChildren { get; set; }`
  - `public Boolean FocusableInTouch { get; set; }`
  - `protected Boolean HasTokenHandler { get; }`
  - `public Single Height { get; set; }`
  - `public ResizePolicy HeightResizePolicy { get; set; }`
  - `public UInt32 ID { get; }`
  - `public Boolean InheritLayoutDirection { get; set; }`
  - `public Boolean IsEnabled { get; set; }`
  - `public Boolean IsOnWindow { get; }`
  - `public Boolean IsVisible { get; set; }`
  - `public View Item { get; }`
  - `public LayoutDirection LayoutDirection { get; set; }`
  - `private Boolean LeaveRequired { get; set; }`
  - `public View LeftFocusableView { get; set; }`
  - `public Color MultipliedColor { get; set; }`
  - `public String Name { get; set; }`
  - `public Size NaturalSize { get; }`
  - `public OffScreenRendering OffScreenRendering { get; set; }`
  - `public Single Opacity { get; set; }`
  - `public View Parent { get; }`
  - `public NObject ParentObject { get; }`
  - `public Point ParentOrigin { get; set; }`
  - `public Single ParentOriginX { get; set; }`
  - `public Single ParentOriginY { get; set; }`
  - `public Point PivotPoint { get; set; }`
  - `public Single PivotPointX { get; set; }`
  - `public Single PivotPointY { get; set; }`
  - `public Boolean PositionUsesPivotPoint { get; set; }`
  - `public View RightFocusableView { get; set; }`
  - `public Single Rotation { get; set; }`
  - `public Single RotationX { get; set; }`
  - `public Single RotationY { get; set; }`
  - `public Single ScaleX { get; set; }`
  - `public Single ScaleY { get; set; }`
  - `public Point ScreenPosition { get; }`
  - `public Boolean Sensitive { get; set; }`
  - `public Thickness TouchEdgeInsets { get; set; }`
  - `public View UpFocusableView { get; set; }`
  - `public Single Width { get; set; }`
  - `public ResizePolicy WidthResizePolicy { get; set; }`
  - `public Size WorldScale { get; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`

### class `ViewBindings`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static BindingProperty<View, Color> get_BackgroundColorProperty()`
  - `public static BindingProperty<View, Single> get_HeightProperty()`
  - `public static TwoWayBindingProperty<View, Boolean> get_IsEnabledProperty()`
  - `public static BindingProperty<View, Single> get_WidthProperty()`
  - `public static BindingProperty<View, Single> get_XProperty()`
  - `public static BindingProperty<View, Single> get_YProperty()`
  - `public BindingProperty<View, Color> BackgroundColorProperty { get; }`
  - `public BindingProperty<View, Single> HeightProperty { get; }`
  - `public TwoWayBindingProperty<View, Boolean> IsEnabledProperty { get; }`
  - `public BindingProperty<View, Single> WidthProperty { get; }`
  - `public BindingProperty<View, Single> XProperty { get; }`
  - `public BindingProperty<View, Single> YProperty { get; }`

### class `ViewExtensions`

- Base: `System.Object`
- Members:
  - `public static T Attach(T view, U value)`
  - `public static T Background(T view, Background background)`
  - `public static TView BackgroundColor(TView view, Color color)`
  - `public static TView BackgroundColorFromHex(TView view, String color)`
  - `public static TView BorderlineColor(TView view, Color color)`
  - `public static TView BorderlineColorFromHex(TView view, String color)`
  - `public static TView BorderlineOffset(TView view, Single offset)`
  - `public static TView BorderlineWidth(TView view, Single width)`
  - `public static TView CornerRadius(TView view, CornerRadius cornerRadius)`
  - `public static TView DesiredHeight(TView view, Single height)`
  - `public static TView DesiredSize(TView view, Single width, Single height)`
  - `public static TView DesiredSize(TView view, Size size)`
  - `public static TView DesiredWidth(TView view, Single width)`
  - `public static TView Focusable(TView view, Boolean focusable)`
  - `public static TView FocusableChildren(TView view, Boolean focusableChildren)`
  - `public static TView FocusableInTouch(TView view, Boolean focusableInTouch)`
  - `public static Boolean IsDescendantOf(View view, View parent)`
  - `public static T Name(T view, String name)`
  - `public static Size NaturalUnitSize(View view)`
  - `public static Point Position(View view)`
  - `public static Void Position(View view, Point point)`
  - `public static Size Scale(View view)`
  - `public static T Scale(T view, Size scale)`
  - `public static T Scale(T view, Single scaleX, Single scaleY)`
  - `public static T ScaleX(T view, Single scaleX)`
  - `public static T ScaleY(T view, Single scaleY)`
  - `public static Rect ScreenScaledBounds(View view)`
  - `public static TView Self(TView view, TView& variable)`
  - `public static TView Self(TView view, Action<TView> action)`
  - `public static T Shadow(T view, Shadow shadow)`
  - `public static Size Size(View view)`
  - `public static Void Size(View view, Size size)`
  - `public static T UnitHeight(T view, Single unitHeight)`
  - `public static T UnitPosition(T view, Point pos)`
  - `public static Point UnitPosition(View view)`
  - `public static T UnitSize(T view, Size unitSize)`
  - `public static T UnitSize(T view, Single width, Single height)`
  - `public static Size UnitSize(View view)`
  - `public static T UnitWidth(T view, Single unitWidth)`
  - `public static T UnitX(T view, Single unitX)`
  - `public static Single UnitX(View view)`
  - `public static T UnitY(T view, Single unitY)`
  - `public static Single UnitY(View view)`

### class `ViewGroup`

- Base: `Tizen.UI.View`
- Interfaces: `System.Collections.Generic.ICollection`1<Tizen.UI.View>`, `System.Collections.Generic.IEnumerable`1<Tizen.UI.View>`, `System.Collections.Generic.IList`1<Tizen.UI.View>`, `System.Collections.IEnumerable`, `Tizen.UI.IParentObject`
- Members:
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void Add(View item)`
  - `public Void Clear()`
  - `public Boolean Contains(View item)`
  - `public Void CopyTo(View[] array, Int32 arrayIndex)`
  - `protected Void Dispose(Boolean disposing)`
  - `public IList<View> get_Children()`
  - `public Int32 get_Count()`
  - `public Boolean get_IsReadOnly()`
  - `public View get_Item(Int32 index)`
  - `public View GetDescendant(String name)`
  - `public IEnumerator<View> GetEnumerator()`
  - `public Int32 IndexOf(View item)`
  - `public Void Insert(Int32 index, View item)`
  - `protected Void OnChildAdded(View view, Int32 index)`
  - `protected Void OnChildAdded(View view)`
  - `protected Void OnChildRemoved(View view, Int32 index)`
  - `protected Void OnChildRemoved(View view)`
  - `private Void OnChildrenChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `public Boolean Remove(View item)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_Item(Int32 index, View value)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `private Void Tizen.UI.IParentObject.Remove(View view)`
  - `public IList<View> Children { get; }`
  - `public Int32 Count { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public View Item { get; set; }`

### class `ViewTemplate`

- Base: `Tizen.UI.Template`1<Tizen.UI.View>`
- Members:
  - `public .ctor(Type type)`
  - `public .ctor(Func<View> loadTemplate)`

### class `ViewTemplateSelector`

- Base: `Tizen.UI.ViewTemplate`
- Members:
  - `protected .ctor()`
  - `protected ViewTemplate OnSelectTemplate(Object item, View container)`
  - `public ViewTemplate SelectTemplate(Object item, View container)`
  - `private static View ThrowExceptionOnCall()`

### class `VisibilityChangedEventArgs`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Boolean get_ChangedByParent()`
  - `public Boolean get_IsAggregatedChange()`
  - `public Boolean get_Visibility()`
  - `public Void set_ChangedByParent(Boolean value)`
  - `public Void set_IsAggregatedChange(Boolean value)`
  - `public Void set_Visibility(Boolean value)`
  - `public Boolean ChangedByParent { get; set; }`
  - `public Boolean IsAggregatedChange { get; set; }`
  - `public Boolean Visibility { get; set; }`

### enum `VisualCutoutPolicy`

- Base: `System.Enum`
- Members:

### class `WeakEventManager`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Void AddEventHandler(EventHandler<TEventArgs> handler, String eventName)`
  - `public Void AddEventHandler(Delegate handler, String eventName)`
  - `private Void AddEventHandler(String eventName, Object handlerTarget, MethodInfo methodInfo)`
  - `public Void HandleEvent(Object sender, Object args, String eventName)`
  - `public Void RemoveEventHandler(EventHandler<TEventArgs> handler, String eventName)`
  - `public Void RemoveEventHandler(Delegate handler, String eventName)`
  - `private Void RemoveEventHandler(String eventName, Object handlerTarget, MemberInfo methodInfo)`

### class `WebNavigationEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor(String url)`
  - `public String get_Url()`
  - `internal Void set_Url(String value)`
  - `public String Url { get; set; }`

### class `WebView`

- Base: `Tizen.UI.View`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownshandle)`
  - `private event EventHandler<WebNavigationEventArgs> _navigationCompleted`
  - `private event EventHandler<WebNavigationEventArgs> _navigationStarted`
  - `public event EventHandler<WebNavigationEventArgs> NavigationCompleted`
  - `public event EventHandler<WebNavigationEventArgs> NavigationStarted`
  - `private Void add__navigationCompleted(EventHandler<WebNavigationEventArgs> value)`
  - `private Void add__navigationStarted(EventHandler<WebNavigationEventArgs> value)`
  - `public Void add_NavigationCompleted(EventHandler<WebNavigationEventArgs> value)`
  - `public Void add_NavigationStarted(EventHandler<WebNavigationEventArgs> value)`
  - `public Void EvaluateJavaScript(String script)`
  - `private Void EvaluateJavaScript(String script, JavaScriptMessageHandler handler)`
  - `public Task<String> EvaluateJavaScriptAsync(String script)`
  - `public Boolean get_CanGoBack()`
  - `public Boolean get_CanGoForward()`
  - `public WebViewSource get_Source()`
  - `public Void GoBack()`
  - `public Void GoForward()`
  - `private Void LoadContents(String contents, String mimeType, String encoding, String baseUrl)`
  - `private Void LoadHtmlString(String html)`
  - `private Void LoadUrl(String url)`
  - `private Void OnPageLoadFinished(String url)`
  - `private Void OnPageLoadStarted(String url)`
  - `public Void Reload()`
  - `private Void remove__navigationCompleted(EventHandler<WebNavigationEventArgs> value)`
  - `private Void remove__navigationStarted(EventHandler<WebNavigationEventArgs> value)`
  - `public Void remove_NavigationCompleted(EventHandler<WebNavigationEventArgs> value)`
  - `public Void remove_NavigationStarted(EventHandler<WebNavigationEventArgs> value)`
  - `public Void set_Source(WebViewSource value)`
  - `public Boolean CanGoBack { get; }`
  - `public Boolean CanGoForward { get; }`
  - `public WebViewSource Source { get; set; }`

### class `WebViewSource`

- Base: `System.Object`
- Members:
  - `protected .ctor()`
  - `public static WebViewSource op_Implicit(Uri url)`
  - `public static WebViewSource op_Implicit(String url)`

### enum `WheelDirection`

- Base: `System.Enum`
- Members:

### class `WheelEvent`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Int32 get_Delta()`
  - `public WheelDirection get_Direction()`
  - `public Boolean get_IsAltModifier()`
  - `public Boolean get_IsCtrlModifier()`
  - `public Boolean get_IsShiftModifier()`
  - `public UInt32 get_Modifier()`
  - `public Point get_Position()`
  - `public UInt32 get_Time()`
  - `public WheelType get_WheelType()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Int32 Delta { get; }`
  - `public WheelDirection Direction { get; }`
  - `public Boolean IsAltModifier { get; }`
  - `public Boolean IsCtrlModifier { get; }`
  - `public Boolean IsShiftModifier { get; }`
  - `public UInt32 Modifier { get; }`
  - `public Point Position { get; }`
  - `public UInt32 Time { get; }`
  - `public WheelType WheelType { get; }`

### class `WheelEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public Boolean get_Handled()`
  - `public WheelEvent get_Wheel()`
  - `public Void set_Handled(Boolean value)`
  - `public Void set_Wheel(WheelEvent value)`
  - `public Boolean Handled { get; set; }`
  - `public WheelEvent Wheel { get; set; }`

### enum `WheelType`

- Base: `System.Enum`
- Members:

### class `Window`

- Base: `Tizen.UI.NObject`
- Interfaces: `Tizen.Common.IWindowProvider`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public .ctor(String name)`
  - `public .ctor(String name, Boolean isTranslucent)`
  - `internal .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private event EventHandler<WindowFocusChangedEventArgs> _focusChanged`
  - `private event EventHandler<HoverEventArgs> _hoverEvent`
  - `private event EventHandler<TouchEventArgs> _interceptTouchEvent`
  - `private event EventHandler<WheelEventArgs> _interceptWheelEvent`
  - `private event EventHandler<KeyEventArgs> _keyEvent`
  - `private event EventHandler _resized`
  - `private event EventHandler<TouchEventArgs> _touchEvent`
  - `private event EventHandler _visibilityChanged`
  - `private event EventHandler<WheelEventArgs> _wheelEvent`
  - `public event EventHandler<WindowFocusChangedEventArgs> FocusChanged`
  - `public event EventHandler<HoverEventArgs> HoverEvent`
  - `public event EventHandler<TouchEventArgs> InterceptTouchEvent`
  - `public event EventHandler<WheelEventArgs> InterceptWheelEvent`
  - `public event EventHandler<KeyEventArgs> KeyEvent`
  - `private event EventHandler MoveCompleted`
  - `public event EventHandler Moved`
  - `private event EventHandler ResizeCompleted`
  - `public event EventHandler Resized`
  - `public event EventHandler<TouchEventArgs> TouchEvent`
  - `public event EventHandler VisibilityChanged`
  - `public event EventHandler<WheelEventArgs> WheelEvent`
  - `public Void Activate()`
  - `public Void Add(Layer layer)`
  - `private Void add__focusChanged(EventHandler<WindowFocusChangedEventArgs> value)`
  - `private Void add__hoverEvent(EventHandler<HoverEventArgs> value)`
  - `private Void add__interceptTouchEvent(EventHandler<TouchEventArgs> value)`
  - `private Void add__interceptWheelEvent(EventHandler<WheelEventArgs> value)`
  - `private Void add__keyEvent(EventHandler<KeyEventArgs> value)`
  - `private Void add__resized(EventHandler value)`
  - `private Void add__touchEvent(EventHandler<TouchEventArgs> value)`
  - `private Void add__visibilityChanged(EventHandler value)`
  - `private Void add__wheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void add_FocusChanged(EventHandler<WindowFocusChangedEventArgs> value)`
  - `public Void add_HoverEvent(EventHandler<HoverEventArgs> value)`
  - `public Void add_InterceptTouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void add_InterceptWheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void add_KeyEvent(EventHandler<KeyEventArgs> value)`
  - `private Void add_MoveCompleted(EventHandler value)`
  - `public Void add_Moved(EventHandler value)`
  - `private Void add_ResizeCompleted(EventHandler value)`
  - `public Void add_Resized(EventHandler value)`
  - `public Void add_TouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void add_VisibilityChanged(EventHandler value)`
  - `public Void add_WheelEvent(EventHandler<WheelEventArgs> value)`
  - `public UInt32 AddAuxiliaryHint(String hint, String value)`
  - `public Void AddAvailableOrientation(WindowOrientation orientation)`
  - `public Void AddFrameUpdateCallback(FrameUpdateCallback frameUpdateCallback)`
  - `private Void ApplyBorderArea(Layer layer)`
  - `private Void ApplyBorderArea()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Void FeedHover(TouchPoint touchPoint)`
  - `public Void FeedKey(KeyEvent keyEvent)`
  - `public Void FeedTouch(TouchPoint touchPoint, Int32 timeStamp)`
  - `public Thickness get_BorderArea()`
  - `public static Window get_Default()`
  - `public Layer get_DefaultLayer()`
  - `public Int32 get_DPI()`
  - `public Boolean get_IsAlwaysOnTop()`
  - `public Boolean get_IsMaximized()`
  - `public Boolean get_IsMinimized()`
  - `public Boolean get_IsModal()`
  - `public Boolean get_IsVisible()`
  - `public IReadOnlyList<Layer> get_Layers()`
  - `public String get_Title()`
  - `public WindowType get_WindowType()`
  - `public String GetAuxiliaryHint(String hint)`
  - `public Size GetClientSize()`
  - `public WindowOrientation GetCurrentOrientation()`
  - `public HoverEvent GetLastHoverEvent()`
  - `public KeyEvent GetLastKeyEvent()`
  - `public TouchEvent GetLastTouchEvent()`
  - `public IntPtr GetNativeWindowHandle()`
  - `public Point GetPosition()`
  - `private IntPtr GetRootLayerHandle()`
  - `public Size GetSize()`
  - `public static Window GetWindow(View view)`
  - `public static Window GetWindow(Layer layer)`
  - `public Boolean GrabKey(Int32 keyCode, KeyGrabMode mode)`
  - `public Void Hide()`
  - `public Void KeepRendering(Single durationSeconds)`
  - `public Void Lower()`
  - `public Void LowerLayerToBottom(Layer layer)`
  - `public Void Maximize(Boolean enable)`
  - `public Void Minimize(Boolean enable)`
  - `private Void OnFocusChanged(IntPtr handle, Boolean focused)`
  - `private Boolean OnHoverEvent(IntPtr view, IntPtr evt)`
  - `private Boolean OnInterceptTouchEvent(IntPtr view, IntPtr evt)`
  - `private Boolean OnInterceptWheelEvent(IntPtr view, IntPtr evt)`
  - `private Void OnKeyEvent(IntPtr handle)`
  - `private Void OnMoveCompleted(IntPtr data, IntPtr position)`
  - `private Void OnMoved(IntPtr data, IntPtr position)`
  - `private Void OnResize(IntPtr window, IntPtr windowSize)`
  - `private Void OnResizeCompleted(IntPtr window, IntPtr windowSize)`
  - `private Boolean OnTouchEvent(IntPtr view, IntPtr evt)`
  - `private Void OnVisibilityChanged(IntPtr data, Boolean visibility)`
  - `private Boolean OnWheelEvent(IntPtr view, IntPtr evt)`
  - `private Void OnWindowResized(Object sender, EventArgs e)`
  - `public Void Raise()`
  - `public Void RaiseLayerToTop(Layer layer)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void Remove(Layer layer)`
  - `private Void remove__focusChanged(EventHandler<WindowFocusChangedEventArgs> value)`
  - `private Void remove__hoverEvent(EventHandler<HoverEventArgs> value)`
  - `private Void remove__interceptTouchEvent(EventHandler<TouchEventArgs> value)`
  - `private Void remove__interceptWheelEvent(EventHandler<WheelEventArgs> value)`
  - `private Void remove__keyEvent(EventHandler<KeyEventArgs> value)`
  - `private Void remove__resized(EventHandler value)`
  - `private Void remove__touchEvent(EventHandler<TouchEventArgs> value)`
  - `private Void remove__visibilityChanged(EventHandler value)`
  - `private Void remove__wheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void remove_FocusChanged(EventHandler<WindowFocusChangedEventArgs> value)`
  - `public Void remove_HoverEvent(EventHandler<HoverEventArgs> value)`
  - `public Void remove_InterceptTouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void remove_InterceptWheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void remove_KeyEvent(EventHandler<KeyEventArgs> value)`
  - `private Void remove_MoveCompleted(EventHandler value)`
  - `public Void remove_Moved(EventHandler value)`
  - `private Void remove_ResizeCompleted(EventHandler value)`
  - `public Void remove_Resized(EventHandler value)`
  - `public Void remove_TouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void remove_VisibilityChanged(EventHandler value)`
  - `public Void remove_WheelEvent(EventHandler<WheelEventArgs> value)`
  - `public Void RemoveAvailableOrientation(WindowOrientation orientation)`
  - `public Void RemoveFrameUpdateCallback(FrameUpdateCallback frameUpdateCallbck)`
  - `private Void ResetBorderArea()`
  - `public Void set_BorderArea(Thickness value)`
  - `private static Void set_Default(Window value)`
  - `public Void set_IsAlwaysOnTop(Boolean value)`
  - `public Void set_IsModal(Boolean value)`
  - `public Void set_Title(String value)`
  - `public Void set_WindowType(WindowType value)`
  - `public Void SetBackgroundColor(Color color)`
  - `public Void SetBlur(WindowBlur blurInfo)`
  - `internal static Void SetDefaultWindow(IntPtr handle)`
  - `public Void SetFocusSkip(Boolean skip)`
  - `public Void SetInputRegion(Rect region)`
  - `public Void SetMaximumSize(Int32 width, Int32 height)`
  - `public Void SetMinimumSize(Int32 width, Int32 height)`
  - `public Void SetPosition(Point position)`
  - `public Void SetPositionSize(Int32 x, Int32 y, Int32 width, Int32 height)`
  - `public Void SetPreferredOrientation(WindowOrientation orientation)`
  - `public Void SetSize(UInt16 width, UInt16 height)`
  - `public Void SetSize(Size size)`
  - `public Void SetTransparency(Boolean transparent)`
  - `public Void SetWindowResizePolicy(WindowResizePolicy policy)`
  - `public Void Show()`
  - `public Task StartMoveRequest()`
  - `public Task StartResizeRequest(WindowResizeDirection direction)`
  - `private Single Tizen.Common.IWindowProvider.get_Height()`
  - `private Int32 Tizen.Common.IWindowProvider.get_Rotation()`
  - `private Single Tizen.Common.IWindowProvider.get_Width()`
  - `private IntPtr Tizen.Common.IWindowProvider.get_WindowHandle()`
  - `private Single Tizen.Common.IWindowProvider.get_X()`
  - `private Single Tizen.Common.IWindowProvider.get_Y()`
  - `public Boolean UngrabKey(Int32 keyCode)`
  - `public Thickness BorderArea { get; set; }`
  - `public Window Default { get; set; }`
  - `public Layer DefaultLayer { get; }`
  - `public Int32 DPI { get; }`
  - `public Boolean IsAlwaysOnTop { get; set; }`
  - `public Boolean IsMaximized { get; }`
  - `public Boolean IsMinimized { get; }`
  - `public Boolean IsModal { get; set; }`
  - `public Boolean IsVisible { get; }`
  - `public IReadOnlyList<Layer> Layers { get; }`
  - `public String Title { get; set; }`
  - `private Single Tizen.Common.IWindowProvider.Height { get; }`
  - `private Int32 Tizen.Common.IWindowProvider.Rotation { get; }`
  - `private Single Tizen.Common.IWindowProvider.Width { get; }`
  - `private IntPtr Tizen.Common.IWindowProvider.WindowHandle { get; }`
  - `private Single Tizen.Common.IWindowProvider.X { get; }`
  - `private Single Tizen.Common.IWindowProvider.Y { get; }`
  - `public WindowType WindowType { get; set; }`

### struct `WindowBlur`

- Base: `System.ValueType`
- Interfaces: `System.IEquatable`1<Tizen.UI.WindowBlur>`
- Members:
  - `public .ctor(WindowBlurType blurType, Int32 blurRadius, Int32 cornerRadius)`
  - `public .ctor(WindowBlurType blurType, Int32 blurRadius)`
  - `public .ctor()`
  - `public Boolean Equals(WindowBlur other)`
  - `public Boolean Equals(Object obj)`
  - `public Int32 get_BackgroundCornerRadius()`
  - `public Int32 get_BlurRadius()`
  - `public WindowBlurType get_BlurType()`
  - `public Int32 GetHashCode()`
  - `public static Boolean op_Equality(WindowBlur operand1, WindowBlur operand2)`
  - `public static Boolean op_Inequality(WindowBlur operand1, WindowBlur operand2)`
  - `public Void set_BackgroundCornerRadius(Int32 value)`
  - `public Void set_BlurRadius(Int32 value)`
  - `public Void set_BlurType(WindowBlurType value)`
  - `public Int32 BackgroundCornerRadius { get; set; }`
  - `public Int32 BlurRadius { get; set; }`
  - `public WindowBlurType BlurType { get; set; }`

### enum `WindowBlurType`

- Base: `System.Enum`
- Members:

### class `WindowFocusChangedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public Boolean get_HasFocus()`
  - `public Void set_HasFocus(Boolean value)`
  - `public Boolean HasFocus { get; set; }`

### enum `WindowMode`

- Base: `System.Enum`
- Members:

### enum `WindowOrientation`

- Base: `System.Enum`
- Members:

### enum `WindowResizeDirection`

- Base: `System.Enum`
- Members:

### enum `WindowResizePolicy`

- Base: `System.Enum`
- Members:

### enum `WindowType`

- Base: `System.Enum`
- Members:

## Namespace `Tizen.UI.Internal`

### class `AccessibilityActionSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.AccessibilityActionSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Boolean WrapperCallback(IntPtr handle)`

### class `AnimationSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrSignal`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### class `AppControlSignal`

- Base: `Tizen.UI.Internal.ApplicationSignal`1<Tizen.UI.Internal.AppControlSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `private Void WrapperCallback(IntPtr application, IntPtr voidp)`

### class `ApplicationSignal`1`

- Base: `Tizen.UI.Internal.Signal`1<T>`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### class `BatteryLowSignal`

- Base: `Tizen.UI.Internal.ApplicationSignal`1<Tizen.UI.Internal.BatteryLowSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `private Void WrapperCallback(Int32 batteryStatus)`

### enum `BlendEquationType`

- Base: `System.Enum`
- Members:

### enum `BlendFactorType`

- Base: `System.Enum`
- Members:

### enum `BlendModeType`

- Base: `System.Enum`
- Members:

### class `CaptureResultSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.CaptureResultSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr handle, Int32 state)`

### class `CreateSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrApplicationSignal`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`

### class `CustomFocusAlgorithmWrapper`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `public .ctor(IFocusAlgorithm focusAlgorithm)`
  - `public Void Dispose()`
  - `private Void Dispose(Boolean disposing)`
  - `public IFocusAlgorithm get_FocusAlgorithm()`
  - `private CustomFocusWrapperHandle get_Handle()`
  - `public static CustomFocusWrapperHandle op_Implicit(CustomFocusAlgorithmWrapper obj)`
  - `private Void set_Handle(CustomFocusWrapperHandle value)`
  - `private IntPtr WrapperGetNextFocusableView(IntPtr current, IntPtr proposed, Int32 direction, String deviceName)`
  - `public IFocusAlgorithm FocusAlgorithm { get; }`
  - `private CustomFocusWrapperHandle Handle { get; set; }`

### class `CustomFocusWrapperHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `DaliNativeApp`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(String[] args, WindowMode windowMode, Rect windowBounds, Boolean useTask)`
  - `private static IntPtr CreateNativeDaliAppHandle(String[] args, WindowMode windowMode, Rect windowBounds, Boolean useTask)`
  - `protected Void Dispose(Boolean disposing)`
  - `public Void Exit()`
  - `public Action<IntPtr> get_AppControlHandler()`
  - `public Action<Int32> get_BatteryLowHandler()`
  - `public Action get_CreatedHandler()`
  - `public static DaliNativeApp get_Current()`
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
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void RunLoop()`
  - `public Void set_AppControlHandler(Action<IntPtr> value)`
  - `public Void set_BatteryLowHandler(Action<Int32> value)`
  - `public Void set_CreatedHandler(Action value)`
  - `private static Void set_Current(DaliNativeApp value)`
  - `public Void set_DeviceOrientationChangedHandler(Action<Int32> value)`
  - `public Void set_LanguageChangedHandler(Action<String> value)`
  - `public Void set_MemoryLowHandler(Action<Int32> value)`
  - `public Void set_PausedHandler(Action value)`
  - `public Void set_RegionChangedHandler(Action<String> value)`
  - `public Void set_ResumedHandler(Action value)`
  - `public Void set_TerminatedHandler(Action value)`
  - `public Void SetApplicationLocale(String locale)`
  - `public Action<IntPtr> AppControlHandler { get; set; }`
  - `public Action<Int32> BatteryLowHandler { get; set; }`
  - `public Action CreatedHandler { get; set; }`
  - `public DaliNativeApp Current { get; set; }`
  - `public Action<Int32> DeviceOrientationChangedHandler { get; set; }`
  - `public Action<String> LanguageChangedHandler { get; set; }`
  - `public Action<Int32> MemoryLowHandler { get; set; }`
  - `public Action PausedHandler { get; set; }`
  - `public Action<String> RegionChangedHandler { get; set; }`
  - `public Action ResumedHandler { get; set; }`
  - `public Action TerminatedHandler { get; set; }`

### class `DaliProperty`

- Base: `System.Object`
- Members:
  - `public static Void Get(NObject view, Int32 key, Single& value)`
  - `public static Void Get(NObject view, Int32 key, Int32& value)`
  - `public static Void Get(NObject view, Int32 key, Boolean& value)`
  - `public static Void Get(NObject view, Int32 key, PropertyValueHandle& value)`
  - `public static Void Get(NObject view, Int32 key, CornerRadius& value)`
  - `public static Void Get(NObject view, Int32 key, PropertyMapHandle map)`
  - `public static Void Get(NObject view, Int32 key, String& value)`
  - `public static Void Get(NObject view, Int32 visualIndex, Int32 key, Color& value)`
  - `public static Void Get(NObject view, Int32 visualIndex, Int32 key, Int32& value)`
  - `public static Void Get(NObject view, Int32 visualIndex, Int32 key, CornerRadius& value)`
  - `public static Void Get(NObject view, Int32 key, Color& value)`
  - `public static Void Get(NObject view, Int32 key, Single& v1, Single& v2, Single& v3)`
  - `public static Void Get(NObject view, Int32 key, Point& value)`
  - `public static Void GetCurrent(NObject view, Int32 key, Size& value)`
  - `public static Void GetCurrent(NObject view, Int32 key, Point& value)`
  - `public static Int32 Register(NObject obj, String name, PropertyValueHandle value)`
  - `public static Void Set(NObject view, Int32 key, Single value)`
  - `public static Void Set(NObject view, Int32 key, PropertyValueHandle value)`
  - `public static Void Set(NObject view, Int32 key, Vector4Handle value)`
  - `public static Void Set(NObject view, Int32 key, Vector3Handle value)`
  - `public static Void Set(NObject view, Int32 key, Vector2Handle value)`
  - `public static Void Set(NObject view, Int32 key, Int32 value)`
  - `public static Void Set(NObject view, Int32 key, Boolean value)`
  - `public static Void Set(NObject view, Int32 key, String value)`
  - `public static Void Set(NObject view, Int32 key, Color value)`
  - `public static Void Set(NObject obj, String name, PropertyValueHandle value)`

### class `DaliPropertyKey`

- Base: `System.Object`
- Members:

### enum `DaliPropertyType`

- Base: `System.Enum`
- Members:

### enum `DepthFunctionType`

- Base: `System.Enum`
- Members:

### enum `DepthTestModeType`

- Base: `System.Enum`
- Members:

### enum `DepthWriteModeType`

- Base: `System.Enum`
- Members:

### class `DeviceOrientationChangedSignal`

- Base: `Tizen.UI.Internal.ApplicationSignal`1<Tizen.UI.Internal.DeviceOrientationChangedSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `private Void WrapperCallback(Int32 degree)`

### enum `FaceCullingModeType`

- Base: `System.Enum`
- Members:

### enum `FilterMode`

- Base: `System.Enum`
- Members:

### class `FocusChangedSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.FocusChangedSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr prev, IntPtr next)`

### class `Geometry`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public UInt32 AddVertexBuffer(VertexBuffer vertexBuffer)`
  - `public GeometryType get_GeometryType()`
  - `public UInt32 GetNumberOfVertexBuffers()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void RemoveVertexBuffer(UInt32 index)`
  - `public Void set_GeometryType(GeometryType value)`
  - `public Void SetIndexBuffer(UInt16[] indices, UInt32 count)`
  - `public GeometryType GeometryType { get; set; }`

### enum `GeometryType`

- Base: `System.Enum`
- Members:

### class `HoverEventSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.HoverEventSignal/SignalDelegate>`
- Members:
  - `public .ctor(View view)`
  - `public .ctor(Layer layer)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Boolean WrapperCallback(IntPtr view, IntPtr eventArgs)`

### interface `ILabelSignalHandler`

- Members:
  - `public Void OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `public Void OnTextFitChangedSignal(IntPtr arg)`

### class `ImageVisualMap`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `public .ctor()`
  - `public event EventHandler UpdateRequired`
  - `public Void add_UpdateRequired(EventHandler value)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `public String get_AlphaMaskUrl()`
  - `public Thickness get_Border()`
  - `public Boolean get_BorderOnly()`
  - `public Boolean get_CropToMask()`
  - `public FittingMode get_FittingMode()`
  - `public PropertyMapHandle get_Handle()`
  - `public Boolean get_ImageLoadWithViewSize()`
  - `public Boolean get_IsNinePatchImage()`
  - `public Color get_MultipliedColor()`
  - `public Boolean get_OrientationCorrection()`
  - `public String get_ResourceUrl()`
  - `public Boolean get_SynchronousLoading()`
  - `protected Int32 GetImageTypeIndex()`
  - `public Void remove_UpdateRequired(EventHandler value)`
  - `protected Void SendUpdateRequired()`
  - `public Void set_AlphaMaskUrl(String value)`
  - `public Void set_Border(Thickness value)`
  - `public Void set_BorderOnly(Boolean value)`
  - `public Void set_CropToMask(Boolean value)`
  - `public Void set_FittingMode(FittingMode value)`
  - `private Void set_Handle(PropertyMapHandle value)`
  - `public Void set_ImageLoadWithViewSize(Boolean value)`
  - `public Void set_IsNinePatchImage(Boolean value)`
  - `public Void set_MultipliedColor(Color value)`
  - `public Void set_OrientationCorrection(Boolean value)`
  - `public Void set_ResourceUrl(String value)`
  - `public Void set_SynchronousLoading(Boolean value)`
  - `public Void SetDirty()`
  - `public Void SetFastTrackUploading(Boolean enable)`
  - `public Void SetMaskingMode(ImageMaskingMode mode)`
  - `public Void SetPixelArea(Rect area)`
  - `public Void SetReleasePolicy(ImageReleasePolicy releasePolicy)`
  - `public Void SetWrapModeU(ImageWrapMode mode)`
  - `public Void SetWrapModeV(ImageWrapMode mode)`
  - `protected Void UpdateResourceUrl()`
  - `public String AlphaMaskUrl { get; set; }`
  - `public Thickness Border { get; set; }`
  - `public Boolean BorderOnly { get; set; }`
  - `public Boolean CropToMask { get; set; }`
  - `public FittingMode FittingMode { get; set; }`
  - `public PropertyMapHandle Handle { get; set; }`
  - `public Boolean ImageLoadWithViewSize { get; set; }`
  - `public Boolean IsNinePatchImage { get; set; }`
  - `public Color MultipliedColor { get; set; }`
  - `public Boolean OrientationCorrection { get; set; }`
  - `public String ResourceUrl { get; set; }`
  - `public Boolean SynchronousLoading { get; set; }`

### class `InputMethodStatusSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.InputMethodStatusSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(Boolean statusChanged)`

### class `InterceptTouchSignal`

- Base: `Tizen.UI.Internal.TouchEventSignal`
- Members:
  - `public .ctor(View view)`
  - `public .ctor(Window window)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`

### class `InterceptWheelEventSignal`

- Base: `Tizen.UI.Internal.WheelEventSignal`
- Members:
  - `public .ctor(View view)`
  - `public .ctor(Layer layer)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`

### interface `IResourceReadySignalHandler`

- Members:
  - `public UInt32 get_ID()`
  - `public Void OnResourceReadySignal(IntPtr view)`
  - `public UInt32 ID { get; }`

### interface `ITextEditorSignalHandler`

- Members:
  - `public Void OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `public Void OnMaxLengthReachedSignal(IntPtr view)`
  - `public Void OnTextChangedSignal(IntPtr view)`

### interface `ITextFieldSignalHandler`

- Members:
  - `public Void OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hrefLength)`
  - `public Void OnMaxLengthReachedSignal(IntPtr view)`
  - `public Void OnTextChangedSignal(IntPtr view)`

### interface `IViewSignalHandler`

- Members:
  - `public Void OnAggregatedVisibilityChangedSignal(IntPtr data, Boolean visibility)`
  - `public Void OnFocusedSignal(IntPtr view)`
  - `public Boolean OnHitTestSignal(IntPtr view, IntPtr evt)`
  - `public Boolean OnHoverEventSignal(IntPtr view, IntPtr evt)`
  - `public Boolean OnInterceptTouchEventSignal(IntPtr view, IntPtr evt)`
  - `public Boolean OnInterceptWheelEventSignal(IntPtr view, IntPtr evt)`
  - `public Boolean OnKeyEventSignal(IntPtr view, IntPtr keyEvt)`
  - `public Void OnLayoutDirectionChangedSignal(IntPtr data, Int32 direction)`
  - `public Void OnOffSceneSignal(IntPtr view)`
  - `public Void OnRelayoutSignal(IntPtr view)`
  - `public Void OnSceneSignal(IntPtr view)`
  - `public Boolean OnTouchEventSignal(IntPtr view, IntPtr evt)`
  - `public Void OnUnfocusedSignal(IntPtr view)`
  - `public Void OnVisibilityChangedSignal(IntPtr data, Boolean visibility, Int32 type)`
  - `public Boolean OnWheelEventSignal(IntPtr view, IntPtr evt)`

### class `KeyboardEventSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.KeyboardEventSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private IntPtr WrapperCallback(IntPtr inputMethodContext, IntPtr eventData)`

### class `KeyboardResizedSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.KeyboardResizedSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(Int32 resized)`

### class `KeyInputFocusSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrSignal`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### class `LanguageChangedSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrApplicationSignal`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`

### class `LongPressGestureSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.LongPressGestureSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr view, IntPtr gesture)`

### class `LottieVisualMap`

- Base: `Tizen.UI.Internal.ImageVisualMap`
- Members:
  - `public .ctor()`

### class `MemoryLowSignal`

- Base: `Tizen.UI.Internal.ApplicationSignal`1<Tizen.UI.Internal.MemoryLowSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `private Void WrapperCallback(Int32 memoryStatus)`

### struct `MultiPageData`

- Base: `System.ValueType`
- Members:
  - `public .ctor(String text, Single fontSize, Single width, Single height)`
  - `public String get_FontFamily()`
  - `public Single get_FontSize()`
  - `public Single get_Height()`
  - `public Nullable<TextAlignment> get_HorizontalAlignment()`
  - `public Nullable<Boolean> get_IsMarkupEnabled()`
  - `public Nullable<Single> get_LineHeight()`
  - `public Nullable<Thickness> get_Padding()`
  - `public String get_Text()`
  - `public Nullable<TextAlignment> get_VerticalAlignment()`
  - `public Single get_Width()`
  - `public Void set_FontFamily(String value)`
  - `public Void set_HorizontalAlignment(Nullable<TextAlignment> value)`
  - `public Void set_IsMarkupEnabled(Nullable<Boolean> value)`
  - `public Void set_LineHeight(Nullable<Single> value)`
  - `public Void set_Padding(Nullable<Thickness> value)`
  - `public Void set_VerticalAlignment(Nullable<TextAlignment> value)`
  - `internal TextRendererParametersHandle ToHandle()`
  - `public String FontFamily { get; set; }`
  - `public Single FontSize { get; }`
  - `public Single Height { get; }`
  - `public Nullable<TextAlignment> HorizontalAlignment { get; set; }`
  - `public Nullable<Boolean> IsMarkupEnabled { get; set; }`
  - `public Nullable<Single> LineHeight { get; set; }`
  - `public Nullable<Thickness> Padding { get; set; }`
  - `public String Text { get; }`
  - `public Nullable<TextAlignment> VerticalAlignment { get; set; }`
  - `public Single Width { get; }`

### class `NativeImageSource`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `public .ctor(Int32 width, Int32 height)`
  - `public IntPtr AccquiredBuffer(Int32& width, Int32& height, Int32& stride)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `internal IntPtr get_Handle()`
  - `public Void ReleaseBuffer()`
  - `public Void SetTbmSurface(IntPtr tbmSurface)`
  - `internal IntPtr Handle { get; }`

### class `ObjectPool`

- Base: `System.Object`
- Members:
  - `public static ObjectPool<Vector2Handle> NewVector2()`
  - `public static ObjectPool<Vector2Handle> NewVector2(Single x, Single y)`
  - `public static ObjectPool<Vector3Handle> NewVector3()`
  - `public static ObjectPool<Vector3Handle> NewVector3(Single x, Single y, Single z)`
  - `public static ObjectPool<Vector4Handle> NewVector4()`
  - `public static ObjectPool<Vector4Handle> NewVector4(Single x, Single y, Single z, Single w)`

### class `ObjectPool`1`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `private static .cctor()`
  - `public .ctor(T value)`
  - `public Void Dispose()`
  - `public static Int32 get_Count()`
  - `public T get_Value()`
  - `public static ObjectPool<T> New()`
  - `public static T op_Implicit(ObjectPool<T> obj)`
  - `public Int32 Count { get; }`
  - `public T Value { get; }`

### class `ObservableCollectionEx`1`

- Base: `System.Collections.ObjectModel.ObservableCollection`1<T>`
- Members:
  - `public .ctor()`
  - `protected Void ClearItems()`

### class `PanGestureSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.PanGestureSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr view, IntPtr gesture)`

### class `PauseSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrApplicationSignal`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`

### class `PinchGestureSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.PinchGestureSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr view, IntPtr gesture)`

### class `ProcessorController`

- Base: `Tizen.UI.NObject`
- Members:
  - `private static .cctor()`
  - `private .ctor()`
  - `public event EventHandler LayoutIteration`
  - `public event EventHandler LayoutIterationOnce`
  - `private Void <Awake>b__19_0()`
  - `private Void <Process>b__21_0()`
  - `public Void add_LayoutIteration(EventHandler value)`
  - `public Void add_LayoutIterationOnce(EventHandler value)`
  - `public Void Awake()`
  - `public static ProcessorController get_Instance()`
  - `public Boolean get_IsOnProcess()`
  - `public Boolean get_IsOnRelayout()`
  - `private Void Process()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void remove_LayoutIteration(EventHandler value)`
  - `public Void remove_LayoutIterationOnce(EventHandler value)`
  - `private Void set_IsOnProcess(Boolean value)`
  - `internal Void set_IsOnRelayout(Boolean value)`
  - `public ProcessorController Instance { get; }`
  - `public Boolean IsOnProcess { get; set; }`
  - `public Boolean IsOnRelayout { get; set; }`

### class `RegionChangedSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrApplicationSignal`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`

### class `Renderer`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(Geometry geometry, Shader shader)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Color get_BlendColor()`
  - `public BlendEquationType get_BlendEquationAlpha()`
  - `public BlendEquationType get_BlendEquationRgb()`
  - `public BlendFactorType get_BlendFactorDestAlpha()`
  - `public BlendFactorType get_BlendFactorDestRgb()`
  - `public BlendFactorType get_BlendFactorSrcAlpha()`
  - `public BlendFactorType get_BlendFactorSrcRgb()`
  - `public BlendModeType get_BlendMode()`
  - `public Boolean get_BlendPreMultipliedAlpha()`
  - `public DepthFunctionType get_DepthFunction()`
  - `public Int32 get_DepthIndex()`
  - `public DepthTestModeType get_DepthTestMode()`
  - `public DepthWriteModeType get_DepthWriteMode()`
  - `public FaceCullingModeType get_FaceCullingMode()`
  - `public Int32 get_IndexRangeCount()`
  - `public Int32 get_IndexRangeFirst()`
  - `public StencilOperationType get_InternalStencilOperationOnZFail()`
  - `public RenderModeType get_RenderMode()`
  - `public StencilFunctionType get_StencilFunction()`
  - `public Int32 get_StencilFunctionReference()`
  - `public Int32 get_StencilMask()`
  - `public StencilOperationType get_StencilOperationOnFail()`
  - `public StencilOperationType get_StencilOperationOnZPass()`
  - `public Geometry GetGeometry()`
  - `public Shader GetShader()`
  - `public TextureSet GetTextures()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void set_BlendColor(Color value)`
  - `public Void set_BlendEquationAlpha(BlendEquationType value)`
  - `public Void set_BlendEquationRgb(BlendEquationType value)`
  - `public Void set_BlendFactorDestAlpha(BlendFactorType value)`
  - `public Void set_BlendFactorDestRgb(BlendFactorType value)`
  - `public Void set_BlendFactorSrcAlpha(BlendFactorType value)`
  - `public Void set_BlendFactorSrcRgb(BlendFactorType value)`
  - `public Void set_BlendMode(BlendModeType value)`
  - `public Void set_BlendPreMultipliedAlpha(Boolean value)`
  - `public Void set_DepthFunction(DepthFunctionType value)`
  - `public Void set_DepthIndex(Int32 value)`
  - `public Void set_DepthTestMode(DepthTestModeType value)`
  - `public Void set_DepthWriteMode(DepthWriteModeType value)`
  - `public Void set_FaceCullingMode(FaceCullingModeType value)`
  - `public Void set_IndexRangeCount(Int32 value)`
  - `public Void set_IndexRangeFirst(Int32 value)`
  - `public Void set_InternalStencilOperationOnZFail(StencilOperationType value)`
  - `public Void set_RenderMode(RenderModeType value)`
  - `public Void set_StencilFunction(StencilFunctionType value)`
  - `public Void set_StencilFunctionReference(Int32 value)`
  - `public Void set_StencilMask(Int32 value)`
  - `public Void set_StencilOperationOnFail(StencilOperationType value)`
  - `public Void set_StencilOperationOnZPass(StencilOperationType value)`
  - `public Void SetGeometry(Geometry geometry)`
  - `public Void SetIndexRange(Int32 firstElement, Int32 elementsCount)`
  - `public Void SetShader(Shader shader)`
  - `public Void SetTextures(TextureSet textureSet)`
  - `public Color BlendColor { get; set; }`
  - `public BlendEquationType BlendEquationAlpha { get; set; }`
  - `public BlendEquationType BlendEquationRgb { get; set; }`
  - `public BlendFactorType BlendFactorDestAlpha { get; set; }`
  - `public BlendFactorType BlendFactorDestRgb { get; set; }`
  - `public BlendFactorType BlendFactorSrcAlpha { get; set; }`
  - `public BlendFactorType BlendFactorSrcRgb { get; set; }`
  - `public BlendModeType BlendMode { get; set; }`
  - `public Boolean BlendPreMultipliedAlpha { get; set; }`
  - `public DepthFunctionType DepthFunction { get; set; }`
  - `public Int32 DepthIndex { get; set; }`
  - `public DepthTestModeType DepthTestMode { get; set; }`
  - `public DepthWriteModeType DepthWriteMode { get; set; }`
  - `public FaceCullingModeType FaceCullingMode { get; set; }`
  - `public Int32 IndexRangeCount { get; set; }`
  - `public Int32 IndexRangeFirst { get; set; }`
  - `public StencilOperationType InternalStencilOperationOnZFail { get; set; }`
  - `public RenderModeType RenderMode { get; set; }`
  - `public StencilFunctionType StencilFunction { get; set; }`
  - `public Int32 StencilFunctionReference { get; set; }`
  - `public Int32 StencilMask { get; set; }`
  - `public StencilOperationType StencilOperationOnFail { get; set; }`
  - `public StencilOperationType StencilOperationOnZPass { get; set; }`

### enum `RenderingBehavior`

- Base: `System.Enum`
- Members:

### enum `RenderModeType`

- Base: `System.Enum`
- Members:

### class `RenderPropertyNotification`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Dispose(Boolean disposing)`
  - `public Action get_UpdateAction()`
  - `private Void OnUpdate(IntPtr _)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void set_UpdateAction(Action value)`
  - `public Action UpdateAction { get; set; }`

### class `RenderPropertyNotificationSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrSignal`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### class `ResetSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrApplicationSignal`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`

### class `ResourceManager`

- Base: `System.Object`
- Members:
  - `public static String get_ResourceDirectory()`
  - `public static String GetCommonPath(String res)`
  - `public static String GetPath(String res)`
  - `public String ResourceDirectory { get; }`

### class `ResourceReadySignal`

- Base: `Tizen.UI.Internal.SingleIntPtrSignal`
- Members:
  - `public .ctor(NObject view)`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### class `ResumeSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrApplicationSignal`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`

### class `Rotation`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `public .ctor(Single degree, RotationAxis axis)`
  - `public .ctor(RotationHandle handle)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `public RotationHandle get_Handle()`
  - `public Rotation Multiply(Rotation rotation)`
  - `public static Rotation op_Multiply(Rotation arg1, Rotation arg2)`
  - `public RotationHandle Handle { get; }`

### enum `RotationAxis`

- Base: `System.Enum`
- Members:

### class `RotationGestureSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.RotationGestureSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr view, IntPtr gesture)`

### class `Sampler`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void SetFilterMode(FilterMode minFilter, FilterMode magFilter)`
  - `public Void SetWrapMode(ImageWrapMode uWrap, ImageWrapMode vWrap)`
  - `public Void SetWrapMode(ImageWrapMode rWrap, ImageWrapMode sWrap, ImageWrapMode tWrap)`

### class `Shader`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(String vertexShader, String fragmentShader)`
  - `public .ctor(String vertexShader, String fragmentShader, ShaderHint hints)`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### enum `ShaderHint`

- Base: `System.Enum`
- Members:

### class `Signal`1`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `protected .ctor(IntPtr handle)`
  - `protected .ctor(IntPtr handle, Boolean hasOwnership)`
  - `public Void Connect(T callback)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `public Void Disconnect()`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected Void Finalize()`
  - `public IntPtr get_Handle()`
  - `public Boolean get_HasOwnership()`
  - `protected T get_UserCallback()`
  - `protected T GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `protected Void set_Handle(IntPtr value)`
  - `protected Void set_HasOwnership(Boolean value)`
  - `public IntPtr Handle { get; set; }`
  - `public Boolean HasOwnership { get; set; }`
  - `protected T UserCallback { get; }`

### class `SingleIntPtrApplicationSignal`

- Base: `Tizen.UI.Internal.ApplicationSignal`1<Tizen.UI.Internal.SingleIntPtrApplicationSignal/SignalDelegate>`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `private Void WrapperCallback(IntPtr app)`

### class `SingleIntPtrSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.SingleIntPtrSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `private Void WrapperCallback(IntPtr handle)`

### class `StaticAggregatedVisibilityChangedSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnAggregatedVisibilityChangedSignal(IntPtr view, Boolean visibility)`

### class `StaticHitTestResultSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Boolean OnHitTestSignal(IntPtr view, IntPtr evt)`

### class `StaticHoverEventSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Boolean OnHoverEventSignal(IntPtr view, IntPtr evt)`

### class `StaticInterceptTouchSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Boolean OnInterceptTouchEventSignal(IntPtr view, IntPtr evt)`

### class `StaticInterceptWheelEventSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Boolean OnInterceptWheelEventSignal(IntPtr view, IntPtr evt)`

### class `StaticKeyInputFocusGainedSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnFocusedSignal(IntPtr view)`

### class `StaticKeyInputFocusLostSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnUnfocusedSignal(IntPtr view)`

### class `StaticLayoutDirectionSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnLayoutDirectionChangedSignal(IntPtr view, Int32 type)`

### class `StaticOffSceneSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnOffSceneSignal(IntPtr view)`

### class `StaticOnSceneSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnSceneSignal(IntPtr view)`

### class `StaticRelayoutSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnRelayoutSignal(IntPtr view)`

### class `StaticTextEditorAnchorClickedSignal`

- Base: `Tizen.UI.Internal.StaticTextEditorSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hrefLength)`

### class `StaticTextEditorMaxLengthReachedSignal`

- Base: `Tizen.UI.Internal.StaticTextEditorSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnMaxLengthReachedSignal(IntPtr view)`

### class `StaticTextEditorSignal`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `protected static ITextEditorSignalHandler GetHandler(UInt32 id)`
  - `protected static UInt32 GetId(IntPtr view)`

### class `StaticTextEditorTextChangedSignal`

- Base: `Tizen.UI.Internal.StaticTextEditorSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnTextChangedSignal(IntPtr view)`

### class `StaticTextFieldAnchorClickedSignal`

- Base: `Tizen.UI.Internal.StaticTextFieldSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hreflength)`

### class `StaticTextFieldMaxLengthReachedSignal`

- Base: `Tizen.UI.Internal.StaticTextFieldSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnMaxLengthReachedSignal(IntPtr view)`

### class `StaticTextFieldSignal`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `protected static ITextFieldSignalHandler GetHandler(UInt32 id)`
  - `protected static UInt32 GetId(IntPtr view)`

### class `StaticTextFieldTextChangedSignal`

- Base: `Tizen.UI.Internal.StaticTextFieldSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnTextChangedSignal(IntPtr view)`

### class `StaticTextFitChangedSignal`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnTextFitChangedSignal(IntPtr view)`

### class `StaticTextLabelAnchorClickedSignal`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnAnchorClickedSignal(IntPtr view, IntPtr href, UInt32 hrefLength)`

### class `StaticTouchEventSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Boolean OnTouchEventSignal(IntPtr view, IntPtr evt)`

### class `StaticViewKeyEventSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Boolean OnKeyEventSignal(IntPtr view, IntPtr evt)`

### class `StaticViewResourceSignal`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IResourceReadySignalHandler GetHandler(UInt32 id)`
  - `private static UInt32 GetId(IntPtr view)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnResourceReadySignal(IntPtr view)`

### class `StaticViewSignal`

- Base: `System.Object`
- Members:
  - `protected .ctor()`
  - `protected static IViewSignalHandler GetHandler(UInt32 id)`
  - `protected static UInt32 GetId(IntPtr handle)`

### class `StaticVisibilityChangedSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Void OnVisibilityChangedSignal(IntPtr view, Boolean visibility, Int32 type)`

### class `StaticWheelEventSignal`

- Base: `Tizen.UI.Internal.StaticViewSignal`
- Members:
  - `public .ctor()`
  - `public static Void Connect(IntPtr handle)`
  - `public static Void Disconnect(IntPtr handle)`
  - `private static IntPtr GetNativeCallbackPtr()`
  - `private static Boolean OnWheelEventSignal(IntPtr view, IntPtr evt)`

### enum `StencilFunctionType`

- Base: `System.Enum`
- Members:

### enum `StencilOperationType`

- Base: `System.Enum`
- Members:

### class `StringToVoidSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrSignal`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `internal static String GetResult(IntPtr data)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `internal static Void SetResult(IntPtr data, String res)`

### class `TapGestureSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.TapGestureSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr view, IntPtr gesture)`

### class `TerminateSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrApplicationSignal`
- Members:
  - `public .ctor(IntPtr daliAppHandle)`

### class `TextPageUtil`

- Base: `System.Object`
- Members:
  - `public static IList<String> GetPages(String text, Single fontSize, Single width, Single height, Nullable<Boolean> enableMarkup, Nullable<Single> lineHeight, Nullable<TextAlignment> horizonAlignment, Nullable<TextAlignment> verticalAlignment, String fontFamily, Nullable<Thickness> textPadding)`
  - `public static IList<String> GetPages(MultiPageData param)`
  - `private static Void HandleCloseTags(StringBuilder sb, IList<TagData> tags)`
  - `private static IList<String> HandleHtmlText(String text, IList<Int32> stopIndexes)`
  - `private static Void HandleOpenTags(StringBuilder sb, IList<TagData> tags)`
  - `private static IList<String> HandlePlainText(String text, IList<Int32> stopIndexes)`
  - `private static Void PopTag(IList<TagData> opend, String tagName)`
  - `private static Void ResolveTag(Char[] raw, Int32 total, StringBuilder sb, Int32& current, String& tag, String& attr, Boolean& isClose)`

### class `Texture`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(TextureType type, PixelFormat format, UInt32 width, UInt32 height)`
  - `public .ctor(NativeImageSource nativeImageSource)`
  - `public .ctor(IntPtr tbmSurface)`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void GenerateMipmaps()`
  - `public UInt32 GetHeight()`
  - `public UInt32 GetWidth()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Boolean Upload(PixelData pixelData)`
  - `public Boolean Upload(PixelData pixelData, UInt32 layer, UInt32 mipmap, UInt32 xOffset, UInt32 yOffset, UInt32 width, UInt32 height)`

### class `TextureSet`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Sampler GetSampler(Int32 index)`
  - `public Texture GetTexture(Int32 index)`
  - `public Int32 GetTextureCount()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void SetSampler(Int32 index, Sampler sampler)`
  - `public Void SetTexture(Int32 index, Texture texture)`

### enum `TextureType`

- Base: `System.Enum`
- Members:

### class `TimerSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.TimerSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Boolean WrapperCallback()`

### class `TouchEventSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.TouchEventSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Boolean WrapperCallback(IntPtr view, IntPtr eventArgs)`

### class `TouchSignal`

- Base: `Tizen.UI.Internal.TouchEventSignal`
- Members:
  - `public .ctor(NObject nObject)`
  - `public .ctor(Window window)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`

### class `UICoreBackend`

- Base: `System.Object`
- Interfaces: `System.IDisposable`, `Tizen.Applications.CoreBackend.ICoreBackend`, `Tizen.Applications.CoreBackend.ICoreTaskBackend`
- Members:
  - `public .ctor()`
  - `public .ctor(WindowMode windowMode, Rect windowBounds)`
  - `public Void AddEventHandler(EventType evType, Action handler)`
  - `public Void AddEventHandler(EventType evType, Action<TEventArgs> handler)`
  - `public static UICoreBackend CreateForMock()`
  - `public Void Dispose()`
  - `public Void Exit()`
  - `public Boolean get_IsUIThread()`
  - `public NObject GetDaliNativeApp()`
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
  - `public Void Post(Action action)`
  - `public Void Run(String[] args)`
  - `public Void SetCoreTask(ICoreTask task)`
  - `public Boolean IsUIThread { get; }`

### class `VertexBuffer`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor(IDictionary<String, DaliPropertyType> format)`
  - `protected .ctor(PropertyMapHandle format)`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private static PropertyMapHandle CreateFormat(IDictionary<String, DaliPropertyType> format)`
  - `public UInt32 GetSize()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void SetData(IntPtr data, Int32 size)`
  - `public Void SetData(T[] vertices)`

### class `ViewExtensions`

- Base: `System.Object`
- Members:
  - `public static Int32 AddRenderer(View view, Renderer renderer)`
  - `public static Thickness GetPadding(View view)`
  - `public static Renderer GetRendererAt(View view, Int32 index)`
  - `public static Int32 GetRendererCount(View view)`
  - `public static Void RemoveRenderer(View view, Renderer renderer)`
  - `public static Void SetPadding(View view, Thickness padding)`

### class `ViewSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrSignal`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### class `VisualSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.VisualSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr view, Int32 visualIndex, Int32 signalId)`

### enum `VisualTransformPolicyType`

- Base: `System.Enum`
- Members:

### enum `VisualTransformPropertyType`

- Base: `System.Enum`
- Members:

### class `VoidSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.VoidSignal/SignalDelegate>`
- Members:
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback()`

### class `WheelEventSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.WheelEventSignal/SignalDelegate>`
- Members:
  - `public .ctor(View view)`
  - `public .ctor(Layer layer)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Boolean WrapperCallback(IntPtr view, IntPtr eventArgs)`

### class `WindowExtensions`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Void AddFrameRenderedCallback(Window window, Action<Int32> callback, Int32 id)`
  - `public static RenderingBehavior GetRenderingBehavior(Window window)`
  - `private static Void OnFrameRendered(Int32 frameId)`
  - `public static Void SetRenderingBehavior(Window window, RenderingBehavior behavior)`

### class `WindowFocusEventSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.WindowFocusEventSignal/SignalDelegate>`
- Members:
  - `public .ctor(Window window)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr window, Boolean focusGained)`

### class `WindowKeyEventSignal`

- Base: `Tizen.UI.Internal.SingleIntPtrSignal`
- Members:
  - `public .ctor(Window window)`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected Void ReleaseHandle(IntPtr handle)`

### class `WindowMoveCompletedSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.WindowMoveCompletedSignal/SignalDelegate>`
- Members:
  - `public .ctor(Window window)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr window, IntPtr windowSize)`

### class `WindowMovedSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.WindowMovedSignal/SignalDelegate>`
- Members:
  - `public .ctor(Window window)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr window, IntPtr windowSize)`

### class `WindowResizeCompletedSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.WindowResizeCompletedSignal/SignalDelegate>`
- Members:
  - `public .ctor(Window window)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr window, IntPtr windowSize)`

### class `WindowResizeSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.WindowResizeSignal/SignalDelegate>`
- Members:
  - `public .ctor(Window window)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr window, IntPtr windowSize)`

### class `WindowVisibilityChangedSignal`

- Base: `Tizen.UI.Internal.Signal`1<Tizen.UI.Internal.WindowVisibilityChangedSignal/SignalDelegate>`
- Members:
  - `public .ctor(Window window)`
  - `protected Void Connect(IntPtr handle, IntPtr callback)`
  - `protected Void Disconnect(IntPtr handle, IntPtr callback)`
  - `protected SignalDelegate GetWrapperCallback()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `private Void WrapperCallback(IntPtr data, Boolean visibility)`

## Namespace `Tizen.UI.NativeHandle`

### class `ActorHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `AnimatablePropertyHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `BackgroundBlurEffectHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `ByteVectorHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `DegreeHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `ExtentsHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `Factory`

- Base: `System.Object`
- Members:
  - `public static ObjectPool<Vector2Handle> CreateVector2(Single x, Single y)`
  - `public static ObjectPool<Vector3Handle> CreateVector3(Single x, Single y, Single z)`
  - `public static ObjectPool<Vector4Handle> CreateVector4(Single x, Single y, Single z, Single w)`

### class `GaussianBlurEffectHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `GeometryHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `Int32PairHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `KeyFramesHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `MaskEffectHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `NativeBaseHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `NativeHandleExtensions`

- Base: `System.Object`
- Members:
  - `public static T CheckException(T v)`
  - `public static Void CheckException()`
  - `public static RadianHandle DegreeToRadian(Single degree)`
  - `internal static String PascalToCamel(String value)`
  - `public static Single RadianToDegree(Single radians)`
  - `internal static Void SetValue(Vector4Handle vector, Single x, Single y, Single z, Single w)`
  - `internal static Void SetValue(Vector2Handle vector, Single x, Single y)`
  - `internal static Void SetValue(Vector3Handle vector, Single x, Single y, Single z)`
  - `public static Color ToColor(Vector4Handle vector)`
  - `public static Color ToColor(ObjectPool<Vector4Handle> vector)`
  - `public static CornerRadius ToCornerRadius(ObjectPool<Vector4Handle> vector)`
  - `public static CornerRadius ToCornerRadius(Vector4Handle vector)`
  - `internal static Int32PairHandle ToInt32Pair(Point point)`
  - `public static ObjectPool<Vector4Handle> ToNative(Color color)`
  - `public static ObjectPool<Vector4Handle> ToNative(CornerRadius value)`
  - `public static ObjectPool<Vector4Handle> ToNative(Rect value)`
  - `public static Vector4Handle ToNative(Color color, Vector4Handle vector)`
  - `public static ObjectPool<Vector2Handle> ToNative(Point value)`
  - `public static ObjectPool<Vector2Handle> ToNative(Size value)`
  - `public static DegreeHandle ToNative(Single degree)`
  - `public static RectangleHandle ToNativeRectangle(Thickness rect)`
  - `public static RectangleHandle ToNativeRectangle(Rect rect)`
  - `public static Point ToPoint(Vector2Handle value)`
  - `public static Point ToPoint(Vector3Handle value)`
  - `internal static Point ToPoint(Int32PairHandle handle)`
  - `internal static Int32 ToPropertyIndex(RenderNotificationKey key)`
  - `public static Rect ToRect(Vector4Handle vector)`
  - `public static Size ToSize(Vector3Handle vector)`
  - `internal static Size ToSize(UInt16PairHandle handle)`
  - `internal static ObjectPool<Vector3Handle> ToVector(RotationAxis axis)`

### class `NativeImageSourceWrapperHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `PropertyArrayExtensions`

- Base: `System.Object`
- Members:
  - `public static Void Add(PropertyArrayHandle handle, PropertyValueHandle value)`
  - `public static PropertyValueHandle GetAt(PropertyArrayHandle handle, Int32 index)`

### class `PropertyArrayHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `PropertyConditionHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `PropertyKeyHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `PropertyMapHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `PropertyMapHandleExtensions`

- Base: `System.Object`
- Members:
  - `public static Void Add(PropertyMapHandle handle, Int32 key, Int32 value)`
  - `public static Void Add(PropertyMapHandle handle, Int32 key, Boolean value)`
  - `public static Void Add(PropertyMapHandle handle, Int32 key, Color value)`
  - `public static Void Add(PropertyMapHandle handle, Int32 key, Single value)`
  - `public static Void Add(PropertyMapHandle handle, Int32 key, String value)`
  - `public static Void Add(PropertyMapHandle handle, Int32 key, Point value)`
  - `public static Void Add(PropertyMapHandle handle, Int32 key, Size value)`
  - `public static Void Add(PropertyMapHandle handle, String key, Color value)`
  - `public static Void Add(PropertyMapHandle handle, String key, Point value)`
  - `public static Void Add(PropertyMapHandle handle, String key, Single value)`
  - `public static Void Add(PropertyMapHandle handle, String key, Int32 value)`
  - `public static Void Add(PropertyMapHandle handle, String key, Boolean value)`
  - `public static Void Add(PropertyMapHandle handle, Int32 key, PropertyValueHandle value)`
  - `public static Void Add(PropertyMapHandle handle, Int32 key, PropertyMapHandle value)`
  - `public static PropertyValueHandle Find(PropertyMapHandle handle, Int32 key)`
  - `public static PropertyValueHandle Find(PropertyMapHandle handle, String key)`
  - `public static Void Remove(PropertyMapHandle handle, Int32 key)`
  - `public static Void Remove(PropertyMapHandle handle, String key)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, Int32 value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, Thickness value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, PropertyValueHandle value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, PropertyMapHandle value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, CornerRadius value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, String value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, Boolean value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, Color value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, Single value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, Point value)`
  - `public static Void Set(PropertyMapHandle handle, Int32 key, Rect value)`
  - `public static Void Set(PropertyMapHandle handle, String key, Color value)`
  - `public static Void Set(PropertyMapHandle handle, String key, Point value)`
  - `public static Void Set(PropertyMapHandle handle, String key, Single value)`
  - `public static Void Set(PropertyMapHandle handle, String key, Boolean value)`
  - `public static Void Set(PropertyMapHandle handle, String key, String value)`

### class `PropertyValueHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `PropertyValueHandleExtensions`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Color ToColor(PropertyValueHandle handle)`
  - `public static CornerRadius ToCornerRadius(PropertyValueHandle handle)`
  - `public static Single ToFloat(PropertyValueHandle handle)`
  - `public static Int32 ToInt(PropertyValueHandle handle)`
  - `public static PropertyValueHandle ToRectangleValue(Thickness value)`
  - `public static String ToStr(PropertyValueHandle handle)`
  - `public static PropertyValueHandle ToValue(PropertyMapHandle handle)`
  - `public static PropertyValueHandle ToValue(PropertyArrayHandle handle)`
  - `public static PropertyValueHandle ToValue(Background background)`
  - `public static PropertyValueHandle ToValue(TextShadow shadow)`
  - `public static PropertyValueHandle ToValue(InputMethodSetting setting)`
  - `public static PropertyValueHandle ToValue(Underline underline)`
  - `public static PropertyValueHandle ToValue(Outline outline)`
  - `public static PropertyValueHandle ToValue(Shadow shadow)`
  - `public static PropertyValueHandle ToValue(InnerShadow shadow)`
  - `public static PropertyValueHandle ToValue(FontStyle fontStyle)`
  - `public static PropertyValueHandle ToValue(HiddenInputSetting setting)`
  - `public static PropertyValueHandle ToValue(AutoFontSize autoSize)`
  - `public static PropertyValueHandle ToValue(Color color)`
  - `public static PropertyValueHandle ToValue(Color color, Boolean useVector3)`
  - `public static PropertyValueHandle ToValue(CornerRadius value)`
  - `public static PropertyValueHandle ToValue(Int32 value)`
  - `public static PropertyValueHandle ToValue(Size value)`
  - `public static PropertyValueHandle ToValue(Point value, Boolean useVector3)`
  - `public static PropertyValueHandle ToValue(Rect value)`
  - `public static PropertyValueHandle ToValue(Thickness value)`
  - `public static PropertyValueHandle ToValue(Object value, Boolean useVector3)`
  - `public static PropertyValueHandle ToValue(Single value)`
  - `public static PropertyValueHandle ToValue(Boolean value)`
  - `public static PropertyValueHandle ToValue(String value)`

### class `RadianHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `RectangleHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `RendererHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `RotationHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `StyleManagerHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `TextRendererParametersHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `TimePeriodHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `UInt16PairHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `Vector2Handle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `Vector3Handle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `Vector4Handle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `WindowBlurHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

