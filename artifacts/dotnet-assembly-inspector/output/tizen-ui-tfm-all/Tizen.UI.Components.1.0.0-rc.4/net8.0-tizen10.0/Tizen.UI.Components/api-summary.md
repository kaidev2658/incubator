# API Summary: Tizen.UI.Components

Source: `/var/folders/hz/_qhnnrpx00nbls9m60h83lym0000gp/T/dotnet-assembly-inspector/7d2fe5005d754aa7b1e335d5a14d17be/lib/net8.0-tizen10.0/Tizen.UI.Components.dll`
Generated (UTC): 2026-03-04T14:04:50.1214080+00:00

## Namespace `(global)`

### class `<PrivateImplementationDetails>`

- Base: `System.Object`
- Members:

## Namespace `Tizen.UI.Components`

### class `AccessibilityExtraData`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public String get_TranslatableDescription()`
  - `public String get_TranslatableName()`
  - `public Func<String, String> get_Translator()`
  - `public String GetTranslatedDescription()`
  - `public String GetTranslatedName()`
  - `private Func<String, String> GetTranslator()`
  - `public Void set_TranslatableDescription(String value)`
  - `public Void set_TranslatableName(String value)`
  - `public Void set_Translator(Func<String, String> value)`
  - `public String TranslatableDescription { get; set; }`
  - `public String TranslatableName { get; set; }`
  - `public Func<String, String> Translator { get; set; }`

### class `AnimatedImageVisualMap`

- Base: `Tizen.UI.Internal.ImageVisualMap`
- Members:
  - `public .ctor()`
  - `public Int32 get_CacheSize()`
  - `public Single get_FrameSpeedFactor()`
  - `public Boolean get_HasValidSequentialResourceUrls()`
  - `public Int32 get_PreloadingBatchSize()`
  - `public Int32 get_RepeatCount()`
  - `public AnimationRepeatMode get_RepeatMode()`
  - `public IEnumerable<String> get_SequentialResourceUrls()`
  - `public AnimationStopBehavior get_StopBehavior()`
  - `protected Int32 GetImageTypeIndex()`
  - `public Void set_CacheSize(Int32 value)`
  - `public Void set_FrameSpeedFactor(Single value)`
  - `public Void set_PreloadingBatchSize(Int32 value)`
  - `public Void set_RepeatCount(Int32 value)`
  - `public Void set_RepeatMode(AnimationRepeatMode value)`
  - `public Void set_SequentialResourceUrls(IEnumerable<String> value)`
  - `public Void set_StopBehavior(AnimationStopBehavior value)`
  - `private Void SetResourceUrlWithSequential()`
  - `protected Void UpdateResourceUrl()`
  - `public Int32 CacheSize { get; set; }`
  - `public Single FrameSpeedFactor { get; set; }`
  - `public Boolean HasValidSequentialResourceUrls { get; }`
  - `public Int32 PreloadingBatchSize { get; set; }`
  - `public Int32 RepeatCount { get; set; }`
  - `public AnimationRepeatMode RepeatMode { get; set; }`
  - `public IEnumerable<String> SequentialResourceUrls { get; set; }`
  - `public AnimationStopBehavior StopBehavior { get; set; }`

### class `AnimatedLayout`

- Base: `System.Object`
- Members:
  - `public static T ApplyAnimation(T layout, Int32 duration, AlphaFunction alphaFunction)`

### class `ApplicationResumedEvent`

- Base: `System.Object`
- Members:
  - `public static Void Add(EventHandler handler)`
  - `private static WeakEventManager EnsureProxy()`
  - `private static Void OnApplicationResumed(Object sender, EventArgs e)`
  - `public static Void Remove(EventHandler handler)`

### class `Clickable`

- Base: `Tizen.UI.Components.Pressable`
- Interfaces: `Tizen.UI.Components.IClickable`
- Members:
  - `public .ctor()`
  - `protected .ctor(IClickableVariables variables)`
  - `public event EventHandler<InputEventArgs> Clicked`
  - `public event EventHandler<InputEventArgs> LongPressed`
  - `public Void add_Clicked(EventHandler<InputEventArgs> value)`
  - `public Void add_LongPressed(EventHandler<InputEventArgs> value)`
  - `private Void ApplyVariables(IClickableVariables variables)`
  - `private Void BlockClickedByKey(Boolean shouldBlock)`
  - `private Void BlockClickedByTouch(Boolean shouldBlock)`
  - `protected Void Dispose(Boolean disposing)`
  - `public Action<Object, InputEventArgs> get_ClickedCommand()`
  - `public ClickKeyType get_ClickKeyType()`
  - `public Boolean get_IsClickable()`
  - `public Action<Object, InputEventArgs> get_LongPressedCommand()`
  - `public View GetTouchEffectTarget()`
  - `private Void OnAccessibilityActionReceived(Object sender, AccessibilityActionReceivedEventArgs e)`
  - `protected Boolean OnClicked(KeyDeviceClass device)`
  - `protected Void OnEnabled(Boolean enable)`
  - `protected Boolean OnKeyPressed(KeyEventArgs e)`
  - `protected Boolean OnLongPressed(KeyDeviceClass device)`
  - `private Void OnLongPressGestureDetected(Object sender, LongPressGestureDetectedEventArgs e)`
  - `protected Boolean OnPressed(KeyDeviceClass device)`
  - `protected Boolean OnReleased(KeyDeviceClass device)`
  - `private Void OnTapGestureDetected(Object sender, TapGestureDetectedEventArgs e)`
  - `protected Void OnUnfocused(Object sender, EventArgs e)`
  - `public Void remove_Clicked(EventHandler<InputEventArgs> value)`
  - `public Void remove_LongPressed(EventHandler<InputEventArgs> value)`
  - `public Void set_ClickedCommand(Action<Object, InputEventArgs> value)`
  - `public Void set_ClickKeyType(ClickKeyType value)`
  - `public Void set_IsClickable(Boolean value)`
  - `public Void set_LongPressedCommand(Action<Object, InputEventArgs> value)`
  - `private Boolean ShouldKeyPressTriggerClicked()`
  - `private Boolean ShouldKeyPressTriggerLongPressed()`
  - `private Boolean ShouldKeyReleaseTriggerClicked()`
  - `private Boolean ShouldTapTriggerClicked()`
  - `public Action<Object, InputEventArgs> ClickedCommand { get; set; }`
  - `public ClickKeyType ClickKeyType { get; set; }`
  - `public Boolean IsClickable { get; set; }`
  - `public Action<Object, InputEventArgs> LongPressedCommand { get; set; }`

### class `ClickableBox`

- Base: `Tizen.UI.Components.Clickable`
- Interfaces: `System.Collections.Generic.ICollection`1<Tizen.UI.View>`, `System.Collections.Generic.IEnumerable`1<Tizen.UI.View>`, `System.Collections.Generic.IList`1<Tizen.UI.View>`, `System.Collections.IEnumerable`, `Tizen.UI.Components.ILayoutBox`
- Members:
  - `protected .ctor()`
  - `public Void Clear()`
  - `public Boolean Contains(View item)`
  - `public Void CopyTo(View[] array, Int32 arrayIndex)`
  - `public IList<View> get_Children()`
  - `public Int32 get_Count()`
  - `protected IList<View> get_InnerList()`
  - `public Boolean get_IsReadOnly()`
  - `public View get_Item(Int32 index)`
  - `public Thickness get_Padding()`
  - `public IEnumerator<View> GetEnumerator()`
  - `public Int32 IndexOf(View item)`
  - `public Void Insert(Int32 index, View item)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_Item(Int32 index, View value)`
  - `public Void set_Padding(Thickness value)`
  - `private Void System.Collections.Generic.ICollection<Tizen.UI.View>.Add(View item)`
  - `private Boolean System.Collections.Generic.ICollection<Tizen.UI.View>.Remove(View item)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `public IList<View> Children { get; }`
  - `public Int32 Count { get; }`
  - `protected IList<View> InnerList { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public View Item { get; set; }`
  - `public Thickness Padding { get; set; }`

### class `ClickableGrid`

- Base: `Tizen.UI.Components.ClickableBox`
- Members:
  - `public .ctor()`
  - `public IList<GridColumnDefinition> get_ColumnDefinitions()`
  - `public Single get_ColumnSpacing()`
  - `protected IList<View> get_InnerList()`
  - `public Thickness get_Padding()`
  - `public IList<GridRowDefinition> get_RowDefinitions()`
  - `public Single get_RowSpacing()`
  - `public Void set_ColumnDefinitions(IList<GridColumnDefinition> value)`
  - `public Void set_ColumnSpacing(Single value)`
  - `public Void set_Padding(Thickness value)`
  - `public Void set_RowDefinitions(IList<GridRowDefinition> value)`
  - `public Void set_RowSpacing(Single value)`
  - `public IList<GridColumnDefinition> ColumnDefinitions { get; set; }`
  - `public Single ColumnSpacing { get; set; }`
  - `protected IList<View> InnerList { get; }`
  - `public Thickness Padding { get; set; }`
  - `public IList<GridRowDefinition> RowDefinitions { get; set; }`
  - `public Single RowSpacing { get; set; }`

### class `ClickableHStack`

- Base: `Tizen.UI.Components.ClickableBox`
- Members:
  - `public .ctor()`
  - `protected IList<View> get_InnerList()`
  - `public LayoutAlignment get_ItemAlignment()`
  - `public Thickness get_Padding()`
  - `public Single get_Spacing()`
  - `public Void set_ItemAlignment(LayoutAlignment value)`
  - `public Void set_Padding(Thickness value)`
  - `public Void set_Spacing(Single value)`
  - `protected IList<View> InnerList { get; }`
  - `public LayoutAlignment ItemAlignment { get; set; }`
  - `public Thickness Padding { get; set; }`
  - `public Single Spacing { get; set; }`

### class `ClickableVStack`

- Base: `Tizen.UI.Components.ClickableBox`
- Members:
  - `public .ctor()`
  - `protected IList<View> get_InnerList()`
  - `public LayoutAlignment get_ItemAlignment()`
  - `public Thickness get_Padding()`
  - `public Single get_Spacing()`
  - `public Void set_ItemAlignment(LayoutAlignment value)`
  - `public Void set_Padding(Thickness value)`
  - `public Void set_Spacing(Single value)`
  - `protected IList<View> InnerList { get; }`
  - `public LayoutAlignment ItemAlignment { get; set; }`
  - `public Thickness Padding { get; set; }`
  - `public Single Spacing { get; set; }`

### enum `ClickKeyType`

- Base: `System.Enum`
- Members:

### struct `ClosedRange`1`

- Base: `System.ValueType`
- Members:
  - `public .ctor(T startValue, T endValue)`
  - `public T Clamp(T value)`
  - `public Boolean Contains(T value)`
  - `public T get_EndValue()`
  - `public T get_StartValue()`
  - `public String ToString()`
  - `public T EndValue { get; }`
  - `public T StartValue { get; }`

### class `ColorCutQuantizer`

- Base: `System.Object`
- Members:
  - `private .ctor(ColorHistogram colorHistogram, Int32 maxColors)`
  - `public static ColorCutQuantizer FromBitmap(PixelBuffer pixelBuffer, Int32 maxColors)`
  - `public static ColorCutQuantizer FromBitmap(PixelBuffer pixelBuffer, Int32 maxColors, Int32 regionX, Int32 regionY, Int32 regionWidth, Int32 regionHeight)`
  - `private List<Swatch> GenerateAverageColors(CustomHeap<Vbox> vboxes)`
  - `public IEnumerable<Swatch> get_QuantizedColors()`
  - `private static Int32 GetBytesPerPixel(PixelFormat pixelFormat)`
  - `private static Boolean IsBlack(Span<Single> hslColor)`
  - `private static Boolean IsNearRedILine(Span<Single> hslColor)`
  - `private static Boolean IsWhite(Span<Single> hslColor)`
  - `private List<Swatch> QuantizePixels(Int32 maxColorIndex, Int32 maxColors)`
  - `private static Void RgbToHsl(Int32 red, Int32 green, Int32 blue, Span<Single> hsl)`
  - `private Boolean ShouldIgnoreColor(Int32 color)`
  - `private static Boolean ShouldIgnoreColor(Swatch color)`
  - `private static Boolean ShouldIgnoreColor(Span<Single> hslColor)`
  - `private Void SplitBoxes(CustomHeap<Vbox> queue, Int32 maxSize)`
  - `public IEnumerable<Swatch> QuantizedColors { get; }`

### class `ColorHistogram`

- Base: `System.Object`
- Members:
  - `public .ctor(Int32[] pixels)`
  - `private Void CountFrequencies(Int32[] pixels)`
  - `private static Int32 DistinctCount(Int32[] pixels)`
  - `public Int32[] GetColorCounts()`
  - `public Int32[] GetColors()`
  - `public Int32 GetNumberOfColors()`

### class `ColorUtils`

- Base: `System.Object`
- Members:
  - `public static Double CalculateContrast(Int32 foreground, Int32 background)`
  - `public static Double CalculateLuminance(Int32 color)`
  - `public static Int32 CalculateMinimumAlpha(Int32 foreground, Int32 background, Single minContrastRatio)`
  - `private static Void ColorToXyz(Int32 color, Double[] outXyz)`
  - `private static Int32 CompositeAlpha(Int32 foregroundAlpha, Int32 backgroundAlpha)`
  - `public static Int32 CompositeColors(Int32 foreground, Int32 background)`
  - `private static Int32 CompositeComponent(Int32 fgC, Int32 fgA, Int32 bgC, Int32 bgA, Int32 alpha)`
  - `private static Void RgbToXyz(Int32 red, Int32 green, Int32 blue, Double[] outXyz)`
  - `public static Int32 SetAlphaComponent(Int32 color, Int32 alpha)`
  - `public static Color ToColor(Int32 colorValue)`

### class `ComponentMathF`

- Base: `System.Object`
- Members:
  - `public static Single DigitRound(Single a)`
  - `public static Boolean EvenlyDividedBy(Single a, Single b)`
  - `public static Boolean IsCertainlyGreaterThan(Single a, Single b)`
  - `public static Single MaxNumber(Single a, Single b)`
  - `public static Single MinNumber(Single a, Single b)`
  - `public static Boolean NearlyEquals(Single a, Single b)`
  - `public static Single RoundToNearestMultiple(Single a, Single b)`

### class `DestroyUtility`

- Base: `System.Object`
- Members:
  - `public static Void Destroy(T& disposable)`
  - `public static Void Destroy(IEnumerable<T> enumerable)`
  - `public static Void Destroy(IEnumerable enumerable)`

### class `Dimension`

- Base: `System.Object`
- Members:
  - `public static Boolean IsExplicitSet(Single value)`
  - `public static Boolean IsMaximumSet(Single value)`
  - `public static Boolean IsMinimumSet(Single value)`
  - `public static Single ResolveMinimum(Single value)`

### class `DisabledEffect`

- Base: `Tizen.UI.Components.UIAttachable`
- Members:
  - `private static .cctor()`
  - `public .ctor(Single disabledOpacity)`
  - `private Void ApplyDisabledEffect(View view)`
  - `public Void OnAttached(View view)`
  - `public Void OnDetached(View view)`
  - `private Void OnDisabledChanged(View sender, UIStateChangedEventArgs e)`
  - `private Void RestoreNormalState(View view)`

### class `DummyFontFamilyLoader`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.IFontFamilyLoader`
- Members:
  - `private .ctor()`
  - `public event EventHandler FontFamilyChanged`
  - `public Void add_FontFamilyChanged(EventHandler value)`
  - `public String get_FontFamily()`
  - `public static DummyFontFamilyLoader get_Instance()`
  - `public Void remove_FontFamilyChanged(EventHandler value)`
  - `public Void set_FontFamily(String value)`
  - `public String FontFamily { get; set; }`
  - `public DummyFontFamilyLoader Instance { get; }`

### class `DummyFontScaleLoader`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.IFontScaleLoader`
- Members:
  - `private .ctor()`
  - `public event EventHandler FontScaleChanged`
  - `public Void add_FontScaleChanged(EventHandler value)`
  - `public Single get_FontScale()`
  - `public static DummyFontScaleLoader get_Instance()`
  - `public Void remove_FontScaleChanged(EventHandler value)`
  - `public Void set_FontScale(Single value)`
  - `public Single FontScale { get; set; }`
  - `public DummyFontScaleLoader Instance { get; }`

### class `DummyThemeLoader`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.IThemeLoader`
- Members:
  - `public .ctor(String[] themeResourceDirectories)`
  - `public event EventHandler ThemeChanged`
  - `public Void add_ThemeChanged(EventHandler value)`
  - `public String get_CurrentResourcePath()`
  - `public String get_CurrentThemeId()`
  - `public static DummyThemeLoader get_Instance()`
  - `private String GetCurrentThemeResourcePath()`
  - `public IDictionary<String, Color> LoadColorTable()`
  - `public Void NextTheme()`
  - `public Void remove_ThemeChanged(EventHandler value)`
  - `public Void ResetTheme()`
  - `private Void set_CurrentResourcePath(String value)`
  - `public Void SwitchTheme(Int32 index)`
  - `public String CurrentResourcePath { get; set; }`
  - `public String CurrentThemeId { get; }`
  - `public DummyThemeLoader Instance { get; }`

### enum `EdgeDirection`

- Base: `System.Enum`
- Members:

### enum `EdgeState`

- Base: `System.Enum`
- Members:

### class `EnumHelper`

- Base: `System.Object`
- Members:
  - `public static TEnum Parse(String value, TEnum fallback, Boolean ignoreCase)`

### class `ExceptionHelper`

- Base: `System.Object`
- Members:
  - `public static Void ThrowIfNullOrEmpty(String argument, String paramName)`
  - `public static Void ThrowIfNullOrWhiteSpace(String argument, String paramName)`

### enum `FloatingOrigin`

- Base: `System.Enum`
- Members:

### class `GroupSelectable`

- Base: `Tizen.UI.Components.Selectable`
- Interfaces: `Tizen.UI.Components.IGroupSelectable`, `Tizen.UI.Components.ISelectable`
- Members:
  - `public .ctor()`
  - `protected .ctor(IClickableVariables variables)`
  - `protected Void Dispose(Boolean disposing)`
  - `public SelectionGroup get_Group()`
  - `public String get_GroupName()`
  - `protected Boolean get_IsDeselectable()`
  - `public Boolean get_IsGrouped()`
  - `private Void OnAddedToWindow(Object sender, EventArgs e)`
  - `public Void set_GroupName(String value)`
  - `private String Tizen.UI.Components.IGroupSelectable.get_Name()`
  - `public SelectionGroup Group { get; }`
  - `public String GroupName { get; set; }`
  - `protected Boolean IsDeselectable { get; }`
  - `public Boolean IsGrouped { get; }`

### class `GroupSelectableBox`

- Base: `Tizen.UI.Components.GroupSelectable`
- Interfaces: `System.Collections.Generic.ICollection`1<Tizen.UI.View>`, `System.Collections.Generic.IEnumerable`1<Tizen.UI.View>`, `System.Collections.Generic.IList`1<Tizen.UI.View>`, `System.Collections.IEnumerable`, `Tizen.UI.Components.ILayoutBox`
- Members:
  - `protected .ctor()`
  - `public Void Clear()`
  - `public Boolean Contains(View item)`
  - `public Void CopyTo(View[] array, Int32 arrayIndex)`
  - `public IList<View> get_Children()`
  - `public Int32 get_Count()`
  - `protected IList<View> get_InnerList()`
  - `public Boolean get_IsReadOnly()`
  - `public View get_Item(Int32 index)`
  - `public Thickness get_Padding()`
  - `public IEnumerator<View> GetEnumerator()`
  - `public Int32 IndexOf(View item)`
  - `public Void Insert(Int32 index, View item)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_Item(Int32 index, View value)`
  - `public Void set_Padding(Thickness value)`
  - `private Void System.Collections.Generic.ICollection<Tizen.UI.View>.Add(View item)`
  - `private Boolean System.Collections.Generic.ICollection<Tizen.UI.View>.Remove(View item)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `public IList<View> Children { get; }`
  - `public Int32 Count { get; }`
  - `protected IList<View> InnerList { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public View Item { get; set; }`
  - `public Thickness Padding { get; set; }`

### class `GroupSelectionChangedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor(IGroupSelectable previous, IGroupSelectable current)`
  - `public IGroupSelectable get_Current()`
  - `public IGroupSelectable get_Previous()`
  - `public IGroupSelectable Current { get; }`
  - `public IGroupSelectable Previous { get; }`

### interface `IAnchoredModal`

- Members:
  - `public event EventHandler Dismissed`
  - `public Void add_Dismissed(EventHandler value)`
  - `public ModalPivot get_ModalPivot()`
  - `public Void Post(Window window, Rect anchorBounds)`
  - `public Void remove_Dismissed(EventHandler value)`
  - `public ModalPivot ModalPivot { get; }`

### interface `IAnimatedImage`

- Members:
  - `public Boolean get_IsLooping()`
  - `public Int32 get_RepeatCount()`
  - `public AnimationStopBehavior get_StopBehavior()`
  - `public Void Pause()`
  - `public Void Play()`
  - `public Void set_IsLooping(Boolean value)`
  - `public Void set_RepeatCount(Int32 value)`
  - `public Void set_StopBehavior(AnimationStopBehavior value)`
  - `public Void Stop()`
  - `public Boolean IsLooping { get; set; }`
  - `public Int32 RepeatCount { get; set; }`
  - `public AnimationStopBehavior StopBehavior { get; set; }`

### interface `IClickable`

- Members:
  - `public event EventHandler<InputEventArgs> Clicked`
  - `public Void add_Clicked(EventHandler<InputEventArgs> value)`
  - `public Action<Object, InputEventArgs> get_ClickedCommand()`
  - `public Void remove_Clicked(EventHandler<InputEventArgs> value)`
  - `public Void set_ClickedCommand(Action<Object, InputEventArgs> value)`
  - `public Action<Object, InputEventArgs> ClickedCommand { get; set; }`

### interface `IClickableVariables`

- Members:
  - `public UIAttachable get_TouchEffect()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TouchEffect(UIAttachable value)`
  - `public UIAttachable TouchEffect { get; set; }`

### interface `IColorProvider`

- Members:
  - `public event EventHandler DominantColorChanged`
  - `public Void add_DominantColorChanged(EventHandler value)`
  - `public Color get_DominantColor()`
  - `public Void remove_DominantColorChanged(EventHandler value)`
  - `public Color DominantColor { get; }`

### enum `IconPlacement`

- Base: `System.Enum`
- Members:

### interface `IDecoratableText`

- Members:
  - `public Nullable<Outline> get_Outline()`
  - `public Nullable<Strikethrough> get_Strikethrough()`
  - `public Nullable<TextShadow> get_TextShadow()`
  - `public Nullable<Underline> get_Underline()`
  - `public Void set_Outline(Nullable<Outline> value)`
  - `public Void set_Strikethrough(Nullable<Strikethrough> value)`
  - `public Void set_TextShadow(Nullable<TextShadow> value)`
  - `public Void set_Underline(Nullable<Underline> value)`
  - `public Nullable<Outline> Outline { get; set; }`
  - `public Nullable<Strikethrough> Strikethrough { get; set; }`
  - `public Nullable<TextShadow> TextShadow { get; set; }`
  - `public Nullable<Underline> Underline { get; set; }`

### interface `IDoubleTitle`

- Interfaces: `Tizen.UI.Components.ITitle`
- Members:
  - `public String get_Subtitle()`
  - `public Void set_Subtitle(String value)`
  - `public String Subtitle { get; set; }`

### interface `IEdgeEffect`

- Members:
  - `public Void Finish()`
  - `public EdgeDirection get_EdgeDirection()`
  - `public Single get_MaxDistance()`
  - `public View get_SourceView()`
  - `public EdgeState get_State()`
  - `public Void OnAbsorb(Single velocity)`
  - `public Single OnPull(Single deltaDistance, Single displacement)`
  - `public Void OnRelease()`
  - `public Void set_EdgeDirection(EdgeDirection value)`
  - `public Void set_MaxDistance(Single value)`
  - `public Void set_SourceView(View value)`
  - `public EdgeDirection EdgeDirection { get; set; }`
  - `public Single MaxDistance { get; set; }`
  - `public View SourceView { get; set; }`
  - `public EdgeState State { get; }`

### interface `IFlexibleText`

- Members:
  - `public AutoFontSize get_AutoFontSize()`
  - `public Single get_FontSize()`
  - `public TextOverflow get_TextOverflow()`
  - `public Void set_AutoFontSize(AutoFontSize value)`
  - `public Void set_FontSize(Single value)`
  - `public Void set_TextOverflow(TextOverflow value)`
  - `public AutoFontSize AutoFontSize { get; set; }`
  - `public Single FontSize { get; set; }`
  - `public TextOverflow TextOverflow { get; set; }`

### interface `IFontFamilyLoader`

- Members:
  - `public event EventHandler FontFamilyChanged`
  - `public Void add_FontFamilyChanged(EventHandler value)`
  - `public String get_FontFamily()`
  - `public Void remove_FontFamilyChanged(EventHandler value)`
  - `public String FontFamily { get; }`

### interface `IFontScaleLoader`

- Members:
  - `public event EventHandler FontScaleChanged`
  - `public Void add_FontScaleChanged(EventHandler value)`
  - `public Single get_FontScale()`
  - `public Void remove_FontScaleChanged(EventHandler value)`
  - `public Single FontScale { get; }`

### interface `IGroupSelectable`

- Interfaces: `Tizen.UI.Components.ISelectable`
- Members:
  - `public SelectionGroup get_Group()`
  - `public String get_GroupName()`
  - `public Boolean get_IsGrouped()`
  - `public String get_Name()`
  - `public Void set_GroupName(String value)`
  - `public SelectionGroup Group { get; }`
  - `public String GroupName { get; set; }`
  - `public Boolean IsGrouped { get; }`
  - `public String Name { get; }`

### interface `IImage`

- Members:
  - `public event EventHandler ResourceReady`
  - `public Void add_ResourceReady(EventHandler value)`
  - `public String get_AlphaMaskUrl()`
  - `public Boolean get_CropToMask()`
  - `public ImageLoaderFittingMode get_FittingMode()`
  - `public ImageLoadingStatus get_LoadingStatus()`
  - `public Color get_MultipliedColor()`
  - `public String get_ResourceUrl()`
  - `public Boolean get_SynchronousLoading()`
  - `public Void Reload()`
  - `public Void remove_ResourceReady(EventHandler value)`
  - `public Void set_AlphaMaskUrl(String value)`
  - `public Void set_CropToMask(Boolean value)`
  - `public Void set_FittingMode(ImageLoaderFittingMode value)`
  - `public Void set_MultipliedColor(Color value)`
  - `public Void set_ResourceUrl(String value)`
  - `public Void set_SynchronousLoading(Boolean value)`
  - `public String AlphaMaskUrl { get; set; }`
  - `public Boolean CropToMask { get; set; }`
  - `public ImageLoaderFittingMode FittingMode { get; set; }`
  - `public ImageLoadingStatus LoadingStatus { get; }`
  - `public Color MultipliedColor { get; set; }`
  - `public String ResourceUrl { get; set; }`
  - `public Boolean SynchronousLoading { get; set; }`

### interface `ILayoutBox`

- Members:
  - `public IList<View> get_Children()`
  - `public Thickness get_Padding()`
  - `public Void set_Padding(Thickness value)`
  - `public IList<View> Children { get; }`
  - `public Thickness Padding { get; set; }`

### interface `IModalContainer`

- Members:
  - `public View get_ModalContent()`
  - `public View get_Scrim()`
  - `public Void set_ModalContent(View value)`
  - `public Void set_Scrim(View value)`
  - `public View ModalContent { get; set; }`
  - `public View Scrim { get; set; }`

### interface `INavigateBackHandler`

- Members:
  - `public Boolean HandleNavigateBack()`

### interface `INavigation`

- Members:
  - `public IReadOnlyList<View> get_ModalStack()`
  - `public IReadOnlyList<View> get_NavigationStack()`
  - `public Void InsertBefore(View view, View before)`
  - `public Boolean NavigateBack()`
  - `public Task<View> PopAsync()`
  - `public Task<View> PopAsync(Boolean animated)`
  - `public Task<View> PopModalAsync()`
  - `public Task<View> PopModalAsync(Boolean animated)`
  - `public Task PushAsync(View view)`
  - `public Task PushAsync(View view, Boolean animated)`
  - `public Task PushModalAsync(View view)`
  - `public Task PushModalAsync(View view, Boolean animated)`
  - `public Void Remove(View view)`
  - `public IReadOnlyList<View> ModalStack { get; }`
  - `public IReadOnlyList<View> NavigationStack { get; }`

### interface `INavigationAnimation`

- Members:
  - `public Func<View, View, INavigationAnimationController> get_PopAnimation()`
  - `public Func<View, View, INavigationAnimationController> get_PopModalAnimation()`
  - `public Func<View, View, INavigationAnimationController> get_PushAnimation()`
  - `public Func<View, View, INavigationAnimationController> get_PushModalAnimation()`
  - `public Func<View, View, INavigationAnimationController> PopAnimation { get; }`
  - `public Func<View, View, INavigationAnimationController> PopModalAnimation { get; }`
  - `public Func<View, View, INavigationAnimationController> PushAnimation { get; }`
  - `public Func<View, View, INavigationAnimationController> PushModalAnimation { get; }`

### interface `INavigationAnimationController`

- Members:
  - `public Task PlayAsync()`
  - `public Void Stop()`

### interface `INavigationTransition`

- Members:
  - `public Void DidAppear(Boolean byPopNavigation)`
  - `public Void DidDisappear(Boolean byPopNavigation)`
  - `public Void WillAppear(Boolean byPopNavigation)`
  - `public Void WillDisappear(Boolean byPopNavigation)`

### class `InputEditorImpl`

- Base: `Tizen.UI.Components.InputTextBaseImpl`
- Members:
  - `public .ctor()`
  - `protected Void ConnectAnchorClicked(IntPtr handler)`
  - `protected Void ConnectCursorMoved(IntPtr handler)`
  - `protected Void ConnectInputFiltered(IntPtr handler)`
  - `protected Void ConnectMaximumLengthReached(IntPtr handler)`
  - `protected Void ConnectSelectionChanged(IntPtr handler)`
  - `protected Void ConnectSelectionCleared(IntPtr handler)`
  - `protected Void ConnectSelectionStarted(IntPtr handler)`
  - `protected Void ConnectTextChanged(IntPtr handler)`
  - `private Void ConnectTextEditorSignal(IntPtr signal, IntPtr handler)`
  - `protected IntPtr CreateNativeHandle()`
  - `protected Void DisconnectAnchorClicked(IntPtr handler)`
  - `protected Void DisconnectCursorMoved(IntPtr handler)`
  - `protected Void DisconnectInputFiltered(IntPtr handler)`
  - `protected Void DisconnectMaximumLengthReached(IntPtr handler)`
  - `protected Void DisconnectSelectionChanged(IntPtr handler)`
  - `protected Void DisconnectSelectionCleared(IntPtr handler)`
  - `protected Void DisconnectSelectionStarted(IntPtr handler)`
  - `protected Void DisconnectTextChanged(IntPtr handler)`
  - `private Void DisconnectTextEditorSignal(IntPtr signal, IntPtr handler)`
  - `protected Void DoClearSelection()`
  - `protected InputMethodContext DoCreateInputMethodContext()`
  - `protected Void DoSelectText(Int32 startIndex, Int32 endIndex)`
  - `protected Void DoSelectWholeText()`
  - `public Boolean get_IsAbsoluteLineHeight()`
  - `public LineBreakMode get_LineBreakMode()`
  - `public Int32 get_LineCount()`
  - `public Single get_LineHeight()`
  - `protected Int32 GetCursorBlinkDurationPropertyIndex()`
  - `protected Int32 GetCursorBlinkEnabledPropertyIndex()`
  - `protected Int32 GetCursorBlinkIntervalPropertyIndex()`
  - `protected Int32 GetCursorColorPropertyIndex()`
  - `protected Int32 GetCursorPositionPropertyIndex()`
  - `protected Int32 GetCursorWidthPropertyIndex()`
  - `protected Int32 GetEllipsisPositionPropertyIndex()`
  - `protected Int32 GetEllipsisPropertyIndex()`
  - `protected Int32 GetEnableGrabHandlePopupPropertyIndex()`
  - `protected Int32 GetEnableGrabHandlePropertyIndex()`
  - `protected Int32 GetFontFamilyPropertyIndex()`
  - `protected Int32 GetFontSizeScalePropertyIndex()`
  - `protected Int32 GetFontStylePropertyIndex()`
  - `protected Int32 GetHorizontalAlignmentPropertyIndex()`
  - `protected Int32 GetInputColorPropertyIndex()`
  - `protected Int32 GetInputFilterPropertyIndex()`
  - `protected Int32 GetInputFontFamilyPropertyIndex()`
  - `protected Int32 GetInputFontStylePropertyIndex()`
  - `protected Int32 GetInputMethodSettingPropertyIndex()`
  - `protected Int32 GetInputPointSizePropertyIndex()`
  - `protected Int32 GetIsEditablePropertyIndex()`
  - `protected Int32 GetMarkupEnabledPropertyIndex()`
  - `protected Int32 GetMaximumLengthPropertyIndex()`
  - `protected Int32 GetOutlinePropertyIndex()`
  - `protected Int32 GetPixelSizePropertyIndex()`
  - `protected Int32 GetPlaceholderColorPropertyIndex()`
  - `protected Int32 GetPlaceholderPropertyIndex()`
  - `protected Int32 GetSecondaryCursorColorPropertyIndex()`
  - `protected Int32 GetSelectedTextEndIndexPropertyIndex()`
  - `protected Int32 GetSelectedTextPropertyIndex()`
  - `protected Int32 GetSelectedTextStartIndexPropertyIndex()`
  - `protected Int32 GetSelectionColorPropertyIndex()`
  - `protected Int32 GetSelectionEnabledPropertyIndex()`
  - `protected Int32 GetSelectionHandleLeftImagePropertyIndex()`
  - `protected Int32 GetSelectionHandlePressedLeftImagePropertyIndex()`
  - `protected Int32 GetSelectionHandlePressedRightImagePropertyIndex()`
  - `protected Int32 GetSelectionHandleRightImagePropertyIndex()`
  - `protected Int32 GetStrikethroughPropertyIndex()`
  - `protected Int32 GetTextColorPropertyIndex()`
  - `protected Int32 GetTextDropShadowPropertyIndex()`
  - `protected Int32 GetTextPropertyIndex()`
  - `protected Int32 GetUnderlinePropertyIndex()`
  - `protected Int32 GetVerticalAlignmentPropertyIndex()`
  - `protected Int32 RegisterFontVariationProperty(String tag)`
  - `public Void set_IsAbsoluteLineHeight(Boolean value)`
  - `public Void set_LineBreakMode(LineBreakMode value)`
  - `public Void set_LineHeight(Single value)`
  - `private Void SetMinLineSize(Single value)`
  - `private Void SetRelativeLineHeight(Single value)`
  - `private Void UpdateLineHeight()`
  - `public Boolean IsAbsoluteLineHeight { get; set; }`
  - `public LineBreakMode LineBreakMode { get; set; }`
  - `public Int32 LineCount { get; }`
  - `public Single LineHeight { get; set; }`

### class `InputEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor(KeyDeviceClass device)`
  - `public Boolean get_Handled()`
  - `public KeyDeviceClass get_InputDevice()`
  - `public Void set_Handled(Boolean value)`
  - `protected Void set_InputDevice(KeyDeviceClass value)`
  - `public Boolean Handled { get; set; }`
  - `public KeyDeviceClass InputDevice { get; set; }`

### class `InputFieldImpl`

- Base: `Tizen.UI.Components.InputTextBaseImpl`
- Members:
  - `public .ctor()`
  - `protected Void ConnectAnchorClicked(IntPtr handler)`
  - `protected Void ConnectCursorMoved(IntPtr handler)`
  - `protected Void ConnectInputFiltered(IntPtr handler)`
  - `protected Void ConnectMaximumLengthReached(IntPtr handler)`
  - `protected Void ConnectSelectionChanged(IntPtr handler)`
  - `protected Void ConnectSelectionCleared(IntPtr handler)`
  - `protected Void ConnectSelectionStarted(IntPtr handler)`
  - `protected Void ConnectTextChanged(IntPtr handler)`
  - `private Void ConnectTextFieldSignal(IntPtr signal, IntPtr handler)`
  - `protected IntPtr CreateNativeHandle()`
  - `protected Void DisconnectAnchorClicked(IntPtr handler)`
  - `protected Void DisconnectCursorMoved(IntPtr handler)`
  - `protected Void DisconnectInputFiltered(IntPtr handler)`
  - `protected Void DisconnectMaximumLengthReached(IntPtr handler)`
  - `protected Void DisconnectSelectionChanged(IntPtr handler)`
  - `protected Void DisconnectSelectionCleared(IntPtr handler)`
  - `protected Void DisconnectSelectionStarted(IntPtr handler)`
  - `protected Void DisconnectTextChanged(IntPtr handler)`
  - `private Void DisconnectTextFieldSignal(IntPtr signal, IntPtr handler)`
  - `protected Void DoClearSelection()`
  - `protected InputMethodContext DoCreateInputMethodContext()`
  - `protected Void DoSelectText(Int32 startIndex, Int32 endIndex)`
  - `protected Void DoSelectWholeText()`
  - `public HiddenInputMode get_PasswordMode()`
  - `public Boolean get_ShowPlaceholderOnFocus()`
  - `protected Int32 GetCursorBlinkDurationPropertyIndex()`
  - `protected Int32 GetCursorBlinkEnabledPropertyIndex()`
  - `protected Int32 GetCursorBlinkIntervalPropertyIndex()`
  - `protected Int32 GetCursorColorPropertyIndex()`
  - `protected Int32 GetCursorPositionPropertyIndex()`
  - `protected Int32 GetCursorWidthPropertyIndex()`
  - `protected Int32 GetEllipsisPositionPropertyIndex()`
  - `protected Int32 GetEllipsisPropertyIndex()`
  - `protected Int32 GetEnableGrabHandlePopupPropertyIndex()`
  - `protected Int32 GetEnableGrabHandlePropertyIndex()`
  - `protected Int32 GetFontFamilyPropertyIndex()`
  - `protected Int32 GetFontSizeScalePropertyIndex()`
  - `protected Int32 GetFontStylePropertyIndex()`
  - `protected Int32 GetHorizontalAlignmentPropertyIndex()`
  - `protected Int32 GetInputColorPropertyIndex()`
  - `protected Int32 GetInputFilterPropertyIndex()`
  - `protected Int32 GetInputFontFamilyPropertyIndex()`
  - `protected Int32 GetInputFontStylePropertyIndex()`
  - `protected Int32 GetInputMethodSettingPropertyIndex()`
  - `protected Int32 GetInputPointSizePropertyIndex()`
  - `protected Int32 GetIsEditablePropertyIndex()`
  - `protected Int32 GetMarkupEnabledPropertyIndex()`
  - `protected Int32 GetMaximumLengthPropertyIndex()`
  - `protected Int32 GetOutlinePropertyIndex()`
  - `protected Int32 GetPixelSizePropertyIndex()`
  - `protected Int32 GetPlaceholderColorPropertyIndex()`
  - `protected Int32 GetPlaceholderFocusedPropertyIndex()`
  - `protected Int32 GetPlaceholderPropertyIndex()`
  - `protected Int32 GetSecondaryCursorColorPropertyIndex()`
  - `protected Int32 GetSelectedTextEndIndexPropertyIndex()`
  - `protected Int32 GetSelectedTextPropertyIndex()`
  - `protected Int32 GetSelectedTextStartIndexPropertyIndex()`
  - `protected Int32 GetSelectionColorPropertyIndex()`
  - `protected Int32 GetSelectionEnabledPropertyIndex()`
  - `protected Int32 GetSelectionHandleLeftImagePropertyIndex()`
  - `protected Int32 GetSelectionHandlePressedLeftImagePropertyIndex()`
  - `protected Int32 GetSelectionHandlePressedRightImagePropertyIndex()`
  - `protected Int32 GetSelectionHandleRightImagePropertyIndex()`
  - `protected Int32 GetStrikethroughPropertyIndex()`
  - `protected Int32 GetTextColorPropertyIndex()`
  - `protected Int32 GetTextDropShadowPropertyIndex()`
  - `protected Int32 GetTextPropertyIndex()`
  - `protected Int32 GetUnderlinePropertyIndex()`
  - `protected Int32 GetVerticalAlignmentPropertyIndex()`
  - `protected Void OnPlaceholderChanged(String placeholder)`
  - `protected Int32 RegisterFontVariationProperty(String tag)`
  - `public Void set_PasswordMode(HiddenInputMode value)`
  - `public Void set_ShowPlaceholderOnFocus(Boolean value)`
  - `private Void SetPlaceholderFocused(String placeholder)`
  - `public HiddenInputMode PasswordMode { get; set; }`
  - `public Boolean ShowPlaceholderOnFocus { get; set; }`

### class `InputTextBaseImpl`

- Base: `Tizen.UI.Components.TextBaseImpl`
- Members:
  - `public .ctor()`
  - `private event EventHandler _cursorMoved`
  - `private event EventHandler _maximumLengthReached`
  - `private event EventHandler _selectionChanged`
  - `private event EventHandler _selectionCleared`
  - `private event EventHandler _selectionStarted`
  - `private event EventHandler _textChanged`
  - `public event EventHandler CursorMoved`
  - `public event EventHandler MaximumLengthReached`
  - `public event EventHandler SelectionChanged`
  - `public event EventHandler SelectionCleared`
  - `public event EventHandler SelectionStarted`
  - `public event EventHandler TextChanged`
  - `private Void add__cursorMoved(EventHandler value)`
  - `private Void add__maximumLengthReached(EventHandler value)`
  - `private Void add__selectionChanged(EventHandler value)`
  - `private Void add__selectionCleared(EventHandler value)`
  - `private Void add__selectionStarted(EventHandler value)`
  - `private Void add__textChanged(EventHandler value)`
  - `public Void add_CursorMoved(EventHandler value)`
  - `public Void add_MaximumLengthReached(EventHandler value)`
  - `public Void add_SelectionChanged(EventHandler value)`
  - `public Void add_SelectionCleared(EventHandler value)`
  - `public Void add_SelectionStarted(EventHandler value)`
  - `public Void add_TextChanged(EventHandler value)`
  - `public Void ClearSelection()`
  - `protected Void ConnectCursorMoved(IntPtr handler)`
  - `protected Void ConnectInputFiltered(IntPtr handler)`
  - `protected Void ConnectMaximumLengthReached(IntPtr handler)`
  - `protected Void ConnectSelectionChanged(IntPtr handler)`
  - `protected Void ConnectSelectionCleared(IntPtr handler)`
  - `protected Void ConnectSelectionStarted(IntPtr handler)`
  - `protected Void ConnectTextChanged(IntPtr handler)`
  - `protected Void DisconnectCursorMoved(IntPtr handler)`
  - `protected Void DisconnectInputFiltered(IntPtr handler)`
  - `protected Void DisconnectMaximumLengthReached(IntPtr handler)`
  - `protected Void DisconnectSelectionChanged(IntPtr handler)`
  - `protected Void DisconnectSelectionCleared(IntPtr handler)`
  - `protected Void DisconnectSelectionStarted(IntPtr handler)`
  - `protected Void DisconnectTextChanged(IntPtr handler)`
  - `protected Void DoClearSelection()`
  - `protected InputMethodContext DoCreateInputMethodContext()`
  - `protected Void DoSelectText(Int32 startIndex, Int32 endIndex)`
  - `protected Void DoSelectWholeText()`
  - `public Void EnableGrabHandle(Boolean enable)`
  - `public Void EnableGrabHandlePopup(Boolean enable)`
  - `public Int32 get_CursorPosition()`
  - `public Color get_InputColor()`
  - `public String get_InputFontFamily()`
  - `public Single get_InputFontSize()`
  - `public FontSlant get_InputFontSlant()`
  - `private FontStyle get_InputFontStyle()`
  - `public FontWeight get_InputFontWeight()`
  - `public FontWidth get_InputFontWidth()`
  - `public InputMethodContext get_InputMethodContext()`
  - `private Single get_InputPointSize()`
  - `public Boolean get_IsEditable()`
  - `public Int32 get_MaximumLength()`
  - `public String get_Placeholder()`
  - `public Color get_PlaceholderColor()`
  - `public String get_SelectedText()`
  - `public ClosedRange<Int32> get_SelectedTextIndex()`
  - `public Boolean get_SelectionEnabled()`
  - `protected Int32 GetCursorBlinkDurationPropertyIndex()`
  - `protected Int32 GetCursorBlinkEnabledPropertyIndex()`
  - `protected Int32 GetCursorBlinkIntervalPropertyIndex()`
  - `protected Int32 GetCursorColorPropertyIndex()`
  - `protected Int32 GetCursorPositionPropertyIndex()`
  - `protected Int32 GetCursorWidthPropertyIndex()`
  - `private FontStyle GetDefaultInputFontStyle()`
  - `protected Int32 GetEnableGrabHandlePopupPropertyIndex()`
  - `protected Int32 GetEnableGrabHandlePropertyIndex()`
  - `protected Int32 GetInputColorPropertyIndex()`
  - `protected Int32 GetInputFilterPropertyIndex()`
  - `protected Int32 GetInputFontFamilyPropertyIndex()`
  - `protected Int32 GetInputFontStylePropertyIndex()`
  - `protected Int32 GetInputMethodSettingPropertyIndex()`
  - `protected Int32 GetInputPointSizePropertyIndex()`
  - `protected Int32 GetIsEditablePropertyIndex()`
  - `protected Int32 GetMaximumLengthPropertyIndex()`
  - `protected Int32 GetPlaceholderColorPropertyIndex()`
  - `protected Int32 GetPlaceholderPropertyIndex()`
  - `protected Int32 GetSecondaryCursorColorPropertyIndex()`
  - `protected Int32 GetSelectedTextEndIndexPropertyIndex()`
  - `protected Int32 GetSelectedTextPropertyIndex()`
  - `protected Int32 GetSelectedTextStartIndexPropertyIndex()`
  - `protected Int32 GetSelectionColorPropertyIndex()`
  - `protected Int32 GetSelectionEnabledPropertyIndex()`
  - `protected Int32 GetSelectionHandleLeftImagePropertyIndex()`
  - `protected Int32 GetSelectionHandlePressedLeftImagePropertyIndex()`
  - `protected Int32 GetSelectionHandlePressedRightImagePropertyIndex()`
  - `protected Int32 GetSelectionHandleRightImagePropertyIndex()`
  - `private Void OnCursorMoved(IntPtr handle, UInt32 oldPosition)`
  - `protected Void OnInitialized()`
  - `private Void OnMaximumLengthReached(IntPtr handle)`
  - `protected Void OnPlaceholderChanged(String placeholder)`
  - `private Void OnSelectionChanged(IntPtr handle)`
  - `private Void OnSelectionCleared(IntPtr handle)`
  - `private Void OnSelectionStarted(IntPtr handle)`
  - `private Void OnTextChanged(IntPtr handle)`
  - `private Void remove__cursorMoved(EventHandler value)`
  - `private Void remove__maximumLengthReached(EventHandler value)`
  - `private Void remove__selectionChanged(EventHandler value)`
  - `private Void remove__selectionCleared(EventHandler value)`
  - `private Void remove__selectionStarted(EventHandler value)`
  - `private Void remove__textChanged(EventHandler value)`
  - `public Void remove_CursorMoved(EventHandler value)`
  - `public Void remove_MaximumLengthReached(EventHandler value)`
  - `public Void remove_SelectionChanged(EventHandler value)`
  - `public Void remove_SelectionCleared(EventHandler value)`
  - `public Void remove_SelectionStarted(EventHandler value)`
  - `public Void remove_TextChanged(EventHandler value)`
  - `public Void SelectText(Int32 startIndex, Int32 endIndex)`
  - `public Void SelectWholeText()`
  - `public Void set_CursorPosition(Int32 value)`
  - `public Void set_InputColor(Color value)`
  - `public Void set_InputFontFamily(String value)`
  - `public Void set_InputFontSize(Single value)`
  - `public Void set_InputFontSlant(FontSlant value)`
  - `private Void set_InputFontStyle(FontStyle value)`
  - `public Void set_InputFontWeight(FontWeight value)`
  - `public Void set_InputFontWidth(FontWidth value)`
  - `private Void set_InputPointSize(Single value)`
  - `public Void set_IsEditable(Boolean value)`
  - `public Void set_MaximumLength(Int32 value)`
  - `public Void set_Placeholder(String value)`
  - `public Void set_PlaceholderColor(Color value)`
  - `public Void set_SelectionEnabled(Boolean value)`
  - `public Void SetCursorBlink(Single interval, Single duration)`
  - `public Void SetCursorColor(Color value)`
  - `public Void SetCursorWidth(Int32 value)`
  - `internal Void SetInputFilter(String include, String exclude)`
  - `public Void SetInputMethodActionButtonTitle(ActionButtonTitle actionButtonTitle)`
  - `public Void SetInputMethodCapitalMode(AutoCapital capitalMode)`
  - `public Void SetInputMethodPanelType(PanelLayout panelLayout)`
  - `private Void SetInputMethodSetting(InputMethodSetting setting)`
  - `public Void SetSecondaryCursorColor(Color value)`
  - `public Void SetSelectionColor(Color color)`
  - `public Void SetSelectionHandleImage(String leftResourceUrl, String rightResourceUrl)`
  - `public Void SetSelectionHandlePressedImage(String leftResourceUrl, String rightResourceUrl)`
  - `public Void UnsetCursorBlink()`
  - `public Int32 CursorPosition { get; set; }`
  - `public Color InputColor { get; set; }`
  - `public String InputFontFamily { get; set; }`
  - `public Single InputFontSize { get; set; }`
  - `public FontSlant InputFontSlant { get; set; }`
  - `private FontStyle InputFontStyle { get; set; }`
  - `public FontWeight InputFontWeight { get; set; }`
  - `public FontWidth InputFontWidth { get; set; }`
  - `public InputMethodContext InputMethodContext { get; }`
  - `private Single InputPointSize { get; set; }`
  - `public Boolean IsEditable { get; set; }`
  - `public Int32 MaximumLength { get; set; }`
  - `public String Placeholder { get; set; }`
  - `public Color PlaceholderColor { get; set; }`
  - `public String SelectedText { get; }`
  - `public ClosedRange<Int32> SelectedTextIndex { get; }`
  - `public Boolean SelectionEnabled { get; set; }`

### enum `InteractionType`

- Base: `System.Enum`
- Members:

### class `InteractiveProgress`

- Base: `Tizen.UI.Components.Pressable`
- Members:
  - `public .ctor(Single minimumValue, Single maximumValue)`
  - `public event EventHandler<InputEventArgs> ValueChanged`
  - `public event EventHandler<InputEventArgs> ValueChangeFinished`
  - `public event EventHandler<InputEventArgs> ValueChangeStarted`
  - `public Void add_ValueChanged(EventHandler<InputEventArgs> value)`
  - `public Void add_ValueChangeFinished(EventHandler<InputEventArgs> value)`
  - `public Void add_ValueChangeStarted(EventHandler<InputEventArgs> value)`
  - `private Single CalculatePointRatio(Point lastPoint, Point currentPoint)`
  - `protected Boolean DecreaseValue(Single step, KeyDeviceClass device)`
  - `public Boolean get_IgnoreTap()`
  - `public Boolean get_IgnoreTouchDown()`
  - `protected Boolean get_IsDiscrete()`
  - `protected Single get_PointRatio()`
  - `public ClosedRange<Single> get_Range()`
  - `public Boolean get_UseRelativeTouchPoint()`
  - `public Single get_Value()`
  - `public Boolean get_ValueChanging()`
  - `protected Single get_ValueRatio()`
  - `public Int32 get_ValueStepCount()`
  - `protected String GetAccessibilityValueText()`
  - `protected Single GetKeyStep()`
  - `public Single GetValueInStep(Int32 stepIndex)`
  - `protected Rect GetVisualRangeBounds()`
  - `protected Boolean IncreaseValue(Single step, KeyDeviceClass device)`
  - `protected Boolean IsDecreaseKey(String keyname)`
  - `protected Boolean IsIncreaseKey(String keyname)`
  - `protected Single LengthToRatio(Single deltaX, Single deltaY)`
  - `protected Boolean OnAccessibilityActionReceived(AccessibilityAction actionType)`
  - `private Void OnAccessibilityActionReceivedInternal(Object sender, AccessibilityActionReceivedEventArgs e)`
  - `protected Boolean OnAccessibilityDecrementAction()`
  - `protected Boolean OnAccessibilityIncrementAction()`
  - `protected Boolean OnKeyPressed(KeyEventArgs e)`
  - `protected Boolean OnKeyReleased(KeyEventArgs e)`
  - `protected Void OnPointRatioChanged(Boolean valueUpdated, KeyDeviceClass device)`
  - `protected Void OnPointRatioChangeFailed(Boolean valueChanged, KeyDeviceClass device)`
  - `protected Void OnTouch(Object sender, TouchEventArgs e)`
  - `protected Void OnValueChanged(KeyDeviceClass device)`
  - `protected Void OnValueChangeFinished(KeyDeviceClass device)`
  - `protected Void OnValueChangeStarted(KeyDeviceClass device)`
  - `protected Void OnValueRatioChanged(KeyDeviceClass device)`
  - `protected Void OnValueStepChanged(Single valueStep)`
  - `protected Single PointToRatio(Single x, Single y)`
  - `public Void remove_ValueChanged(EventHandler<InputEventArgs> value)`
  - `public Void remove_ValueChangeFinished(EventHandler<InputEventArgs> value)`
  - `public Void remove_ValueChangeStarted(EventHandler<InputEventArgs> value)`
  - `public Void set_IgnoreTap(Boolean value)`
  - `public Void set_IgnoreTouchDown(Boolean value)`
  - `public Void set_Range(ClosedRange<Single> value)`
  - `public Void set_UseRelativeTouchPoint(Boolean value)`
  - `public Void set_Value(Single value)`
  - `public Void set_ValueStepCount(Int32 value)`
  - `private Boolean SetPointRatio(Single newPointRatio, Boolean valueChanged, KeyDeviceClass device)`
  - `private Boolean SetValue(Single newValue, KeyDeviceClass device)`
  - `private Boolean SetValueAndPoint(Single newValue, KeyDeviceClass device)`
  - `private Boolean SetValueRatio(Single newValueRatio)`
  - `private Void UpdateValueStep()`
  - `public Boolean IgnoreTap { get; set; }`
  - `public Boolean IgnoreTouchDown { get; set; }`
  - `protected Boolean IsDiscrete { get; }`
  - `protected Single PointRatio { get; }`
  - `public ClosedRange<Single> Range { get; set; }`
  - `public Boolean UseRelativeTouchPoint { get; set; }`
  - `public Single Value { get; set; }`
  - `public Boolean ValueChanging { get; }`
  - `protected Single ValueRatio { get; }`
  - `public Int32 ValueStepCount { get; set; }`

### class `InteractiveProgressHelper`

- Base: `System.Object`
- Members:
  - `public static Single HorizontalLengthToRatio(Single currentRatio, Single deltaX, Single width, Rect targetBounds)`
  - `public static Single HorizontalPointToRatio(Single x, Single width, Rect targetBounds)`
  - `public static Single VerticalLengthToRatio(Single currentRatio, Single deltaY, Single height, Rect targetBounds)`
  - `public static Single VerticalPointToRatio(Single y, Single height, Rect targetBounds)`

### interface `IPressable`

- Members:
  - `public event EventHandler<InputEventArgs> PressedChanged`
  - `public Void add_PressedChanged(EventHandler<InputEventArgs> value)`
  - `public Boolean get_IsPressed()`
  - `public Void remove_PressedChanged(EventHandler<InputEventArgs> value)`
  - `public Boolean IsPressed { get; }`

### interface `IPropertySetter`1`

- Members:
  - `public String get_Name()`
  - `public Void Invoke(Object target, TValue value)`
  - `public String Name { get; }`

### interface `ISelectable`

- Members:
  - `public event EventHandler<InputEventArgs> SelectedChanged`
  - `public Void add_SelectedChanged(EventHandler<InputEventArgs> value)`
  - `public Boolean get_IsSelectable()`
  - `public Boolean get_IsSelected()`
  - `public Void remove_SelectedChanged(EventHandler<InputEventArgs> value)`
  - `public Void set_IsSelected(Boolean value)`
  - `public Void Toggle(KeyDeviceClass device)`
  - `public Boolean IsSelectable { get; }`
  - `public Boolean IsSelected { get; set; }`

### interface `ISelectionGroup`

- Members:
  - `public event EventHandler<GroupSelectionChangedEventArgs> SelectionChanged`
  - `public Void add_SelectionChanged(EventHandler<GroupSelectionChangedEventArgs> value)`
  - `public IGroupSelectable get_Selected()`
  - `public Void remove_SelectionChanged(EventHandler<GroupSelectionChangedEventArgs> value)`
  - `public IGroupSelectable Selected { get; }`

### interface `IThemeLoader`

- Members:
  - `public event EventHandler ThemeChanged`
  - `public Void add_ThemeChanged(EventHandler value)`
  - `public String get_CurrentResourcePath()`
  - `public String get_CurrentThemeId()`
  - `public IDictionary<String, Color> LoadColorTable()`
  - `public Void remove_ThemeChanged(EventHandler value)`
  - `public String CurrentResourcePath { get; }`
  - `public String CurrentThemeId { get; }`

### interface `ITitle`

- Members:
  - `public String get_Title()`
  - `public Void set_Title(String value)`
  - `public String Title { get; set; }`

### interface `ITouchEffectTarget`

- Members:
  - `public View GetTouchEffectSecondaryTarget()`
  - `public View GetTouchEffectTarget()`

### interface `IWhenHandler`

- Members:
  - `public Void Execute(Object target, UIStateChangedEventArgs e)`
  - `public Void ExecuteForce(Object target, UIStateChangedEventArgs e)`
  - `public Boolean MatchAction(Delegate action)`
  - `public Void Register(View view)`
  - `public Void Unregister(View view)`

### class `JsonHelper`

- Base: `System.Object`
- Members:
  - `private static .cctor()`

### class `JsonParser`

- Base: `System.Object`
- Members:
  - `public static IDictionary<String, Color> ParseUIColor(String jsonFilePath)`

### class `LabelImpl`

- Base: `Tizen.UI.Components.TextBaseImpl`
- Interfaces: `Tizen.UI.ITextFormatting`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `private event EventHandler _fontSizeAdjusted`
  - `public event EventHandler FontSizeAdjusted`
  - `private Void add__fontSizeAdjusted(EventHandler value)`
  - `public Void add_FontSizeAdjusted(EventHandler value)`
  - `protected Void ConnectAnchorClicked(IntPtr handler)`
  - `private Void ConnectFontSizeAdjusted(IntPtr handler)`
  - `protected IntPtr CreateNativeHandle()`
  - `protected Void DisconnectAnchorClicked(IntPtr handler)`
  - `private Void DisconnectFontSizeAdjusted(IntPtr handler)`
  - `public Single get_AdjustedFontSize()`
  - `public Color get_AnchorClickedColor()`
  - `public Color get_AnchorColor()`
  - `public AutoFontSize get_AutoFontSize()`
  - `public Boolean get_IsAbsoluteLineHeight()`
  - `public Boolean get_IsMultiline()`
  - `public Boolean get_IsTextCutout()`
  - `public LineBreakMode get_LineBreakMode()`
  - `public Single get_LineHeight()`
  - `protected Int32 GetEllipsisPositionPropertyIndex()`
  - `protected Int32 GetEllipsisPropertyIndex()`
  - `protected Int32 GetFontFamilyPropertyIndex()`
  - `protected Int32 GetFontSizeScalePropertyIndex()`
  - `protected Int32 GetFontStylePropertyIndex()`
  - `protected Int32 GetHorizontalAlignmentPropertyIndex()`
  - `public Int32 GetLineCount(Single width)`
  - `protected Int32 GetMarkupEnabledPropertyIndex()`
  - `protected Int32 GetOutlinePropertyIndex()`
  - `protected Int32 GetPixelSizePropertyIndex()`
  - `protected Int32 GetStrikethroughPropertyIndex()`
  - `protected Int32 GetTextColorPropertyIndex()`
  - `protected Int32 GetTextDropShadowPropertyIndex()`
  - `protected Int32 GetTextPropertyIndex()`
  - `protected Int32 GetUnderlinePropertyIndex()`
  - `protected Int32 GetVerticalAlignmentPropertyIndex()`
  - `private Void OnFontSizeAdjusted(IntPtr handle)`
  - `protected Void OnInitialized()`
  - `protected Int32 RegisterFontVariationProperty(String tag)`
  - `private Void remove__fontSizeAdjusted(EventHandler value)`
  - `public Void remove_FontSizeAdjusted(EventHandler value)`
  - `public Void set_AnchorClickedColor(Color value)`
  - `public Void set_AnchorColor(Color value)`
  - `public Void set_AutoFontSize(AutoFontSize value)`
  - `public Void set_IsAbsoluteLineHeight(Boolean value)`
  - `public Void set_IsMultiline(Boolean value)`
  - `public Void set_IsTextCutout(Boolean value)`
  - `public Void set_LineBreakMode(LineBreakMode value)`
  - `public Void set_LineHeight(Single value)`
  - `private Void SetAutoScrollEnabledProperty(Boolean autoScrollEnabled)`
  - `public Void SetMarqueeGap(Single value)`
  - `public Void SetMarqueeLoopCount(Int32 value)`
  - `public Void SetMarqueeLoopDelay(Single value)`
  - `public Void SetMarqueeSpeed(Int32 value)`
  - `private Void SetMinLineSize(Single value)`
  - `private Void SetRelativeLineHeight(Single value)`
  - `protected Void SetTextOverflow(TextOverflow textOverflow)`
  - `private Void UpdateLineHeight()`
  - `public Single AdjustedFontSize { get; }`
  - `public Color AnchorClickedColor { get; set; }`
  - `public Color AnchorColor { get; set; }`
  - `public AutoFontSize AutoFontSize { get; set; }`
  - `public Boolean IsAbsoluteLineHeight { get; set; }`
  - `public Boolean IsMultiline { get; set; }`
  - `public Boolean IsTextCutout { get; set; }`
  - `public LineBreakMode LineBreakMode { get; set; }`
  - `public Single LineHeight { get; set; }`

### class `LocalizationExtensions`

- Base: `System.Object`
- Members:
  - `public static TView BindLocalizedAccessibilityDescription(TView view, String key, ResourceManager resourceManager)`
  - `public static TView BindLocalizedAccessibilityName(TView view, String key, ResourceManager resourceManager)`
  - `public static TText BindLocalizedPlaceholderText(TText view, String key, ResourceManager resourceManager)`
  - `public static TText BindLocalizedSubtitle(TText view, String key, ResourceManager resourceManager)`
  - `public static TText BindLocalizedTitle(TText view, String key, ResourceManager resourceManager)`

### class `LottieImageVisualMap`

- Base: `Tizen.UI.Internal.ImageVisualMap`
- Members:
  - `public .ctor()`
  - `protected Int32 GetImageTypeIndex()`

### enum `ModalPivot`

- Base: `System.Enum`
- Members:

### class `Navigator`

- Base: `Tizen.UI.ContentView`
- Interfaces: `Tizen.UI.Components.INavigateBackHandler`, `Tizen.UI.Components.INavigation`, `Tizen.UI.Components.INavigationAnimation`, `Tizen.UI.Components.INavigationTransition`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `private Void AddModalView(View view)`
  - `private Void AddToBody(View child)`
  - `private Void AddView(View view)`
  - `private static INavigationAnimationController DefaultPopAnimation(View poppedView, View newTop)`
  - `private static INavigationAnimationController DefaultPopModalAnimation(View poppedView, View newTop)`
  - `private static INavigationAnimationController DefaultPushAnimation(View pushedView, View curTop)`
  - `private static INavigationAnimationController DefaultPushModalAnimation(View pushedView, View curTop)`
  - `private Void DetachedFromWindowHandler(Object sender, EventArgs args)`
  - `public Void DidAppear(Boolean byPopNavigation)`
  - `public Void DidDisappear(Boolean byPopNavigation)`
  - `protected Void Dispose(Boolean disposing)`
  - `private Void DisposeChildren(List<View> stack)`
  - `private Void DummyTouchEventHandler(Object sender, TouchEventArgs args)`
  - `private Void EmitAccessibilityStateOnModal(View modalContainer, Boolean value)`
  - `private Layout get_Body()`
  - `public View get_CurrentView()`
  - `public IReadOnlyList<View> get_ModalStack()`
  - `public IReadOnlyList<View> get_NavigationStack()`
  - `public Func<View, View, INavigationAnimationController> get_PopAnimation()`
  - `public Func<View, View, INavigationAnimationController> get_PopModalAnimation()`
  - `public Func<View, View, INavigationAnimationController> get_PushAnimation()`
  - `public Func<View, View, INavigationAnimationController> get_PushModalAnimation()`
  - `private View GetTopView(List<View> stack)`
  - `public Void InsertBefore(View view, View before)`
  - `private Boolean IsTopView(List<View> stack, View view)`
  - `private static Boolean IsViewModalRole(View view)`
  - `private Void LoadNavigationProperties(View view)`
  - `public Boolean NavigateBack()`
  - `public Task<View> PopAsync()`
  - `public Task<View> PopAsync(Boolean animated)`
  - `public Task<View> PopModalAsync()`
  - `public Task<View> PopModalAsync(Boolean animated)`
  - `public Task PushAsync(View view)`
  - `public Task PushAsync(View view, Boolean animated)`
  - `public Task PushModalAsync(View view)`
  - `public Task PushModalAsync(View view, Boolean animated)`
  - `public Void Remove(View view)`
  - `private Void RemoveInternal(View view)`
  - `private Void SaveNavigationProperties(View view)`
  - `private Void ThrowIfContains(View view)`
  - `private Void ThrowIfNotContains(List<View> stack, View view)`
  - `private Void ThrowIfNotContains(View view)`
  - `private Boolean Tizen.UI.Components.INavigateBackHandler.HandleNavigateBack()`
  - `public Void WillAppear(Boolean byPopNavigation)`
  - `public Void WillDisappear(Boolean byPopNavigation)`
  - `private Layout Body { get; }`
  - `public View CurrentView { get; }`
  - `public IReadOnlyList<View> ModalStack { get; }`
  - `public IReadOnlyList<View> NavigationStack { get; }`
  - `public Func<View, View, INavigationAnimationController> PopAnimation { get; }`
  - `public Func<View, View, INavigationAnimationController> PopModalAnimation { get; }`
  - `public Func<View, View, INavigationAnimationController> PushAnimation { get; }`
  - `public Func<View, View, INavigationAnimationController> PushModalAnimation { get; }`

### class `NoneAttachable`

- Base: `Tizen.UI.Components.UIAttachable`
- Members:
  - `public .ctor()`
  - `public Void OnAttached(View view)`
  - `public Void OnDetached(View view)`

### enum `OffScreenRenderingMode`

- Base: `System.Enum`
- Members:

### enum `OverScrollMode`

- Base: `System.Enum`
- Members:

### class `Palette`

- Base: `System.Object`
- Members:
  - `private .ctor(IEnumerable<Swatch> swatches)`
  - `private static Single CreateComparisonValue(Single saturation, Single targetSaturation, Single luma, Single targetLuma, Int32 population, Int32 highestPopulation)`
  - `private Swatch FindColor(Single targetLuma, Single minLuma, Single maxLuma, Single targetSaturation, Single minSaturation, Single maxSaturation)`
  - `private Swatch FindDominantSwatch()`
  - `public static Palette Generate(PixelBuffer buffer)`
  - `public static Palette Generate(PixelBuffer buffer, Single regionX, Single regionY, Single regionWidth, Single regionHeight)`
  - `public static Task<Palette> GenerateAsync(PixelBuffer buffer)`
  - `public static Task<Palette> GenerateAsync(PixelBuffer buffer, Single regionX, Single regionY, Single regionWidth, Single regionHeight)`
  - `private Void GenerateEmptySwatches()`
  - `public Swatch get_DarkMuted()`
  - `public Swatch get_DarkVibrant()`
  - `public Swatch get_Dominant()`
  - `public Swatch get_LightMuted()`
  - `public Swatch get_LightVibrant()`
  - `public Swatch get_Muted()`
  - `public IEnumerable<Swatch> get_Swatches()`
  - `public Swatch get_Vibrant()`
  - `private static Single InvertDiff(Single value, Single targetValue)`
  - `private Boolean IsAlreadySelected(Swatch swatch)`
  - `private static Single ResizeBitmap(PixelBuffer buffer, Int32 size)`
  - `private static Single WeightedMean(Single[] values)`
  - `public Swatch DarkMuted { get; }`
  - `public Swatch DarkVibrant { get; }`
  - `public Swatch Dominant { get; }`
  - `public Swatch LightMuted { get; }`
  - `public Swatch LightVibrant { get; }`
  - `public Swatch Muted { get; }`
  - `public IEnumerable<Swatch> Swatches { get; }`
  - `public Swatch Vibrant { get; }`

### class `Pressable`

- Base: `Tizen.UI.ContentView`
- Interfaces: `Tizen.UI.Components.IPressable`, `Tizen.UI.Components.ITouchEffectTarget`
- Members:
  - `public .ctor()`
  - `public event EventHandler<InputEventArgs> PressedChanged`
  - `private Void <.ctor>b__2_0(Object _, UIStateChangedEventArgs e)`
  - `public Void add_PressedChanged(EventHandler<InputEventArgs> value)`
  - `private Void ClearKeyPressedHistory()`
  - `public Boolean get_IsPressed()`
  - `public Action<Object, InputEventArgs> get_PressedChangedCommand()`
  - `public View GetTouchEffectSecondaryTarget()`
  - `public View GetTouchEffectTarget()`
  - `protected Boolean HasExecutionKeyLongPressed()`
  - `protected Boolean HasExecutionKeyPressedOnce()`
  - `protected Boolean IsExecutionKey(String keyPressedName)`
  - `private Boolean IsPressedExecutionKey(String keyName)`
  - `protected Void OnFocused(Object sender, EventArgs e)`
  - `protected Void OnHover(Object sender, HoverEventArgs e)`
  - `protected Void OnInitialize()`
  - `protected Void OnKey(Object sender, KeyEventArgs e)`
  - `protected Boolean OnKeyPressed(KeyEventArgs e)`
  - `protected Boolean OnKeyReleased(KeyEventArgs e)`
  - `protected Boolean OnPressed(KeyDeviceClass device)`
  - `protected Boolean OnReleased(KeyDeviceClass device)`
  - `protected Void OnStateChanged(UIStateChangedEventArgs eventArgs)`
  - `protected Void OnTouch(Object sender, TouchEventArgs e)`
  - `protected Boolean OnTouchInterrupted(TouchEventArgs e)`
  - `protected Boolean OnTouchMoved(TouchEventArgs e)`
  - `protected Boolean OnTouchPressed(TouchEventArgs e)`
  - `protected Boolean OnTouchReleased(TouchEventArgs e)`
  - `protected Void OnUnfocused(Object sender, EventArgs e)`
  - `private Void RecordPressedExecutionKey(String keyName)`
  - `public Void remove_PressedChanged(EventHandler<InputEventArgs> value)`
  - `protected Void set_IsPressed(Boolean value)`
  - `public Void set_PressedChangedCommand(Action<Object, InputEventArgs> value)`
  - `private Boolean SetPressed(KeyDeviceClass device)`
  - `private Boolean SetReleased(KeyDeviceClass device)`
  - `public Boolean IsPressed { get; set; }`
  - `public Action<Object, InputEventArgs> PressedChangedCommand { get; set; }`

### class `PressableBox`

- Base: `Tizen.UI.Components.Pressable`
- Interfaces: `System.Collections.Generic.ICollection`1<Tizen.UI.View>`, `System.Collections.Generic.IEnumerable`1<Tizen.UI.View>`, `System.Collections.Generic.IList`1<Tizen.UI.View>`, `System.Collections.IEnumerable`, `Tizen.UI.Components.ILayoutBox`
- Members:
  - `protected .ctor()`
  - `public Void Clear()`
  - `public Boolean Contains(View item)`
  - `public Void CopyTo(View[] array, Int32 arrayIndex)`
  - `public IList<View> get_Children()`
  - `public Int32 get_Count()`
  - `protected IList<View> get_InnerList()`
  - `public Boolean get_IsReadOnly()`
  - `public View get_Item(Int32 index)`
  - `public Thickness get_Padding()`
  - `public IEnumerator<View> GetEnumerator()`
  - `public Int32 IndexOf(View item)`
  - `public Void Insert(Int32 index, View item)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_Item(Int32 index, View value)`
  - `public Void set_Padding(Thickness value)`
  - `private Void System.Collections.Generic.ICollection<Tizen.UI.View>.Add(View item)`
  - `private Boolean System.Collections.Generic.ICollection<Tizen.UI.View>.Remove(View item)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `public IList<View> Children { get; }`
  - `public Int32 Count { get; }`
  - `protected IList<View> InnerList { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public View Item { get; set; }`
  - `public Thickness Padding { get; set; }`

### class `Progress`

- Base: `Tizen.UI.ContentView`
- Members:
  - `public .ctor(Single minimumValue, Single maximumValue)`
  - `public event EventHandler ValueChanged`
  - `public Void add_ValueChanged(EventHandler value)`
  - `public Boolean get_IsDeterminate()`
  - `protected Boolean get_IsDiscrete()`
  - `public ClosedRange<Single> get_Range()`
  - `public Single get_Value()`
  - `protected Single get_ValueRatio()`
  - `public Int32 get_ValueStepCount()`
  - `protected String GetAccessibilityValueText()`
  - `private Void OnAccessibilityHighlightChanged(Object sender, AccessibilityHighlightChangedEventArgs e)`
  - `protected Void OnDeterminateStateChanged()`
  - `protected Void OnValueChanged(KeyDeviceClass device)`
  - `protected Void OnValueRatioChanged(KeyDeviceClass device)`
  - `public Void remove_ValueChanged(EventHandler value)`
  - `public Void set_IsDeterminate(Boolean value)`
  - `public Void set_Range(ClosedRange<Single> value)`
  - `public Void set_Value(Single value)`
  - `public Void set_ValueStepCount(Int32 value)`
  - `private Boolean SetValue(Single newValue, KeyDeviceClass device)`
  - `private Boolean SetValueRatio(Single newValueRatio, KeyDeviceClass device)`
  - `private Void UpdateValueStep()`
  - `public Boolean IsDeterminate { get; set; }`
  - `protected Boolean IsDiscrete { get; }`
  - `public ClosedRange<Single> Range { get; set; }`
  - `public Single Value { get; set; }`
  - `protected Single ValueRatio { get; }`
  - `public Int32 ValueStepCount { get; set; }`

### class `PropertySetter`2`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.IPropertySetter`1<TValue>`
- Members:
  - `public .ctor(String name, Action<TView, TValue> setter)`
  - `public String get_Name()`
  - `public Void Invoke(Object target, TValue value)`
  - `public String Name { get; }`

### class `RecoilEffect`

- Base: `Tizen.UI.Components.UIAttachable`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public CornerRadius get_CornerRadius()`
  - `public Boolean get_InheritCornerRadius()`
  - `public Single get_OffsetFactorX()`
  - `public Color get_OverlayColor()`
  - `public Thickness get_OverlayPadding()`
  - `public Single get_OverlayScaleFactorX()`
  - `public Single get_OverlayScaleFactorY()`
  - `public Single get_ScaleFactorX()`
  - `private static View GetOverlayTarget(View view)`
  - `private static View GetScaleTarget(View view, View overlayTarget)`
  - `public Void OnAttached(View view)`
  - `private static Boolean OnCancel(View view)`
  - `public Void OnDetached(View view)`
  - `private Boolean OnPress(View view)`
  - `private Void OnPressedChanged(Object sender, InputEventArgs e)`
  - `private static Boolean OnRelease(View view)`
  - `public Void OnTouched(Object sender, TouchEventArgs args)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_CornerRadius(CornerRadius value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_InheritCornerRadius(Boolean value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_OffsetFactorX(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_OverlayColor(Color value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_OverlayPadding(Thickness value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_OverlayScaleFactorX(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_OverlayScaleFactorY(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_ScaleFactorX(Single value)`
  - `public CornerRadius CornerRadius { get; set; }`
  - `public Boolean InheritCornerRadius { get; set; }`
  - `public Single OffsetFactorX { get; set; }`
  - `public Color OverlayColor { get; set; }`
  - `public Thickness OverlayPadding { get; set; }`
  - `public Single OverlayScaleFactorX { get; set; }`
  - `public Single OverlayScaleFactorY { get; set; }`
  - `public Single ScaleFactorX { get; set; }`

### class `ScaffoldBase`

- Base: `Tizen.UI.ContentView`
- Members:
  - `public .ctor()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected View get_Body()`
  - `public View get_FloatingActionButton()`
  - `public Point get_FloatingOffset()`
  - `public FloatingOrigin get_FloatingOrigin()`
  - `public Boolean get_HasIndicatorArea()`
  - `public Boolean get_HasNavigationBarArea()`
  - `public Color get_IndicatorAreaColor()`
  - `public IColorProvider get_IndicatorAreaColorProvider()`
  - `protected Single GetFloatingOffsetY()`
  - `private Void OnDominantColorChanged(Object sender, EventArgs e)`
  - `protected Void OnIndicatorAreaChanged()`
  - `protected Void OnNavigationBarAreaChanged()`
  - `protected Void set_Body(View value)`
  - `public Void set_FloatingActionButton(View value)`
  - `public Void set_FloatingOffset(Point value)`
  - `public Void set_FloatingOrigin(FloatingOrigin value)`
  - `public Void set_HasIndicatorArea(Boolean value)`
  - `public Void set_HasNavigationBarArea(Boolean value)`
  - `public Void set_IndicatorAreaColor(Color value)`
  - `public Void set_IndicatorAreaColorProvider(IColorProvider value)`
  - `public Void SetExtraFloatingOffsetY(Single extra)`
  - `private Void SetFloatingActionButtonOrigin()`
  - `protected Void SetFloatingOffset()`
  - `private Void UpdateIndicatorAreaColor()`
  - `protected View Body { get; set; }`
  - `public View FloatingActionButton { get; set; }`
  - `public Point FloatingOffset { get; set; }`
  - `public FloatingOrigin FloatingOrigin { get; set; }`
  - `public Boolean HasIndicatorArea { get; set; }`
  - `public Boolean HasNavigationBarArea { get; set; }`
  - `public Color IndicatorAreaColor { get; set; }`
  - `public IColorProvider IndicatorAreaColorProvider { get; set; }`

### class `Scrollable`

- Base: `Tizen.UI.Layouts.Layout`
- Interfaces: `Tizen.UI.IDescendantFocusObserver`, `Tizen.UI.IScrollable`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
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
  - `private Point AdjustDelta(Point movement, Nullable<Point> currentPosition)`
  - `private Point AdjustScrollPosition(Point position)`
  - `private Void CancelScrollAnimation()`
  - `private Point ContentPositionToScrollPosition(Point content)`
  - `protected IScrollBar CreateScrollBar()`
  - `private Point DeltaFromScrollPosition(Point scrollPosition)`
  - `protected Void Dispose(Boolean disposing)`
  - `private Void EnsureScrollBar()`
  - `public Boolean get_CanScrollHorizontally()`
  - `public Boolean get_CanScrollVertically()`
  - `public View get_Content()`
  - `public IEdgeEffect get_HorizontalEdgeEffect()`
  - `public ScrollBarVisibility get_HorizontalScrollBarVisibility()`
  - `public Boolean get_IsScrolledToEnd()`
  - `public Boolean get_IsScrolledToStart()`
  - `public Boolean get_IsScrolling()`
  - `protected ILayoutManager get_LayoutManager()`
  - `public OverScrollMode get_OverScrollMode()`
  - `public Single get_ScrollableHeight()`
  - `public Single get_ScrollableWidth()`
  - `public IScrollBar get_ScrollBar()`
  - `public ScrollDirection get_ScrollDirection()`
  - `public Func<Point, Point> get_ScrollingDestinationHandler()`
  - `public Point get_ScrollPosition()`
  - `public Size get_ScrollSize()`
  - `public IEdgeEffect get_VerticalEdgeEffect()`
  - `public ScrollBarVisibility get_VerticalScrollBarVisibility()`
  - `public Rect get_ViewPort()`
  - `protected ValueTuple<Int32, AlphaFunction> GetDurationAndAlpha(Point movement)`
  - `private Point GetScrollPosition(View view, Point current)`
  - `public Boolean IsScrollableOnDirection(ScrollDirection direction)`
  - `protected Void OnChildAdded(View view)`
  - `protected Void OnChildMeasureInvalidatedOverride(View child)`
  - `protected Void OnChildRemoved(View view)`
  - `private Void OnInterruptTouchingChildTouched(Object source, TouchEventArgs e)`
  - `private Void OnKeyEvent(Object sender, KeyEventArgs e)`
  - `private Void OnPan(Object sender, PanGestureDetectedEventArgs e)`
  - `private Void OnScrollAnimationFinished(Object sender, EventArgs e)`
  - `private Void OnScrolling()`
  - `private Void OnTouchEvent(Object sender, TouchEventArgs e)`
  - `protected Boolean OnWheel(WheelEvent wheel)`
  - `private Void OnWheelEvent(Object sender, WheelEventArgs e)`
  - `private Boolean ReleaseEdgeEffect()`
  - `public Void remove_DragFinished(EventHandler<DragEventArgs> value)`
  - `public Void remove_Dragging(EventHandler<DragEventArgs> value)`
  - `public Void remove_DragStarted(EventHandler<DragEventArgs> value)`
  - `public Void remove_ScrollFinished(EventHandler<ScrollEventArgs> value)`
  - `public Void remove_Scrolling(EventHandler<ScrollEventArgs> value)`
  - `public Void remove_ScrollStarted(EventHandler<ScrollEventArgs> value)`
  - `protected Task ScrollContent(Point delta)`
  - `protected Point ScrollingDestinationOverride(Point dest)`
  - `private Point ScrollPositionFromDelta(Point delta)`
  - `public Task ScrollTo(View child, Boolean animation, ScrollToPosition scrollToPosition)`
  - `public Task ScrollTo(Single position, Boolean animation)`
  - `public Task ScrollTo(Point position, Boolean animation)`
  - `private Void SendScrollFinished()`
  - `private Void SendScrolling()`
  - `private Void SendScrollStarted()`
  - `public Void set_Content(View value)`
  - `public Void set_HorizontalEdgeEffect(IEdgeEffect value)`
  - `public Void set_HorizontalScrollBarVisibility(ScrollBarVisibility value)`
  - `protected Void set_IsScrolling(Boolean value)`
  - `public Void set_OverScrollMode(OverScrollMode value)`
  - `public Void set_ScrollBar(IScrollBar value)`
  - `public Void set_ScrollDirection(ScrollDirection value)`
  - `public Void set_ScrollingDestinationHandler(Func<Point, Point> value)`
  - `public Void set_ScrollSize(Size value)`
  - `public Void set_VerticalEdgeEffect(IEdgeEffect value)`
  - `public Void set_VerticalScrollBarVisibility(ScrollBarVisibility value)`
  - `private Void SetScroll(Point position)`
  - `private Void SetScrollBar(IScrollBar scrollbar)`
  - `private Void Tizen.UI.IDescendantFocusObserver.OnDescendantFocused(View descendant)`
  - `private Task Tizen.UI.IScrollable.ScrollBy(Single dx, Single dy, Boolean animated)`
  - `private Task Tizen.UI.IScrollable.ScrollTo(Single x, Single y, Boolean animated)`
  - `public Size UpdateLayout(Rect bounds)`
  - `private Void UpdateScrollingProperties()`
  - `private Void UpdateScrollPositionByUIPosition()`
  - `protected Point VelocityToMovement(Point velocity)`
  - `public Boolean CanScrollHorizontally { get; }`
  - `public Boolean CanScrollVertically { get; }`
  - `public View Content { get; set; }`
  - `public IEdgeEffect HorizontalEdgeEffect { get; set; }`
  - `public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }`
  - `public Boolean IsScrolledToEnd { get; }`
  - `public Boolean IsScrolledToStart { get; }`
  - `public Boolean IsScrolling { get; set; }`
  - `protected ILayoutManager LayoutManager { get; }`
  - `public OverScrollMode OverScrollMode { get; set; }`
  - `public Single ScrollableHeight { get; }`
  - `public Single ScrollableWidth { get; }`
  - `public IScrollBar ScrollBar { get; set; }`
  - `public ScrollDirection ScrollDirection { get; set; }`
  - `public Func<Point, Point> ScrollingDestinationHandler { get; set; }`
  - `public Point ScrollPosition { get; }`
  - `public Size ScrollSize { get; set; }`
  - `public IEdgeEffect VerticalEdgeEffect { get; set; }`
  - `public ScrollBarVisibility VerticalScrollBarVisibility { get; set; }`
  - `public Rect ViewPort { get; }`

### class `ScrollableLayoutManager`

- Base: `Tizen.UI.Layouts.LayoutManager`
- Members:
  - `public .ctor(Scrollable view)`
  - `private Size AdjustForFill(Size size, Rect bounds, View view)`
  - `public Size ArrangeChildren(Rect bounds)`
  - `public Scrollable get_Scrollable()`
  - `public Size Measure(Single widthConstraint, Single heightConstraint)`
  - `public Scrollable Scrollable { get; }`

### class `ScrollBarBase`

- Base: `Tizen.UI.ViewGroup`
- Interfaces: `System.IDisposable`, `Tizen.UI.IScrollBar`
- Members:
  - `public .ctor()`
  - `private Boolean <Initialize>b__61_0()`
  - `private Void CreateHBar()`
  - `private Void CreateVBar()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected Void FadeIn()`
  - `protected Void FadeOut()`
  - `public Color get_BarColor()`
  - `public CornerRadius get_BarCornerRadius()`
  - `public Thickness get_BarMargin()`
  - `public Single get_BarMinSize()`
  - `public Shadow get_BarShadow()`
  - `public Single get_BarThickness()`
  - `protected Animation get_FadeInAnimation()`
  - `protected Animation get_FadeOutAnimation()`
  - `public ScrollBarVisibility get_HorizontalScrollBarVisibility()`
  - `private Size get_ScrollArea()`
  - `public Point get_ScrollPosition()`
  - `public View get_Target()`
  - `public ScrollBarVisibility get_VerticalScrollBarVisibility()`
  - `private Size get_ViewPortSize()`
  - `protected Void Initialize()`
  - `protected Void OnAttachedToScrollable(IScrollable scrollable)`
  - `private Void OnLayoutDirectionChanged(Object sender, EventArgs e)`
  - `private Void OnRelayout(Object sender, EventArgs e)`
  - `protected Void OnUpdateScrollPosition(Point position)`
  - `private Void RemoveHBar()`
  - `private Void RemoveVBar()`
  - `public Void set_BarColor(Color value)`
  - `public Void set_BarCornerRadius(CornerRadius value)`
  - `public Void set_BarMargin(Thickness value)`
  - `public Void set_BarMinSize(Single value)`
  - `public Void set_BarShadow(Shadow value)`
  - `public Void set_BarThickness(Single value)`
  - `public Void set_HorizontalScrollBarVisibility(ScrollBarVisibility value)`
  - `private Void set_ScrollArea(Size value)`
  - `public Void set_ScrollPosition(Point value)`
  - `public Void set_VerticalScrollBarVisibility(ScrollBarVisibility value)`
  - `private Void set_ViewPortSize(Size value)`
  - `protected Void ShowBar()`
  - `private Void Tizen.UI.IScrollBar.OnAttachedToScrollable(IScrollable scrollable)`
  - `public Void UpdateBarSize(Size scrollArea, Size viewportSize)`
  - `private Void UpdateBarSize()`
  - `private Void UpdateHBarSize(Single ratio)`
  - `private Void UpdatePosition(Point position)`
  - `public Void UpdateScrollPosition(Point position)`
  - `private Void UpdateVBarSize(Single ratio)`
  - `public Color BarColor { get; set; }`
  - `public CornerRadius BarCornerRadius { get; set; }`
  - `public Thickness BarMargin { get; set; }`
  - `public Single BarMinSize { get; set; }`
  - `public Shadow BarShadow { get; set; }`
  - `public Single BarThickness { get; set; }`
  - `protected Animation FadeInAnimation { get; }`
  - `protected Animation FadeOutAnimation { get; }`
  - `public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }`
  - `private Size ScrollArea { get; set; }`
  - `public Point ScrollPosition { get; set; }`
  - `public View Target { get; }`
  - `public ScrollBarVisibility VerticalScrollBarVisibility { get; set; }`
  - `private Size ViewPortSize { get; set; }`

### class `ScrollManager`

- Base: `System.Object`
- Members:
  - `public static Boolean get_IsAboutToScroll()`
  - `public static Void set_IsAboutToScroll(Boolean value)`
  - `public Boolean IsAboutToScroll { get; set; }`

### class `Selectable`

- Base: `Tizen.UI.Components.Clickable`
- Interfaces: `Tizen.UI.Components.ISelectable`
- Members:
  - `public .ctor()`
  - `protected .ctor(IClickableVariables variables)`
  - `public event EventHandler<InputEventArgs> SelectedChanged`
  - `public Void add_SelectedChanged(EventHandler<InputEventArgs> value)`
  - `protected Boolean get_IsDeselectable()`
  - `public Boolean get_IsSelectable()`
  - `public Boolean get_IsSelected()`
  - `public Action<Object, InputEventArgs> get_SelectedChangedCommand()`
  - `private Boolean IsCheckableRole()`
  - `protected Boolean OnClicked(KeyDeviceClass device)`
  - `protected Void OnSelectedChanged(KeyDeviceClass device)`
  - `public Void remove_SelectedChanged(EventHandler<InputEventArgs> value)`
  - `public Void set_IsSelectable(Boolean value)`
  - `public Void set_IsSelected(Boolean value)`
  - `public Void set_SelectedChangedCommand(Action<Object, InputEventArgs> value)`
  - `private Void SetSelection(Boolean selection, KeyDeviceClass keyDeviceClass)`
  - `public Void Toggle(KeyDeviceClass device)`
  - `protected Boolean IsDeselectable { get; }`
  - `public Boolean IsSelectable { get; set; }`
  - `public Boolean IsSelected { get; set; }`
  - `public Action<Object, InputEventArgs> SelectedChangedCommand { get; set; }`

### class `SelectableBindings`1`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static TwoWayBindingProperty<TView, Boolean> get_IsSelectedProperty()`
  - `public TwoWayBindingProperty<TView, Boolean> IsSelectedProperty { get; }`

### class `SelectableBox`

- Base: `Tizen.UI.Components.Selectable`
- Interfaces: `System.Collections.Generic.ICollection`1<Tizen.UI.View>`, `System.Collections.Generic.IEnumerable`1<Tizen.UI.View>`, `System.Collections.Generic.IList`1<Tizen.UI.View>`, `System.Collections.IEnumerable`, `Tizen.UI.Components.ILayoutBox`
- Members:
  - `protected .ctor()`
  - `public Void Clear()`
  - `public Boolean Contains(View item)`
  - `public Void CopyTo(View[] array, Int32 arrayIndex)`
  - `public IList<View> get_Children()`
  - `public Int32 get_Count()`
  - `protected IList<View> get_InnerList()`
  - `public Boolean get_IsReadOnly()`
  - `public View get_Item(Int32 index)`
  - `public Thickness get_Padding()`
  - `public IEnumerator<View> GetEnumerator()`
  - `public Int32 IndexOf(View item)`
  - `public Void Insert(Int32 index, View item)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_Item(Int32 index, View value)`
  - `public Void set_Padding(Thickness value)`
  - `private Void System.Collections.Generic.ICollection<Tizen.UI.View>.Add(View item)`
  - `private Boolean System.Collections.Generic.ICollection<Tizen.UI.View>.Remove(View item)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `public IList<View> Children { get; }`
  - `public Int32 Count { get; }`
  - `protected IList<View> InnerList { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public View Item { get; set; }`
  - `public Thickness Padding { get; set; }`

### class `SelectableHStack`

- Base: `Tizen.UI.Components.SelectableBox`
- Members:
  - `public .ctor()`
  - `protected IList<View> get_InnerList()`
  - `public LayoutAlignment get_ItemAlignment()`
  - `public Thickness get_Padding()`
  - `public Single get_Spacing()`
  - `public Void set_ItemAlignment(LayoutAlignment value)`
  - `public Void set_Padding(Thickness value)`
  - `public Void set_Spacing(Single value)`
  - `protected IList<View> InnerList { get; }`
  - `public LayoutAlignment ItemAlignment { get; set; }`
  - `public Thickness Padding { get; set; }`
  - `public Single Spacing { get; set; }`

### class `SelectableVStack`

- Base: `Tizen.UI.Components.SelectableBox`
- Members:
  - `public .ctor()`
  - `protected IList<View> get_InnerList()`
  - `public LayoutAlignment get_ItemAlignment()`
  - `public Thickness get_Padding()`
  - `public Single get_Spacing()`
  - `public Void set_ItemAlignment(LayoutAlignment value)`
  - `public Void set_Padding(Thickness value)`
  - `public Void set_Spacing(Single value)`
  - `protected IList<View> InnerList { get; }`
  - `public LayoutAlignment ItemAlignment { get; set; }`
  - `public Thickness Padding { get; set; }`
  - `public Single Spacing { get; set; }`

### class `SelectionGroup`

- Base: `System.Object`
- Interfaces: `System.Collections.Generic.ICollection`1<Tizen.UI.Components.IGroupSelectable>`, `System.Collections.Generic.IEnumerable`1<Tizen.UI.Components.IGroupSelectable>`, `System.Collections.Generic.IList`1<Tizen.UI.Components.IGroupSelectable>`, `System.Collections.IEnumerable`, `System.IDisposable`, `Tizen.UI.Components.ISelectionGroup`
- Members:
  - `private static .cctor()`
  - `private .ctor(String name)`
  - `public event EventHandler<GroupSelectionChangedEventArgs> SelectionChanged`
  - `public Void Add(IGroupSelectable selectable)`
  - `public Void add_SelectionChanged(EventHandler<GroupSelectionChangedEventArgs> value)`
  - `public Void Clear()`
  - `public Boolean Contains(IGroupSelectable selectable)`
  - `public Void CopyTo(IGroupSelectable[] array, Int32 arrayIndex)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected Void Finalize()`
  - `public static SelectionGroup Find(String groupName)`
  - `public static SelectionGroup Find(View parent)`
  - `public IList<IGroupSelectable> get_Children()`
  - `public Int32 get_Count()`
  - `public Boolean get_IsReadOnly()`
  - `public IGroupSelectable get_Item(Int32 index)`
  - `public IGroupSelectable get_Item(String name)`
  - `public String get_Name()`
  - `public IGroupSelectable get_Selected()`
  - `public Int32 get_SelectedIndex()`
  - `public Action<Object, GroupSelectionChangedEventArgs> get_SelectionChangedCommand()`
  - `public IEnumerator<IGroupSelectable> GetEnumerator()`
  - `public Int32 IndexOf(IGroupSelectable selectable)`
  - `public Void Insert(Int32 index, IGroupSelectable selectable)`
  - `private Void JoinGroup(IGroupSelectable newSelection)`
  - `private Void OnItemSelectionChanged(Object sender, InputEventArgs e)`
  - `public Boolean Remove(IGroupSelectable selectable)`
  - `public Void remove_SelectionChanged(EventHandler<GroupSelectionChangedEventArgs> value)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_Selected(IGroupSelectable value)`
  - `public Void set_SelectionChangedCommand(Action<Object, GroupSelectionChangedEventArgs> value)`
  - `private IGroupSelectable System.Collections.Generic.IList<Tizen.UI.Components.IGroupSelectable>.get_Item(Int32 index)`
  - `private Void System.Collections.Generic.IList<Tizen.UI.Components.IGroupSelectable>.set_Item(Int32 index, IGroupSelectable value)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `public IList<IGroupSelectable> Children { get; }`
  - `public Int32 Count { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public IGroupSelectable Item { get; }`
  - `public IGroupSelectable Item { get; }`
  - `public String Name { get; }`
  - `public IGroupSelectable Selected { get; set; }`
  - `public Int32 SelectedIndex { get; }`
  - `public Action<Object, GroupSelectionChangedEventArgs> SelectionChangedCommand { get; set; }`
  - `private IGroupSelectable System.Collections.Generic.IList<Tizen.UI.Components.IGroupSelectable>.Item { get; set; }`

### class `SelectionGroupBox`1`

- Base: `Tizen.UI.ContentView`
- Interfaces: `System.Collections.Generic.ICollection`1<T>`, `System.Collections.Generic.IEnumerable`1<T>`, `System.Collections.Generic.IList`1<T>`, `System.Collections.IEnumerable`, `Tizen.UI.Components.ILayoutBox`, `Tizen.UI.Components.ISelectionGroup`
- Members:
  - `public .ctor()`
  - `public event EventHandler<SelectionGroupItemClickedEventArgs> ItemClicked`
  - `public event EventHandler<GroupSelectionChangedEventArgs> SelectionChanged`
  - `public Void Add(T item)`
  - `public Void add_ItemClicked(EventHandler<SelectionGroupItemClickedEventArgs> value)`
  - `public Void add_SelectionChanged(EventHandler<GroupSelectionChangedEventArgs> value)`
  - `public Void Clear()`
  - `public Boolean Contains(T item)`
  - `public Void CopyTo(T[] array, Int32 arrayIndex)`
  - `protected Layout CreateBody()`
  - `private Layout get_Body()`
  - `public IList<View> get_Children()`
  - `public Int32 get_Count()`
  - `public Boolean get_EmptySelectionAllowed()`
  - `public Boolean get_IsReadOnly()`
  - `public T get_Item(Int32 index)`
  - `public Action<Object, SelectionGroupItemClickedEventArgs> get_ItemClickedCommand()`
  - `public ICollection<T> get_Items()`
  - `public Thickness get_Padding()`
  - `public Int32 get_SelectedIndex()`
  - `public T get_SelectedItem()`
  - `public Action<Object, GroupSelectionChangedEventArgs> get_SelectionChangedCommand()`
  - `private Int32 GetCurrentSelectedIndex()`
  - `public IEnumerator<T> GetEnumerator()`
  - `private T GetFirstSelectableItem()`
  - `public Int32 IndexOf(T item)`
  - `public Void Insert(Int32 index, T item)`
  - `protected Void OnChildAdded(Int32 index, T child)`
  - `protected Void OnChildRemoved(T child)`
  - `private Void OnItemClicked(Object sender, EventArgs e)`
  - `protected Void OnSelectionChanged(Object sender, GroupSelectionChangedEventArgs e)`
  - `public Boolean Remove(T item)`
  - `public Void remove_ItemClicked(EventHandler<SelectionGroupItemClickedEventArgs> value)`
  - `public Void remove_SelectionChanged(EventHandler<GroupSelectionChangedEventArgs> value)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_EmptySelectionAllowed(Boolean value)`
  - `public Void set_Item(Int32 index, T value)`
  - `public Void set_ItemClickedCommand(Action<Object, SelectionGroupItemClickedEventArgs> value)`
  - `public Void set_Padding(Thickness value)`
  - `public Void set_SelectionChangedCommand(Action<Object, GroupSelectionChangedEventArgs> value)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `private IGroupSelectable Tizen.UI.Components.ISelectionGroup.get_Selected()`
  - `private Layout Body { get; }`
  - `public IList<View> Children { get; }`
  - `public Int32 Count { get; }`
  - `public Boolean EmptySelectionAllowed { get; set; }`
  - `public Boolean IsReadOnly { get; }`
  - `public T Item { get; set; }`
  - `public Action<Object, SelectionGroupItemClickedEventArgs> ItemClickedCommand { get; set; }`
  - `public ICollection<T> Items { get; }`
  - `public Thickness Padding { get; set; }`
  - `public Int32 SelectedIndex { get; }`
  - `public T SelectedItem { get; }`
  - `public Action<Object, GroupSelectionChangedEventArgs> SelectionChangedCommand { get; set; }`
  - `private IGroupSelectable Tizen.UI.Components.ISelectionGroup.Selected { get; }`

### class `SelectionGroupItemClickedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public Int32 get_ItemIndex()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_ItemIndex(Int32 value)`
  - `public Int32 ItemIndex { get; set; }`

### class `SnapControlExtensions`

- Base: `System.Object`
- Members:
  - `private static Void ClearSnapData(Scrollable Scrollable)`
  - `internal static SnapControlData GetSnapData(Scrollable Scrollable)`
  - `public static T SetSnap(T Scrollable, SnapPointsType snapType, SnapPointsAlignment align)`

### class `Swatch`

- Base: `System.Object`
- Members:
  - `public .ctor(Color color, Int32 population)`
  - `public .ctor(Int32 colorValue, Int32 population)`
  - `public .ctor(Int32 r, Int32 g, Int32 b, Int32 population)`
  - `private static ValueTuple<Color, Color> GenerateTextColor(Int32 colorValue)`
  - `public Color get_BodyTextColor()`
  - `public Color get_Color()`
  - `private Int32 get_ColorValue()`
  - `public Int32 get_Population()`
  - `public Color get_TitleTextColor()`
  - `public Color BodyTextColor { get; }`
  - `public Color Color { get; }`
  - `private Int32 ColorValue { get; }`
  - `public Int32 Population { get; }`
  - `public Color TitleTextColor { get; }`

### class `SystemFeedback`

- Base: `Tizen.UI.Components.UIAttachable`
- Members:
  - `private static .cctor()`
  - `public .ctor(InteractionType interactionType, String pattern)`
  - `private Boolean <EnsureTimer>b__18_0()`
  - `private Void CancelTimer()`
  - `private static Feedback EnsureFeedback()`
  - `private Timer EnsureTimer()`
  - `private static Void FeedbackLog(String message)`
  - `private Void InvokeSystemFeedback()`
  - `private Void InvokeSystemFeedbackDelayed()`
  - `private Void InvokeSystemFeedbackNow()`
  - `public Void OnAttached(View view)`
  - `private Void OnClicked(Object sender, EventArgs e)`
  - `public Void OnDetached(View view)`
  - `private Void OnFocused(Object sender, EventArgs e)`
  - `private Void OnKeyEvent(Object sender, KeyEventArgs e)`
  - `private Void OnTouchEvent(Object sender, TouchEventArgs e)`
  - `private Void OnUnfocused(Object sender, EventArgs e)`

### class `SystemLocaleLanguageChangedManager`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public event EventHandler<LocaleLanguageChangedEventArgs> Finished`
  - `public static Void Add(EventHandler<LocaleLanguageChangedEventArgs> handler)`
  - `public static Void add_Finished(EventHandler<LocaleLanguageChangedEventArgs> value)`
  - `public static String get_LocaleLanguage()`
  - `public static Void Remove(EventHandler<LocaleLanguageChangedEventArgs> handler)`
  - `public static Void remove_Finished(EventHandler<LocaleLanguageChangedEventArgs> value)`
  - `private static Void SystemLocaleLanguageChanged(Object sender, LocaleLanguageChangedEventArgs args)`
  - `public String LocaleLanguage { get; }`

### class `TextBaseImpl`

- Base: `System.Object`
- Interfaces: `System.IDisposable`, `Tizen.UI.IFontSizeScale`, `Tizen.UI.IText`, `Tizen.UI.ITextAlignment`, `Tizen.UI.ITextPadding`
- Members:
  - `public .ctor()`
  - `private event EventHandler<AnchorClickedEventArgs> _anchorClicked`
  - `public event EventHandler<AnchorClickedEventArgs> AnchorClicked`
  - `private Void add__anchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `public Void add_AnchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `protected Void CheckValidNativeHandle()`
  - `protected Void ConnectAnchorClicked(IntPtr handler)`
  - `protected IntPtr CreateNativeHandle()`
  - `protected Void DisconnectAnchorClicked(IntPtr handler)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `protected Void Finalize()`
  - `public Single get_AdjustedFontSizeScale()`
  - `public String get_FontFamily()`
  - `public Single get_FontSize()`
  - `public Single get_FontSizeScale()`
  - `public FontSlant get_FontSlant()`
  - `private FontStyle get_FontStyle()`
  - `public FontWeight get_FontWeight()`
  - `public FontWidth get_FontWidth()`
  - `public IntPtr get_Handle()`
  - `public TextAlignment get_HorizontalAlignment()`
  - `public Boolean get_IsDisposed()`
  - `public Boolean get_IsMarkupEnabled()`
  - `public Single get_MaximumFontSizeScale()`
  - `public Single get_MinimumFontSizeScale()`
  - `public Nullable<Outline> get_Outline()`
  - `public View get_Owner()`
  - `protected Boolean get_ShouldBlockTextChangedEvent()`
  - `public Nullable<Strikethrough> get_Strikethrough()`
  - `public Boolean get_SystemFontFamilyEnabled()`
  - `public Boolean get_SystemFontSizeScaleEnabled()`
  - `public String get_Text()`
  - `public Color get_TextColor()`
  - `public TextOverflow get_TextOverflow()`
  - `public Nullable<TextShadow> get_TextShadow()`
  - `public Nullable<Underline> get_Underline()`
  - `public TextAlignment get_VerticalAlignment()`
  - `private FontStyle GetDefaultFontStyle()`
  - `protected Int32 GetEllipsisPositionPropertyIndex()`
  - `protected Int32 GetEllipsisPropertyIndex()`
  - `protected Int32 GetFontFamilyPropertyIndex()`
  - `protected Int32 GetFontSizeScalePropertyIndex()`
  - `protected Int32 GetFontStylePropertyIndex()`
  - `public Single GetHeightForWidth(Single width)`
  - `protected Int32 GetHorizontalAlignmentPropertyIndex()`
  - `protected Int32 GetMarkupEnabledPropertyIndex()`
  - `protected Int32 GetOutlinePropertyIndex()`
  - `protected Int32 GetPixelSizePropertyIndex()`
  - `public Single GetSpaceHeight()`
  - `protected Int32 GetStrikethroughPropertyIndex()`
  - `protected Int32 GetTextColorPropertyIndex()`
  - `protected Int32 GetTextDropShadowPropertyIndex()`
  - `protected Int32 GetTextPropertyIndex()`
  - `protected Int32 GetUnderlinePropertyIndex()`
  - `protected Int32 GetVerticalAlignmentPropertyIndex()`
  - `public Void Initialize(View owner, Action resizeHandler)`
  - `private Void OnAnchorClicked(IntPtr handle, IntPtr href, UInt32 hrefLength)`
  - `protected Void OnInitialized()`
  - `protected Void OnSystemFontChanged(Object sender, EventArgs e)`
  - `protected Void OnSystemFontScaleChanged(Object sender, EventArgs e)`
  - `protected Int32 RegisterFontVariationProperty(String tag)`
  - `private Void remove__anchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `public Void remove_AnchorClicked(EventHandler<AnchorClickedEventArgs> value)`
  - `internal Void RequestUpdateManually()`
  - `public Void set_FontFamily(String value)`
  - `public Void set_FontSize(Single value)`
  - `public Void set_FontSizeScale(Single value)`
  - `public Void set_FontSlant(FontSlant value)`
  - `private Void set_FontStyle(FontStyle value)`
  - `public Void set_FontWeight(FontWeight value)`
  - `public Void set_FontWidth(FontWidth value)`
  - `public Void set_HorizontalAlignment(TextAlignment value)`
  - `public Void set_IsMarkupEnabled(Boolean value)`
  - `public Void set_MaximumFontSizeScale(Single value)`
  - `public Void set_MinimumFontSizeScale(Single value)`
  - `public Void set_Outline(Nullable<Outline> value)`
  - `protected Void set_ShouldBlockTextChangedEvent(Boolean value)`
  - `public Void set_Strikethrough(Nullable<Strikethrough> value)`
  - `public Void set_SystemFontFamilyEnabled(Boolean value)`
  - `public Void set_SystemFontSizeScaleEnabled(Boolean value)`
  - `public Void set_Text(String value)`
  - `public Void set_TextColor(Color value)`
  - `public Void set_TextOverflow(TextOverflow value)`
  - `public Void set_TextShadow(Nullable<TextShadow> value)`
  - `public Void set_Underline(Nullable<Underline> value)`
  - `public Void set_VerticalAlignment(TextAlignment value)`
  - `private Void SetFontFamilyProperty(String value)`
  - `private Void SetFontSizeScaleProperty(Single value)`
  - `public Void SetFontVariation(String tag, Single value)`
  - `protected Void SetTextOverflow(TextOverflow textOverflow)`
  - `public Void SetTextPadding(Thickness thickness)`
  - `protected Void SizeInvalidated()`
  - `public Single AdjustedFontSizeScale { get; }`
  - `public String FontFamily { get; set; }`
  - `public Single FontSize { get; set; }`
  - `public Single FontSizeScale { get; set; }`
  - `public FontSlant FontSlant { get; set; }`
  - `private FontStyle FontStyle { get; set; }`
  - `public FontWeight FontWeight { get; set; }`
  - `public FontWidth FontWidth { get; set; }`
  - `public IntPtr Handle { get; }`
  - `public TextAlignment HorizontalAlignment { get; set; }`
  - `public Boolean IsDisposed { get; }`
  - `public Boolean IsMarkupEnabled { get; set; }`
  - `public Single MaximumFontSizeScale { get; set; }`
  - `public Single MinimumFontSizeScale { get; set; }`
  - `public Nullable<Outline> Outline { get; set; }`
  - `public View Owner { get; }`
  - `protected Boolean ShouldBlockTextChangedEvent { get; set; }`
  - `public Nullable<Strikethrough> Strikethrough { get; set; }`
  - `public Boolean SystemFontFamilyEnabled { get; set; }`
  - `public Boolean SystemFontSizeScaleEnabled { get; set; }`
  - `public String Text { get; set; }`
  - `public Color TextColor { get; set; }`
  - `public TextOverflow TextOverflow { get; set; }`
  - `public Nullable<TextShadow> TextShadow { get; set; }`
  - `public Nullable<Underline> Underline { get; set; }`
  - `public TextAlignment VerticalAlignment { get; set; }`

### class `TextPropertySetters`

- Base: `System.Object`
- Members:
  - `private static .cctor()`

### class `TextTranslator`

- Base: `System.Object`
- Members:
  - `private static .cctor()`

### class `ThemeColorTable`

- Base: `Tizen.UI.Components.ThemeTokenTable`1<Tizen.UI.Color>`
- Members:
  - `internal .ctor(IDictionary<String, Color> appTable)`
  - `private Void <.ctor>b__0_0(Object s, EventArgs e)`

### class `ThemeTokenTable`1`

- Base: `System.Object`
- Interfaces: `Tizen.UI.ITokenTable`1<T>`
- Members:
  - `internal .ctor(IDictionary<String, T> appTable, IDictionary<String, T> themeTable)`
  - `public event EventHandler Updated`
  - `public Void add_Updated(EventHandler value)`
  - `public Void Bind(View view, String name, Action<Object, T> setter, IToken token)`
  - `private Void ClearCache()`
  - `private Boolean IsLastHitFromStatic()`
  - `private Void OnUpdate()`
  - `public Void remove_Updated(EventHandler value)`
  - `private Void SetCache(String id, T value, Boolean cacheFromStatic)`
  - `private Boolean TryGetCache(String id, T& value)`
  - `public Boolean TryGetToken(View target, String propertyName, IToken& token)`
  - `public Boolean TryGetValue(String id, T& result)`
  - `private Boolean TryGetValueFromDynamic(String id, T& result)`
  - `private Boolean TryGetValueFromFallback(String id, T& result)`
  - `private Boolean TryGetValueFromStatic(String id, T& result)`
  - `public Void Unbind(View view, String name)`
  - `public Void Update(IDictionary<String, T> table)`
  - `private Boolean UpdateView(View view, String name, Action<Object, T> setter, T value)`

### class `TimeCounter`

- Base: `Tizen.UI.ContentView`
- Members:
  - `public .ctor()`
  - `public .ctor(Int32 totalTimeInMilliseconds)`
  - `public event EventHandler RemainingTimeChanged`
  - `public event EventHandler WarningChanged`
  - `private Void <.ctor>b__5_1(TimeCounter v, EventHandler handler)`
  - `private Void <.ctor>b__5_2(TimeCounter v, EventHandler handler)`
  - `public Void add_RemainingTimeChanged(EventHandler value)`
  - `public Void add_WarningChanged(EventHandler value)`
  - `public Boolean get_IsWarning()`
  - `public Int32 get_RemainingTime()`
  - `protected Single get_TimeRatio()`
  - `public Int32 get_TotalTime()`
  - `public Int32 get_WarningTime()`
  - `protected Void OnRemainingTimeChanged(KeyDeviceClass device)`
  - `protected Void OnTimeRatioChanged(KeyDeviceClass device)`
  - `protected Void OnWarningChanged()`
  - `public Void remove_RemainingTimeChanged(EventHandler value)`
  - `public Void remove_WarningChanged(EventHandler value)`
  - `private Void set_IsWarning(Boolean value)`
  - `public Void set_RemainingTime(Int32 value)`
  - `public Void set_TotalTime(Int32 value)`
  - `public Void set_WarningTime(Int32 value)`
  - `private Boolean SetRemainingTime(Int32 newValue, KeyDeviceClass device)`
  - `private Boolean SetTimeRatio(Single newRatio, KeyDeviceClass device)`
  - `private Void UpdateWarningState()`
  - `public Boolean IsWarning { get; set; }`
  - `public Int32 RemainingTime { get; set; }`
  - `protected Single TimeRatio { get; }`
  - `public Int32 TotalTime { get; set; }`
  - `public Int32 WarningTime { get; set; }`

### class `TizenFontFamilyLoader`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.IFontFamilyLoader`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `private event EventHandler _fontFamilyChanged`
  - `public event EventHandler FontFamilyChanged`
  - `private Void add__fontFamilyChanged(EventHandler value)`
  - `public Void add_FontFamilyChanged(EventHandler value)`
  - `public String get_FontFamily()`
  - `private Void OnFontFamilyChanged(Object sender, FontTypeChangedEventArgs args)`
  - `private Void remove__fontFamilyChanged(EventHandler value)`
  - `public Void remove_FontFamilyChanged(EventHandler value)`
  - `private Void set_FontFamily(String value)`
  - `public String FontFamily { get; set; }`

### class `TizenFontScaleLoader`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.IFontScaleLoader`
- Members:
  - `public .ctor()`
  - `public event EventHandler FontScaleChanged`
  - `public Void add_FontScaleChanged(EventHandler value)`
  - `public Single get_FontScale()`
  - `public Void remove_FontScaleChanged(EventHandler value)`
  - `public Single FontScale { get; }`

### class `TizenThemeLoader`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.IThemeLoader`
- Members:
  - `public .ctor()`
  - `public event EventHandler ThemeChanged`
  - `private Void <.ctor>b__1_0(Object s, ThemeEventArgs e)`
  - `public Void add_ThemeChanged(EventHandler value)`
  - `public String get_CurrentResourcePath()`
  - `public String get_CurrentThemeId()`
  - `public IDictionary<String, Color> LoadColorTable()`
  - `private Void LoadCurrentTheme()`
  - `public Void remove_ThemeChanged(EventHandler value)`
  - `private Void set_CurrentResourcePath(String value)`
  - `private Void set_CurrentThemeId(String value)`
  - `public String CurrentResourcePath { get; set; }`
  - `public String CurrentThemeId { get; set; }`

### class `TokenTableExtensions`

- Base: `System.Object`
- Members:
  - `public static Void Bind(View view, IPropertySetter<Color> setter, Color colorToken)`
  - `public static Void Unbind(ITokenTable<Color> colorTable, View target, IPropertySetter<Color> setter)`
  - `public static Void Unbind(View view, IPropertySetter<Color> setter)`

### class `TouchEffect`

- Base: `System.Object`
- Members:
  - `public static UIAttachable get_Default()`
  - `public static UIAttachable get_None()`
  - `public UIAttachable Default { get; }`
  - `public UIAttachable None { get; }`

### class `TouchHelper`

- Base: `System.Object`
- Members:
  - `public static Boolean HasTouchEnded(Touch touch)`
  - `public static Boolean IsTouchStart(Touch touch)`

### class `UIAttachable`

- Base: `System.Object`
- Members:
  - `protected .ctor()`
  - `public Void OnAttached(View view)`
  - `public Void OnDetached(View view)`

### class `UIAttachableManager`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Void Attach(View view, String key, UIAttachable attachable)`
  - `public static Void ClearAttachables(View view)`
  - `public static Void Detach(View view, String key)`
  - `public static UIAttachable GetAttachable(View view, String key)`

### class `UIConfig`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public IDictionary<String, Single> CreateBorderTable()`
  - `public IDictionary<String, Color> CreateColorTable()`
  - `public IDictionary<String, CornerRadius> CreateCornerTable()`
  - `public IDictionary<String, Color> CreateEffectColorTable()`
  - `public IFontFamilyLoader CreateFontFamilyLoader()`
  - `public IFontScaleLoader CreateFontScaleLoader()`
  - `public IDictionary<String, Single> CreateFontSizeTable()`
  - `public IDictionary<String, String> CreateFontTable()`
  - `public IDictionary<String, Single> CreateSpacingTable()`
  - `public IThemeLoader CreateThemeLoader()`
  - `public UIVariables CreateVariables()`
  - `public Int32 get_BaselineDpi()`
  - `public Boolean get_IgnoreDpi()`
  - `public Func<String, Boolean> get_IsBackKey()`
  - `public Func<String, Boolean> get_IsDecreaseKey()`
  - `public Func<String, Boolean> get_IsExecutionKey()`
  - `public Func<String, Boolean> get_IsIncreaseKey()`
  - `public UInt32 get_LongPressKeyCount()`
  - `public Single get_ScalingFactor()`
  - `protected Void set_BaselineDpi(Int32 value)`
  - `protected Void set_IgnoreDpi(Boolean value)`
  - `protected Void set_IsBackKey(Func<String, Boolean> value)`
  - `protected Void set_IsDecreaseKey(Func<String, Boolean> value)`
  - `protected Void set_IsExecutionKey(Func<String, Boolean> value)`
  - `protected Void set_IsIncreaseKey(Func<String, Boolean> value)`
  - `protected Void set_LongPressKeyCount(UInt32 value)`
  - `protected Void set_ScalingFactor(Single value)`
  - `public Int32 BaselineDpi { get; set; }`
  - `public Boolean IgnoreDpi { get; set; }`
  - `public Func<String, Boolean> IsBackKey { get; set; }`
  - `public Func<String, Boolean> IsDecreaseKey { get; set; }`
  - `public Func<String, Boolean> IsExecutionKey { get; set; }`
  - `public Func<String, Boolean> IsIncreaseKey { get; set; }`
  - `public UInt32 LongPressKeyCount { get; set; }`
  - `public Single ScalingFactor { get; set; }`

### class `UIConfigManager`

- Base: `System.Object`
- Members:
  - `public static UIConfig get_Current()`
  - `public static Void Init(UIConfig config)`
  - `internal static Void ThrowWhenNeverConfigured(String moduleName)`
  - `public UIConfig Current { get; }`

### class `UIShadowExtensions`

- Base: `System.Object`
- Members:
  - `public static IEnumerable<ColorVisual> CornerRadius(IEnumerable<ColorVisual> visuals, CornerRadius cornerRadius)`
  - `public static IEnumerable<ColorVisual> CornerSquareness(IEnumerable<ColorVisual> visuals, CornerRadius cornerSquareness)`
  - `public static IEnumerable<ColorVisual> ExtraSize(IEnumerable<ColorVisual> visuals, Single extraWidth, Single extraHeight)`
  - `public static ColorVisual ToVisual(Shadow shadow)`
  - `public static ColorVisual ToVisual(Shadow shadow, CornerRadius cornerRadius)`
  - `public static IEnumerable<ColorVisual> ToVisual(IEnumerable<Shadow> shadows)`
  - `public static IEnumerable<ColorVisual> ToVisual(IEnumerable<Shadow> shadows, CornerRadius cornerRadius)`
  - `public static IEnumerable<ColorVisual> ToVisual(IEnumerable<Shadow> shadows, CornerRadius cornerRadius, CornerRadius cornerSquareness)`
  - `public static ColorVisual ToVisual(Shadow shadow, CornerRadius cornerRadius, CornerRadius cornerSquareness)`

### struct `UIState`

- Base: `System.ValueType`
- Interfaces: `System.IEquatable`1<Tizen.UI.Components.UIState>`
- Members:
  - `private static .cctor()`
  - `private .ctor(UInt64 bitMask)`
  - `private .ctor(String name)`
  - `private static Boolean AddedOperation(UIState input, UIStateChangedEventArgs e)`
  - `private static Boolean ChangedOperation(UIState input, UIStateChangedEventArgs e)`
  - `public Boolean Contains(UIState state)`
  - `public static UIState Create(String name)`
  - `public Boolean Equals(UIState other)`
  - `public Boolean Equals(Object obj)`
  - `private static Boolean ExcludedOperation(UIState input, UIStateChangedEventArgs e)`
  - `public UIStateConstraint get_Added()`
  - `public UIStateConstraint get_Changed()`
  - `public UIStateConstraint get_Excluded()`
  - `private static UInt64 get_FullMask()`
  - `public UIStateConstraint get_Included()`
  - `public UIStateConstraint get_Is()`
  - `internal Boolean get_IsCombined()`
  - `public UIStateConstraint get_Not()`
  - `public UIStateConstraint get_Removed()`
  - `public Int32 GetHashCode()`
  - `public Boolean HasIntersectionWith(UIState other)`
  - `private static Boolean IncludedOperation(UIState input, UIStateChangedEventArgs e)`
  - `private static Boolean IsOperation(UIState input, UIStateChangedEventArgs e)`
  - `private static Boolean NotOperation(UIState input, UIStateChangedEventArgs e)`
  - `public static UIState op_Addition(UIState lhs, UIState rhs)`
  - `public static Boolean op_Equality(UIState lhs, UIState rhs)`
  - `public static Boolean op_Inequality(UIState lhs, UIState rhs)`
  - `public static UIState op_OnesComplement(UIState state)`
  - `public static UIState op_Subtraction(UIState lhs, UIState rhs)`
  - `private static UInt64 Register(String stateName)`
  - `private static IEnumerable<ValueTuple<String, UInt64>> RegisteredStates()`
  - `private static Boolean RemovedOperation(UIState input, UIStateChangedEventArgs e)`
  - `public String ToString()`
  - `public UIStateConstraint Added { get; }`
  - `public UIStateConstraint Changed { get; }`
  - `public UIStateConstraint Excluded { get; }`
  - `private UInt64 FullMask { get; }`
  - `public UIStateConstraint Included { get; }`
  - `public UIStateConstraint Is { get; }`
  - `internal Boolean IsCombined { get; }`
  - `public UIStateConstraint Not { get; }`
  - `public UIStateConstraint Removed { get; }`

### class `UIStateChangedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor()`
  - `public UIState get_Current()`
  - `public UIState get_Previous()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Current(UIState value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Previous(UIState value)`
  - `public Boolean Test(UIStateConstraint constraint)`
  - `public UIState Current { get; set; }`
  - `public UIState Previous { get; set; }`

### class `UIStateConnectingProperty`1`

- Base: `System.Object`
- Members:
  - `public .ctor(Func<TView, Boolean> getter, Action<TView, EventHandler> addObserver, Action<TView, EventHandler> removeObserver)`
  - `public Action<TView, EventHandler> get_AddObserver()`
  - `public Func<TView, Boolean> get_Getter()`
  - `public Action<TView, EventHandler> get_RemoveObserver()`
  - `private Void set_AddObserver(Action<TView, EventHandler> value)`
  - `private Void set_Getter(Func<TView, Boolean> value)`
  - `private Void set_RemoveObserver(Action<TView, EventHandler> value)`
  - `public Action<TView, EventHandler> AddObserver { get; set; }`
  - `public Func<TView, Boolean> Getter { get; set; }`
  - `public Action<TView, EventHandler> RemoveObserver { get; set; }`

### struct `UIStateConstraint`

- Base: `System.ValueType`
- Interfaces: `System.IEquatable`1<Tizen.UI.Components.UIStateConstraint>`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public .ctor(UIState state, Func<UIState, UIStateChangedEventArgs, Boolean> constraint)`
  - `public Boolean Equals(Object other)`
  - `public Boolean Equals(UIStateConstraint other)`
  - `public Int32 GetHashCode()`
  - `public Boolean Match(UIStateChangedEventArgs e)`
  - `public static Boolean op_Equality(UIStateConstraint operand1, UIStateConstraint operand2)`
  - `public static UIStateConstraint op_Implicit(UIState state)`
  - `public static Boolean op_Inequality(UIStateConstraint operand1, UIStateConstraint operand2)`

### class `UIStateManager`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Void AddStateChangedEventHandler(TView view, EventHandler<UIStateChangedEventArgs> handler)`
  - `internal static Void AddStateChangedEventHandler(TView view, UIStateConstraint constraint, EventHandler<UIStateChangedEventArgs> handler)`
  - `private static Boolean AddToPendingUIStateIfNeed(View view, UIState viewState, Boolean isActivated, KeyDeviceClass inputDevice)`
  - `private static UIState By(UIState viewState, KeyDeviceClass inputDevice)`
  - `private static UIState ByAll(UIState viewState)`
  - `public static Void ConnectUIState(TView view, UIState viewState, UIStateConnectingProperty<TView> connectingProperty)`
  - `private static Void ConnectUIStateInternal(TView target, UIState viewState, UIStateConnectingProperty<TConnecting> connectingProperty)`
  - `public static Void DisconnectUIState(View view, UIState viewState)`
  - `private static UIStateData EnsureAttachedUIStateData(View target)`
  - `private static UIStateConnectingProperty<View> get_DisabledUIStateConnectingProperty()`
  - `private static UIStateConnectingProperty<View> get_FocusedUIStateConnectingProperty()`
  - `private static UIStateConnectingProperty<IPressable> get_PressedUIStateConnectingProperty()`
  - `private static UIStateConnectingProperty<ISelectable> get_SelectedUIStateConnectingProperty()`
  - `private static Dictionary<UIState, Action> GetAttachedConnectionCleaners(TView target)`
  - `private static KeyDeviceClass GetInputDevice(EventArgs e)`
  - `public static UIState GetState(View view)`
  - `private static Void InvokeUIStateActions(View target, UIStateChangedEventArgs eventArgs)`
  - `private static Void ProcessPendingUIState()`
  - `public static Void RemoveStateChangedEventHandler(TView view, EventHandler<UIStateChangedEventArgs> handler)`
  - `private static Void SetupInitialUIState(View target)`
  - `private static Void UpdateUIState(View view, UIState changedUIState, Boolean isActivated, KeyDeviceClass inputDevice)`
  - `private static Void UpdateUIState(View view, UIState newUIState)`
  - `private UIStateConnectingProperty<View> DisabledUIStateConnectingProperty { get; }`
  - `private UIStateConnectingProperty<View> FocusedUIStateConnectingProperty { get; }`
  - `private UIStateConnectingProperty<IPressable> PressedUIStateConnectingProperty { get; }`
  - `private UIStateConnectingProperty<ISelectable> SelectedUIStateConnectingProperty { get; }`

### class `UITextManager`

- Base: `System.Object`
- Members:
  - `public event EventHandler SystemFontFamilyChanged`
  - `internal event EventHandler SystemFontFamilyChangedInternal`
  - `public event EventHandler SystemFontScaleChanged`
  - `internal event EventHandler SystemFontScaleChangedInternal`
  - `public event EventHandler SystemLocaleLanguageChanged`
  - `internal event EventHandler SystemLocaleLanguageChangedInternal`
  - `public static Void add_SystemFontFamilyChanged(EventHandler value)`
  - `internal static Void add_SystemFontFamilyChangedInternal(EventHandler value)`
  - `public static Void add_SystemFontScaleChanged(EventHandler value)`
  - `internal static Void add_SystemFontScaleChangedInternal(EventHandler value)`
  - `public static Void add_SystemLocaleLanguageChanged(EventHandler value)`
  - `internal static Void add_SystemLocaleLanguageChangedInternal(EventHandler value)`
  - `private static IFontFamilyLoader EnsureFontFamilyLoader()`
  - `private static IFontScaleLoader EnsureFontScaleLoader()`
  - `private static Void EnsureLocaleLanguageChanged()`
  - `public static String get_SystemFontFamily()`
  - `public static Single get_SystemFontScale()`
  - `public static String get_SystemLocaleLanguage()`
  - `public static Void remove_SystemFontFamilyChanged(EventHandler value)`
  - `internal static Void remove_SystemFontFamilyChangedInternal(EventHandler value)`
  - `public static Void remove_SystemFontScaleChanged(EventHandler value)`
  - `internal static Void remove_SystemFontScaleChangedInternal(EventHandler value)`
  - `public static Void remove_SystemLocaleLanguageChanged(EventHandler value)`
  - `internal static Void remove_SystemLocaleLanguageChangedInternal(EventHandler value)`
  - `public String SystemFontFamily { get; }`
  - `public Single SystemFontScale { get; }`
  - `public String SystemLocaleLanguage { get; }`

### class `UIThemeManager`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public event EventHandler ThemeChanged`
  - `public static Void add_ThemeChanged(EventHandler value)`
  - `public static String get_CurrentResourcePath()`
  - `public static String get_CurrentThemeId()`
  - `public static IThemeLoader get_CurrentThemeLoader()`
  - `public static Void remove_ThemeChanged(EventHandler value)`
  - `public String CurrentResourcePath { get; }`
  - `public String CurrentThemeId { get; }`
  - `public IThemeLoader CurrentThemeLoader { get; }`

### class `UIVariables`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Lazy<UIAttachable> get_ClickableSoundEffect()`
  - `public Lazy<UIAttachable> get_ClickableTouchEffect()`
  - `public Lazy<UIAttachable> get_ComponentDisabledEffect()`
  - `public static UIVariables get_Current()`
  - `public Int32 get_EffectDelayOnScroll()`
  - `public Func<Variables> get_Item(String name)`
  - `public Single get_MaximumFontSizeScale()`
  - `public Single get_MinimumFontSizeScale()`
  - `public Color get_RecoilColor()`
  - `public Single get_RecoilOffset()`
  - `public Single get_RecoilScaleFactor()`
  - `public Single get_RecoilScaleFactorForList()`
  - `public Boolean get_ResizePolicyEnabled()`
  - `public Point get_ScaffoldFloatingOffset()`
  - `public Boolean get_SystemFontSizeScaleEnabled()`
  - `public Single get_SystemIndicatorThickness()`
  - `public Single get_SystemNavigationBarThickness()`
  - `public Color get_TextAnchorClickedColor()`
  - `public Color get_TextAnchorColor()`
  - `public Color get_TextCursorColor()`
  - `public Int32 get_TextCursorWidth()`
  - `public Single get_TextMarqueeGap()`
  - `public Int32 get_TextMarqueeLoopCount()`
  - `public Int32 get_TextMarqueeSpeed()`
  - `public Color get_TextSelectionColor()`
  - `public String get_TextSelectionHandleLeftImage()`
  - `public String get_TextSelectionHandleRightImage()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_ClickableSoundEffect(Lazy<UIAttachable> value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_ClickableTouchEffect(Lazy<UIAttachable> value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_ComponentDisabledEffect(Lazy<UIAttachable> value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_EffectDelayOnScroll(Int32 value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Item(String name, Func<Variables> value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_MaximumFontSizeScale(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_MinimumFontSizeScale(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_RecoilColor(Color value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_RecoilOffset(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_RecoilScaleFactor(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_RecoilScaleFactorForList(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_ResizePolicyEnabled(Boolean value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_ScaffoldFloatingOffset(Point value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_SystemFontSizeScaleEnabled(Boolean value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_SystemIndicatorThickness(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_SystemNavigationBarThickness(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextAnchorClickedColor(Color value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextAnchorColor(Color value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextCursorColor(Color value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextCursorWidth(Int32 value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextMarqueeGap(Single value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextMarqueeLoopCount(Int32 value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextMarqueeSpeed(Int32 value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextSelectionColor(Color value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextSelectionHandleLeftImage(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TextSelectionHandleRightImage(String value)`
  - `public Lazy<UIAttachable> ClickableSoundEffect { get; set; }`
  - `public Lazy<UIAttachable> ClickableTouchEffect { get; set; }`
  - `public Lazy<UIAttachable> ComponentDisabledEffect { get; set; }`
  - `public UIVariables Current { get; }`
  - `public Int32 EffectDelayOnScroll { get; set; }`
  - `public Func<Variables> Item { get; set; }`
  - `public Single MaximumFontSizeScale { get; set; }`
  - `public Single MinimumFontSizeScale { get; set; }`
  - `public Color RecoilColor { get; set; }`
  - `public Single RecoilOffset { get; set; }`
  - `public Single RecoilScaleFactor { get; set; }`
  - `public Single RecoilScaleFactorForList { get; set; }`
  - `public Boolean ResizePolicyEnabled { get; set; }`
  - `public Point ScaffoldFloatingOffset { get; set; }`
  - `public Boolean SystemFontSizeScaleEnabled { get; set; }`
  - `public Single SystemIndicatorThickness { get; set; }`
  - `public Single SystemNavigationBarThickness { get; set; }`
  - `public Color TextAnchorClickedColor { get; set; }`
  - `public Color TextAnchorColor { get; set; }`
  - `public Color TextCursorColor { get; set; }`
  - `public Int32 TextCursorWidth { get; set; }`
  - `public Single TextMarqueeGap { get; set; }`
  - `public Int32 TextMarqueeLoopCount { get; set; }`
  - `public Int32 TextMarqueeSpeed { get; set; }`
  - `public Color TextSelectionColor { get; set; }`
  - `public String TextSelectionHandleLeftImage { get; set; }`
  - `public String TextSelectionHandleRightImage { get; set; }`

### class `UIVectorExtensions`

- Base: `System.Object`
- Members:
  - `public static Boolean HasZero(Point vector2)`
  - `public static Boolean HasZero(Size vector2)`
  - `public static Boolean HasZeroSize(Rect rect)`

### class `Variables`

- Base: `System.Object`
- Interfaces: `System.IEquatable`1<Tizen.UI.Components.Variables>`
- Members:
  - `public .ctor()`
  - `protected .ctor(Variables other)`
  - `public Variables <Clone>$()`
  - `public Boolean Equals(Object obj)`
  - `public Boolean Equals(Variables other)`
  - `protected Type get_EqualityContract()`
  - `public Object get_Item(String name)`
  - `public Int32 GetHashCode()`
  - `public T GetNullable(String name, T fallback)`
  - `public Boolean HasNullable(String name)`
  - `public static Boolean op_Equality(Variables left, Variables right)`
  - `public static Boolean op_Inequality(Variables left, Variables right)`
  - `protected Boolean PrintMembers(StringBuilder builder)`
  - `protected Boolean RemoveNullable(String name)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Item(String name, Object value)`
  - `protected Void SetNullable(String name, Object value)`
  - `public String ToString()`
  - `public Boolean TryGetNullable(String name, T& value)`
  - `protected Type EqualityContract { get; }`
  - `public Object Item { get; set; }`

### class `ViewExtensions`

- Base: `System.Object`
- Members:
  - `public static Void AttachChild(View parent, View child)`
  - `public static View BackgroundColor(View view, Color backgroundColor)`
  - `public static Color BackgroundColor(View view)`
  - `public static View BorderlineColor(View view, Color borderlineColor)`
  - `public static Color BorderlineColor(View view)`
  - `public static View Color(View view, Color color)`
  - `public static Color Color(View view)`
  - `public static Void DetachChild(View parent, View child)`
  - `public static Void DetachFromParent(View view)`
  - `public static TView DisabledEffect(TView view, UIAttachable effect)`
  - `public static INavigation GetNavigation(TView view)`
  - `public static INavigation GetRootNavigation(TView view)`
  - `public static Rect GetScreenBounds(TView view)`
  - `public static TView SoundEffect(TView view, UIAttachable effect)`
  - `public static TView TouchEffect(TView view, UIAttachable effect)`
  - `public static TView UnderShadow(TView view, VisualObject[] visuals)`
  - `public static TView UnderShadow(TView view, IEnumerable<VisualObject> visuals)`
  - `public static TView UnsetWhen(TView view, Action<TView, UIStateChangedEventArgs> handler)`
  - `public static TView UnsetWhen(TView view, String key)`
  - `public static TView UnsetWhen(TView view)`
  - `public static TView UnsetWhenIfNotProcessing(TView view, String key)`
  - `public static TView When(TView view, UIStateConstraint constraint, Action<TView, UIStateChangedEventArgs> handler, Boolean initialExecution)`
  - `public static TView When(TView view, String key, UIStateConstraint constraint, Action<TView, UIStateChangedEventArgs> handler, Boolean initialExecution)`
  - `public static TView When(TView view, Action<TView, UIStateChangedEventArgs> handler, Boolean initialExecution)`
  - `public static TView When(TView view, String key, Action<TView, UIStateChangedEventArgs> handler, Boolean initialExecution)`

### class `ViewPropertySetters`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static PropertySetter<View, Color> BackgroundObjectBind(View view, ColorBackground background)`
  - `public static Action<Object, Color> ShadowBind(Shadow shadow)`
  - `public static PropertySetter<View, Color> ShadowBind(View view, Shadow shadow)`

### class `ViewTokenInfo`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public static ViewTokenInfo Ensure(View view)`
  - `public Void InvokeWithGuard(Action<Object, T> setter, View view, T value)`
  - `public Boolean IsTokenApplied(String propertyName)`
  - `private Void OnViewPropertySet(String propertyName)`
  - `public Void SetTokenApplied(String propertyName)`
  - `public Void UnsetTokenApplied(String propertyName)`

### class `WhenHandler`1`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.IWhenHandler`
- Members:
  - `public .ctor(UIStateConstraint constraint, Action<T, UIStateChangedEventArgs> action)`
  - `public Void Execute(Object target, UIStateChangedEventArgs e)`
  - `public Void ExecuteForce(Object target, UIStateChangedEventArgs e)`
  - `public Boolean MatchAction(Delegate action)`
  - `private Void OnUIStateChanged(Object sender, UIStateChangedEventArgs e)`
  - `public Void Register(View view)`
  - `public Void Unregister(View view)`

### class `WhenHandlers`

- Base: `System.Collections.Generic.List`1<System.ValueTuple`2<System.String,Tizen.UI.Components.IWhenHandler>>`
- Members:
  - `public .ctor()`
  - `public Void Clear(View view)`
  - `public static Int32 get_HandlingCount()`
  - `public static Boolean get_IsProcessingWhen()`
  - `public Void Register(View view, String key, IWhenHandler whenHandler, Boolean initialExecution)`
  - `public static Void set_HandlingCount(Int32 value)`
  - `public Void UnregisterAll(View view, String key)`
  - `public Void UnregisterAll(View view, Delegate action)`
  - `public Int32 HandlingCount { get; set; }`
  - `public Boolean IsProcessingWhen { get; }`

## Namespace `Tizen.UI.Components.Animations`

### class `AnimatableKey`

- Base: `System.Object`
- Members:
  - `public .ctor(View animatable, String handle)`
  - `public Boolean Equals(Object obj)`
  - `protected Boolean Equals(AnimatableKey other)`
  - `public WeakReference<View> get_Animatable()`
  - `public String get_Handle()`
  - `public Int32 GetHashCode()`
  - `public WeakReference<View> Animatable { get; }`
  - `public String Handle { get; }`

### class `AnimationExtensions`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public static Boolean AbortAnimation(View self, String handle)`
  - `internal static Void AbortAnimation(AnimatableKey key)`
  - `internal static Void AbortKinetic(AnimatableKey key)`
  - `public static Void Animate(View self, String name, UIAnimation animation, UInt32 rate, UInt32 length, Easing easing, Action<Single, Boolean> finished, Func<Boolean> repeat)`
  - `public static Void Animate(View self, String name, Action<Single> callback, Single start, Single end, UInt32 rate, UInt32 length, Easing easing, Action<Single, Boolean> finished, Func<Boolean> repeat)`
  - `public static Void Animate(View self, String name, Action<Single> callback, UInt32 rate, UInt32 length, Easing easing, Action<Single, Boolean> finished, Func<Boolean> repeat)`
  - `public static Void Animate(View self, String name, Func<Single, T> transform, Action<T> callback, UInt32 rate, UInt32 length, Easing easing, Action<T, Boolean> finished, Func<Boolean> repeat)`
  - `internal static Void AnimateInternal(View self, String name, Func<Single, T> transform, Action<T> callback, UInt32 rate, UInt32 length, Easing easing, Action<T, Boolean> finished, Func<Boolean> repeat)`
  - `public static Void AnimateKinetic(View self, String name, Func<Single, Single, Boolean> callback, Single velocity, Single drag, Action finished)`
  - `internal static Void AnimateKineticInternal(View self, String name, Func<Single, Single, Boolean> callback, Single velocity, Single drag, Action finished)`
  - `public static Boolean AnimationIsRunning(View self, String handle)`
  - `internal static Void HandleTweenerFinished(Object o, EventArgs args)`
  - `internal static Void HandleTweenerUpdated(Object o, EventArgs args)`
  - `public static Func<Single, Single> Interpolate(Single start, Single end, Single reverseVal, Boolean reverse)`
  - `internal static Void Self(View self, Action action)`

### class `Bezier`

- Base: `Tizen.UI.Components.Animations.Easing`
- Members:
  - `public .ctor(Point p1, Point p2)`
  - `internal static Single EvaluateCubicBezier(Single p0, Single p1, Single t)`

### class `BezierValues`

- Base: `System.Object`
- Members:
  - `private static .cctor()`

### class `Easing`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public .ctor(Func<Single, Single> easingFunc)`
  - `public Single Ease(Single v)`
  - `public static Easing op_Implicit(Func<Single, Single> func)`

### class `Ticker`

- Base: `System.Object`
- Members:
  - `protected .ctor()`
  - `private Void <.ctor>b__7_0(Object o)`
  - `private Void <HandleElapsed>b__18_0(Object o)`
  - `internal Void Disable()`
  - `protected Void DisableTimer()`
  - `internal Void Enable()`
  - `protected Void EnableTimer()`
  - `public static Ticker get_Default()`
  - `internal Void HandleElapsed(Object state)`
  - `public Int32 Insert(Func<Int64, Boolean> timeout)`
  - `public Void Remove(Int32 handle)`
  - `protected Void RemoveTimeout(Int32 handle)`
  - `protected Void SendFinish()`
  - `protected Void SendSignals(Int32 timestep)`
  - `protected Void SendSignals(Int64 step)`
  - `public Ticker Default { get; }`

### class `Tweener`

- Base: `System.Object`
- Members:
  - `public .ctor(UInt32 length)`
  - `public .ctor(UInt32 length, UInt32 rate)`
  - `public event EventHandler Finished`
  - `public event EventHandler ValueUpdated`
  - `private Boolean <Start>b__27_0(Int64 step)`
  - `public Void add_Finished(EventHandler value)`
  - `public Void add_ValueUpdated(EventHandler value)`
  - `protected Void Finalize()`
  - `public AnimatableKey get_Handle()`
  - `public Boolean get_IsLooping()`
  - `public UInt32 get_Length()`
  - `public UInt32 get_Rate()`
  - `public Single get_Value()`
  - `public Void Pause()`
  - `public Void remove_Finished(EventHandler value)`
  - `public Void remove_ValueUpdated(EventHandler value)`
  - `public Void set_Handle(AnimatableKey value)`
  - `public Void set_IsLooping(Boolean value)`
  - `private Void set_Value(Single value)`
  - `public Void Start()`
  - `public Void Stop()`
  - `public AnimatableKey Handle { get; set; }`
  - `public Boolean IsLooping { get; set; }`
  - `public UInt32 Length { get; }`
  - `public UInt32 Rate { get; }`
  - `public Single Value { get; set; }`

### class `TypedAnimationExtensions`

- Base: `System.Object`
- Members:
  - `public static TypedAnimationTargetBridge<T> Animate(Animation animation, T targetView)`
  - `public static Animation BackgroundColorBy(TypedAnimationTargetBridge<T> target, Color relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation BackgroundColorTo(TypedAnimationTargetBridge<T> target, Color destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation BoxShadowBlurRadiusBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation BoxShadowBlurRadiusTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation BoxShadowOffsetBy(TypedAnimationTargetBridge<T> target, Single x, Single y, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation BoxShadowOffsetTo(TypedAnimationTargetBridge<T> target, Single x, Single y, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation BoxShadowOpacityBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation BoxShadowOpacityTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation ColorBy(TypedAnimationTargetBridge<T> target, Color relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation ColorTo(TypedAnimationTargetBridge<T> target, Color destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation CornerRadiusBy(TypedAnimationTargetBridge<T> target, CornerRadius relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation CornerRadiusTo(TypedAnimationTargetBridge<T> target, CornerRadius destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation CornerSquarenessBy(TypedAnimationTargetBridge<T> target, CornerRadius relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation CornerSquarenessTo(TypedAnimationTargetBridge<T> target, CornerRadius destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation HeightBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation HeightTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation OpacityBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation OpacityTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation PositionBy(TypedAnimationTargetBridge<T> target, Point relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation PositionBy(TypedAnimationTargetBridge<T> target, Single relativeX, Single relativeY, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation PositionTo(TypedAnimationTargetBridge<T> target, Point destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation PositionTo(TypedAnimationTargetBridge<T> target, Single destinationX, Single destinationY, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation PositionXBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation PositionXTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation PositionYBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation PositionYTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation ScaleBy(TypedAnimationTargetBridge<T> target, Size relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation ScaleTo(TypedAnimationTargetBridge<T> target, Size destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation ScaleXBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation ScaleXTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation ScaleYBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation ScaleYTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation SizeBy(TypedAnimationTargetBridge<T> target, Size relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation SizeBy(TypedAnimationTargetBridge<T> target, Single relativeX, Single relativeY, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation SizeTo(TypedAnimationTargetBridge<T> target, Size destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation SizeTo(TypedAnimationTargetBridge<T> target, Single destinationX, Single destinationY, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation TextColorBy(TypedAnimationTargetBridge<T> target, Color relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation TextColorTo(TypedAnimationTargetBridge<T> target, Color destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation WidthBy(TypedAnimationTargetBridge<T> target, Single relativeValue, Int32 duration, AlphaFunction alpha, Int32 delay)`
  - `public static Animation WidthTo(TypedAnimationTargetBridge<T> target, Single destinationValue, Int32 duration, AlphaFunction alpha, Int32 delay)`

### struct `TypedAnimationTargetBridge`1`

- Base: `System.ValueType`
- Members:
  - `public T get_TargetView()`
  - `public Animation get_TypedAnimation()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TargetView(T value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_TypedAnimation(Animation value)`
  - `public T TargetView { get; set; }`
  - `public Animation TypedAnimation { get; set; }`

### class `UIAnimation`

- Base: `System.Object`
- Interfaces: `System.Collections.IEnumerable`
- Members:
  - `public .ctor()`
  - `public .ctor(Action<Single> callback, Single start, Single end, Easing easing, Action finished)`
  - `private Void <GetCallback>b__13_0(Single f)`
  - `public Void Abort(View owner, String name)`
  - `public Void Add(Single beginAt, Single finishAt, UIAnimation animation)`
  - `public Void Commit(View owner, String name, UInt32 rate, UInt32 length, Easing easing, Action<Single, Boolean> finished, Func<Boolean> repeat)`
  - `public Action<Single> GetCallback()`
  - `public IEnumerator GetEnumerator()`
  - `public UIAnimation Insert(Single beginAt, Single finishAt, UIAnimation animation)`
  - `internal Void ResetChildren()`
  - `public UIAnimation WithConcurrent(UIAnimation animation, Single beginAt, Single finishAt)`
  - `public UIAnimation WithConcurrent(Action<Single> callback, Single start, Single end, Easing easing, Single beginAt, Single finishAt)`

## Namespace `Tizen.UI.Components.Internal`

### class `TokenPropertyChangedHandler`

- Base: `System.Object`
- Members:
  - `public static Void Init()`
  - `private static Void OnTokenPropertyChanged(Object sender, TokenPropertyChangedEventArgs e)`

## Namespace `Tizen.UI.Components.Recycler`

### class `Adapter`

- Base: `System.Object`
- Interfaces: `System.Collections.Specialized.INotifyCollectionChanged`
- Members:
  - `protected .ctor()`
  - `public event NotifyCollectionChangedEventHandler CollectionChanged`
  - `public Void add_CollectionChanged(NotifyCollectionChangedEventHandler value)`
  - `public Void BindViewHolder(ViewHolder holder, Int32 position)`
  - `public ViewHolder CreateViewHolder(Int32 viewType)`
  - `public Int32 get_ItemCount()`
  - `public RecyclerView get_RecyclerView()`
  - `public Object GetItemOnPosition(Int32 position)`
  - `public Int32 GetItemViewType(Int32 position)`
  - `public Int32 GetViewPosition(View view)`
  - `public Boolean IsSticky(Int32 position)`
  - `public Void OnBindViewHolder(ViewHolder holder, Int32 position)`
  - `public ViewHolder OnCreateViewHolder(Int32 viewType)`
  - `protected Void OnUpdateRecyclerView()`
  - `public Void remove_CollectionChanged(NotifyCollectionChangedEventHandler value)`
  - `protected Void SendNotifyCollectionChanged(NotifyCollectionChangedEventArgs args)`
  - `public Void set_RecyclerView(RecyclerView value)`
  - `public Int32 ItemCount { get; }`
  - `public RecyclerView RecyclerView { get; set; }`

### class `AnimationFinishedEventArgs`

- Base: `System.EventArgs`
- Members:
  - `public .ctor(ViewHolder viewHolder, AnimationType animationType)`
  - `public AnimationType get_AnimationType()`
  - `public ViewHolder get_ViewHolder()`
  - `public AnimationType AnimationType { get; }`
  - `public ViewHolder ViewHolder { get; }`

### enum `AnimationType`

- Base: `System.Enum`
- Members:

### class `DefaultItemAnimator`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.Recycler.IItemAnimator`
- Members:
  - `public .ctor()`
  - `public event EventHandler<AnimationFinishedEventArgs> AnimationFinished`
  - `public Void add_AnimationFinished(EventHandler<AnimationFinishedEventArgs> value)`
  - `public Boolean AnimateAdd(ViewHolder holder)`
  - `private Void AnimateAddImpl(ViewHolder holder)`
  - `public Boolean AnimateChange(ViewHolder oldHolder, ViewHolder newHolder, Single fromX, Single fromY, Single toX, Single toY)`
  - `private Void AnimateChangeImpl(ViewHolder oldHolder, ViewHolder newHolder, Single fromX, Single fromY, Single toX, Single toY)`
  - `public Boolean AnimateMove(ViewHolder holder, Single fromX, Single fromY, Single toX, Single toY)`
  - `private Void AnimateMoveImpl(ViewHolder holder, Single fromX, Single fromY, Single toX, Single toY)`
  - `public Boolean AnimateRemove(ViewHolder holder)`
  - `private Void AnimateRemoveImpl(ViewHolder holder)`
  - `private Void DecrementAnimationCount(AnimationType type, ViewHolder holder)`
  - `public Void EndAnimation(ViewHolder item)`
  - `public Void EndAnimations()`
  - `public RecyclerView get_RecyclerView()`
  - `private Void IncrementAnimationCount(AnimationType type)`
  - `public Boolean IsRunning(ViewHolder item)`
  - `private Void OnAnimationFinished(ViewHolder holder, AnimationType type, Animation animation)`
  - `public Void remove_AnimationFinished(EventHandler<AnimationFinishedEventArgs> value)`
  - `public Void RunPendingAnimations()`
  - `public Void set_RecyclerView(RecyclerView value)`
  - `public RecyclerView RecyclerView { get; set; }`

### class `ExclusiveSelectionModelGroup`1`

- Base: `System.Collections.ObjectModel.ObservableCollection`1<T>`
- Members:
  - `protected .ctor()`
  - `protected TType Get(String name)`
  - `public Boolean get_IsSelectable()`
  - `public T get_Selected()`
  - `private Void OnChildPropertyChanged(Object sender, PropertyChangedEventArgs e)`
  - `protected Void OnCollectionChanged(NotifyCollectionChangedEventArgs e)`
  - `protected Void OnPropertyChanged(String propertyName)`
  - `protected Void Set(TType value, String name)`
  - `public Void set_IsSelectable(Boolean value)`
  - `public Void set_Selected(T value)`
  - `public Boolean IsSelectable { get; set; }`
  - `public T Selected { get; set; }`

### class `GridLayoutManager`

- Base: `Tizen.UI.Components.Recycler.LayoutManager`
- Members:
  - `public .ctor(UInt32 spanCount)`
  - `private Void <OnChildrenChanged>b__41_0(Object _, EventArgs _)`
  - `private Void <OnChildrenChanged>b__41_1(Object sender, AnimationFinishedEventArgs args)`
  - `private Void ClearSnapShot()`
  - `internal Void ClearSpanIndexCache()`
  - `public Int32 ComputeHorizontalScrollExtent(Int32 first, Int32 last)`
  - `public Int32 ComputeHorizontalScrollOffset(Int32 first, Int32 last)`
  - `public Int32 ComputeHorizontalScrollRange(Int32 first, Int32 last)`
  - `private Int32 ComputeScrollExtent(Int32 first, Int32 last)`
  - `private Int32 ComputeScrollOffset(Int32 first, Int32 last)`
  - `private Int32 ComputeScrollRange(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollExtent(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollOffset(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollRange(Int32 first, Int32 last)`
  - `public Int32 Fill(Int32 requested, Int32 position)`
  - `private IGroupedAdapter get__groupAdapter()`
  - `private Single get__height()`
  - `private Boolean get__isGrouped()`
  - `private Single get__width()`
  - `public Boolean get_CanScrollHorizontally()`
  - `public Boolean get_CanScrollVertically()`
  - `internal Boolean get_IsBatchUpdate()`
  - `public Boolean get_IsHorizontal()`
  - `public IItemAnimator get_ItemAnimator()`
  - `public Rect get_LayoutViewPort()`
  - `public UInt32 get_SpanCount()`
  - `private Single GetAspectRatio(View view, Int32 position)`
  - `private UInt32 GetSpanIndex(Int32 position)`
  - `private SpanInfo GetSpanInfo(Int32 position)`
  - `private UInt32 GetSpanSize(Int32 position)`
  - `public Void Initialize()`
  - `private Void InvalidateCache()`
  - `private Boolean IsValidPosition(Int32 position)`
  - `private Int32 LayoutChunk(Int32& position, Int32 direction)`
  - `public Void LayoutPosition(Int32 position)`
  - `public Size MeasureUpdate(View itemView)`
  - `private Size MeasureUpdate(View itemView, Single width, Single height)`
  - `private View Next(Int32 position, Int32 direction)`
  - `public Void OnChildrenChanged(NotifyCollectionChangedEventArgs e)`
  - `public Void OnLayoutChildren()`
  - `private Void RecycleCreatedView()`
  - `private Void RecycleInvisibleViews(List<View> toRemove)`
  - `public Void RecycleOutOfSightView()`
  - `private Int32 RecycleOutOfSightViewItems(Int32 position, HashSet<View> toRemove)`
  - `private Void RunSnapChangedChildren()`
  - `public Int32 ScrollHorizontallyBy(Int32 dx)`
  - `public Int32 ScrollVerticallyBy(Int32 dy)`
  - `internal Void set_IsBatchUpdate(Boolean value)`
  - `public Void set_IsHorizontal(Boolean value)`
  - `public Void set_ItemAnimator(IItemAnimator value)`
  - `public Void set_LayoutViewPort(Rect value)`
  - `public Void set_SpanCount(UInt32 value)`
  - `private Void SetSpanIndex(Int32 position, UInt32 index)`
  - `private Void SnapChildrenBounds()`
  - `private Void UpdateGroupBody()`
  - `private Void UpdateGroupBodyInfo(Int32 position, Rect frame)`
  - `private IGroupedAdapter _groupAdapter { get; }`
  - `private Single _height { get; }`
  - `private Boolean _isGrouped { get; }`
  - `private Single _width { get; }`
  - `public Boolean CanScrollHorizontally { get; }`
  - `public Boolean CanScrollVertically { get; }`
  - `internal Boolean IsBatchUpdate { get; set; }`
  - `public Boolean IsHorizontal { get; set; }`
  - `public IItemAnimator ItemAnimator { get; set; }`
  - `public Rect LayoutViewPort { get; set; }`
  - `public UInt32 SpanCount { get; set; }`

### class `GroupInfo`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Single get_Bottom()`
  - `public Int32 get_Count()`
  - `public Int32 get_End()`
  - `public Single get_Left()`
  - `public Boolean get_NeedLayout()`
  - `public Int32 get_Position()`
  - `public Single get_Right()`
  - `public Int32 get_Start()`
  - `public Single get_Top()`
  - `public Void set_Bottom(Single value)`
  - `public Void set_Count(Int32 value)`
  - `public Void set_End(Int32 value)`
  - `public Void set_Left(Single value)`
  - `public Void set_NeedLayout(Boolean value)`
  - `public Void set_Position(Int32 value)`
  - `public Void set_Right(Single value)`
  - `public Void set_Start(Int32 value)`
  - `public Void set_Top(Single value)`
  - `public Single Bottom { get; set; }`
  - `public Int32 Count { get; set; }`
  - `public Int32 End { get; set; }`
  - `public Single Left { get; set; }`
  - `public Boolean NeedLayout { get; set; }`
  - `public Int32 Position { get; set; }`
  - `public Single Right { get; set; }`
  - `public Int32 Start { get; set; }`
  - `public Single Top { get; set; }`

### class `GroupItemSource`

- Base: `System.Object`
- Interfaces: `System.Collections.ICollection`, `System.Collections.IEnumerable`, `System.Collections.IList`, `System.Collections.Specialized.INotifyCollectionChanged`
- Members:
  - `public .ctor(IEnumerable source)`
  - `public event NotifyCollectionChangedEventHandler CollectionChanged`
  - `public Int32 Add(Object value)`
  - `public Void add_CollectionChanged(NotifyCollectionChangedEventHandler value)`
  - `private Void AddGroupItem(Object groupItem)`
  - `private Int32 AdjustIndex(Int32 index, Int32 count)`
  - `private IList<IList> BuildGroupSource(IEnumerable groupsSource)`
  - `public Void Clear()`
  - `public Boolean Contains(Object value)`
  - `public Void CopyTo(Array array, Int32 index)`
  - `private static IList CreateSource(IEnumerable source)`
  - `public Int32 get_Count()`
  - `public Int32 get_GroupCount()`
  - `private Int32 get_GroupHeaderFooterCount()`
  - `public Boolean get_HasGroupFooter()`
  - `public Boolean get_HasGroupHeader()`
  - `public Boolean get_IsFixedSize()`
  - `public Boolean get_IsReadOnly()`
  - `public Boolean get_IsSynchronized()`
  - `public Object get_Item(Int32 index)`
  - `public Object get_SyncRoot()`
  - `public Int32 GetAbsoluteIndex(Int32 group, Int32 ingroup)`
  - `public Int32 GetChildrenCount(Int32 group)`
  - `private Int32 GetCount()`
  - `public IEnumerator GetEnumerator()`
  - `public ValueTuple<Int32, Int32> GetGroupAndIndex(Int32 index)`
  - `public Object GetGroupItem(Int32 group)`
  - `private Object GetItem(Int32 index)`
  - `private Void HandleGroupCollectionAdded(Int32 startIndex, Object groupItem)`
  - `private Void HandleGroupCollectionRemoved(Object groupItem)`
  - `public Int32 IndexOf(Object value)`
  - `public Void Insert(Int32 index, Object value)`
  - `private Void InsertGroupItem(Int32 index, Object groupItem)`
  - `private Void OnCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `private Void OnGroupCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `public Void Remove(Object value)`
  - `public Void remove_CollectionChanged(NotifyCollectionChangedEventHandler value)`
  - `public Void RemoveAt(Int32 index)`
  - `private Void RemoveGroupItem(Object groupItem)`
  - `public Void set_HasGroupFooter(Boolean value)`
  - `public Void set_HasGroupHeader(Boolean value)`
  - `public Void set_Item(Int32 index, Object value)`
  - `public Int32 Count { get; }`
  - `public Int32 GroupCount { get; }`
  - `private Int32 GroupHeaderFooterCount { get; }`
  - `public Boolean HasGroupFooter { get; set; }`
  - `public Boolean HasGroupHeader { get; set; }`
  - `public Boolean IsFixedSize { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public Boolean IsSynchronized { get; }`
  - `public Object Item { get; set; }`
  - `public Object SyncRoot { get; }`

### class `GroupItemTemplateAdapter`

- Base: `Tizen.UI.Components.Recycler.ItemTemplateAdapter`
- Interfaces: `Tizen.UI.Components.Recycler.IGroupedAdapter`
- Members:
  - `public .ctor()`
  - `public Void BindBodyHolder(ViewHolder holder, Int32 group)`
  - `public ViewHolder CreateBody(Int32 type)`
  - `public ViewTemplate get_GroupBodyTemplate()`
  - `public Int32 get_GroupCount()`
  - `public ViewTemplate get_GroupFooterTemplate()`
  - `public ViewTemplate get_GroupHeaderTemplate()`
  - `public Boolean get_HasGroupBody()`
  - `private ViewTemplate get_InvisibleDividerTemplate()`
  - `public Boolean get_IsStickyHeader()`
  - `public Int32 GetBodyType(Int32 group)`
  - `public Int32 GetChildrenCount(Int32 group)`
  - `public ValueTuple<Int32, Int32> GetGroupAndIndex(Int32 position)`
  - `public Object GetGroupItem(Int32 group)`
  - `public Int32 GetItemViewType(Int32 position)`
  - `public Boolean IsSticky(Int32 position)`
  - `public Void set_GroupBodyTemplate(ViewTemplate value)`
  - `public Void set_GroupFooterTemplate(ViewTemplate value)`
  - `public Void set_GroupHeaderTemplate(ViewTemplate value)`
  - `public Void set_IsStickyHeader(Boolean value)`
  - `public Void SetItemsSource(IEnumerable items)`
  - `public ViewTemplate GroupBodyTemplate { get; set; }`
  - `public Int32 GroupCount { get; }`
  - `public ViewTemplate GroupFooterTemplate { get; set; }`
  - `public ViewTemplate GroupHeaderTemplate { get; set; }`
  - `public Boolean HasGroupBody { get; }`
  - `private ViewTemplate InvisibleDividerTemplate { get; }`
  - `public Boolean IsStickyHeader { get; set; }`

### class `HolderExtensions`

- Base: `System.Object`
- Members:
  - `public static GroupInfo GroupInfo(View view)`
  - `public static Void Holder(View view, ViewHolder holder)`
  - `public static ViewHolder Holder(View view)`
  - `public static HolderInfo HolderInfo(View view)`
  - `public static Int32 HolderPosition(View view)`

### class `HolderInfo`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public ViewHolder get_Holder()`
  - `public Void set_Holder(ViewHolder value)`
  - `public ViewHolder Holder { get; set; }`

### interface `IChildSeizable`

- Members:
  - `public ViewHolder GetSeized(Int32 position)`
  - `public Int32 GetSeizedCount()`
  - `public Boolean IsSeized(Int32 position)`
  - `public Void ReleaseChild(ViewHolder viewHolder)`
  - `public Void SeizeChild(ViewHolder viewHolder)`

### interface `IDividerProvider`

- Members:
  - `public Boolean IsDividerPosition(Int32 position)`

### interface `IGridRelativeSizeHelper`

- Interfaces: `Tizen.UI.Components.Recycler.IItemDecoration`
- Members:
  - `public Single GetAspectRatio(View view, Int32 position, RecyclerView recyclerView)`

### interface `IGridSpanHelper`

- Interfaces: `Tizen.UI.Components.Recycler.IItemDecoration`
- Members:
  - `public Int32 GetSpanIndex(Int32 position, RecyclerView recyclerView)`
  - `public UInt32 GetSpanSize(Int32 position, RecyclerView recyclerView)`

### interface `IGroupedAdapter`

- Members:
  - `public Void BindBodyHolder(ViewHolder holder, Int32 group)`
  - `public ViewHolder CreateBody(Int32 type)`
  - `public Int32 get_GroupCount()`
  - `public Boolean get_HasGroupBody()`
  - `public Int32 GetBodyType(Int32 group)`
  - `public Int32 GetChildrenCount(Int32 group)`
  - `public ValueTuple<Int32, Int32> GetGroupAndIndex(Int32 position)`
  - `public Object GetGroupItem(Int32 group)`
  - `public Int32 GroupCount { get; }`
  - `public Boolean HasGroupBody { get; }`

### interface `IGroupedItem`

- Members:
  - `public Boolean get_IsFirstOfGroup()`
  - `public Boolean get_IsLastOfGroup()`
  - `public Void set_IsFirstOfGroup(Boolean value)`
  - `public Void set_IsLastOfGroup(Boolean value)`
  - `public Void UpdateByPosition(Int32 position)`
  - `public Boolean IsFirstOfGroup { get; set; }`
  - `public Boolean IsLastOfGroup { get; set; }`

### interface `IItemAnimator`

- Members:
  - `public event EventHandler<AnimationFinishedEventArgs> AnimationFinished`
  - `public Void add_AnimationFinished(EventHandler<AnimationFinishedEventArgs> value)`
  - `public Boolean AnimateAdd(ViewHolder holder)`
  - `public Boolean AnimateChange(ViewHolder oldHolder, ViewHolder newHolder, Single fromX, Single fromY, Single toX, Single toY)`
  - `public Boolean AnimateMove(ViewHolder holder, Single fromX, Single fromY, Single toX, Single toY)`
  - `public Boolean AnimateRemove(ViewHolder holder)`
  - `public Void EndAnimation(ViewHolder item)`
  - `public Void EndAnimations()`
  - `public RecyclerView get_RecyclerView()`
  - `public Boolean IsRunning(ViewHolder item)`
  - `public Void remove_AnimationFinished(EventHandler<AnimationFinishedEventArgs> value)`
  - `public Void RunPendingAnimations()`
  - `public Void set_RecyclerView(RecyclerView value)`
  - `public RecyclerView RecyclerView { get; set; }`

### interface `IItemDecoration`

- Members:
  - `public Thickness GetItemOffsets(View view, Int32 position, RecyclerView recyclerView)`

### interface `ILoopedAdapter`

- Members:
  - `public Int32 get_BaseItemCount()`
  - `public Boolean get_IsLooping()`
  - `public Void set_IsLooping(Boolean value)`
  - `public Int32 BaseItemCount { get; }`
  - `public Boolean IsLooping { get; set; }`

### interface `ISelectableModel`

- Interfaces: `System.ComponentModel.INotifyPropertyChanged`
- Members:
  - `public Boolean get_IsSelectable()`
  - `public Boolean get_IsSelected()`
  - `public Void set_IsSelectable(Boolean value)`
  - `public Void set_IsSelected(Boolean value)`
  - `public Boolean IsSelectable { get; set; }`
  - `public Boolean IsSelected { get; set; }`

### interface `ISelectionModelGroup`

- Interfaces: `System.Collections.ICollection`, `System.Collections.IEnumerable`, `System.Collections.IList`
- Members:
  - `public Int32 get_SelectedCount()`
  - `public Int32 SelectedCount { get; }`

### interface `ISelectionModelGroup`1`

- Interfaces: `System.Collections.Generic.ICollection`1<T>`, `System.Collections.Generic.IEnumerable`1<T>`, `System.Collections.Generic.IList`1<T>`, `System.Collections.ICollection`, `System.Collections.IEnumerable`, `System.Collections.IList`, `Tizen.UI.Components.Recycler.ISelectionModelGroup`
- Members:
  - `public IReadOnlyCollection<T> get_SelectedChildren()`
  - `public IReadOnlyCollection<T> SelectedChildren { get; }`

### class `ItemTemplateAdapter`

- Base: `Tizen.UI.Components.Recycler.Adapter`
- Interfaces: `Tizen.UI.Components.Recycler.IDividerProvider`
- Members:
  - `public .ctor()`
  - `protected Void AddToTemplateCache(Int32 viewType, ViewTemplate template)`
  - `protected Int32 AdjustDividerPosition(Int32 position)`
  - `public ViewTemplate get_DividerTemplate()`
  - `public Boolean get_HasDivider()`
  - `public Int32 get_ItemCount()`
  - `public IEnumerable get_ItemsSource()`
  - `public ViewTemplate get_ItemTemplate()`
  - `protected Object GetItem(Int32 position)`
  - `public Object GetItemOnPosition(Int32 position)`
  - `public Int32 GetItemViewType(Int32 position)`
  - `public Int32 GetSourceIndexFromPosition(Int32 position)`
  - `protected ViewTemplate GetViewTemplate(Int32 viewType)`
  - `public Boolean IsDividerPosition(Int32 position)`
  - `public Void OnBindViewHolder(ViewHolder holder, Int32 position)`
  - `private Void OnCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `public ViewHolder OnCreateViewHolder(Int32 viewType)`
  - `public Void set_DividerTemplate(ViewTemplate value)`
  - `public Void set_HasDivider(Boolean value)`
  - `public Void set_ItemsSource(IEnumerable value)`
  - `public Void set_ItemTemplate(ViewTemplate value)`
  - `public Void SetItemsSource(IEnumerable items)`
  - `public ViewTemplate DividerTemplate { get; set; }`
  - `public Boolean HasDivider { get; set; }`
  - `public Int32 ItemCount { get; }`
  - `public IEnumerable ItemsSource { get; set; }`
  - `public ViewTemplate ItemTemplate { get; set; }`

### class `ItemTouchHelper`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Components.Recycler.IItemDecoration`
- Members:
  - `public .ctor(ItemTouchHelperCallback callback)`
  - `public Void AttachToRecyclerView(RecyclerView view)`
  - `public Void DetachToRecyclerView()`
  - `private Void DragDelta(ViewHolder viewHolder)`
  - `private View FindChildView(Point pos)`
  - `private List<ViewHolder> FindSwapTargets(ViewHolder viewHolder)`
  - `private LayoutManager get_LayoutManager()`
  - `public Thickness GetItemOffsets(View view, Int32 position, RecyclerView recyclerView)`
  - `private Void MoveIfNecessary(ViewHolder viewHolder)`
  - `private Void OnInterceptTouchEvent(Object o, TouchEventArgs e)`
  - `private Void OnLongPressed(Object sender, LongPressGestureDetectedEventArgs e)`
  - `private Void OnTouchEvent(Object o, TouchEventArgs e)`
  - `private Boolean ScrollIfNecessary()`
  - `private Void SelectHolder(ViewHolder selected, TouchActionState actionState)`
  - `public Void StartDrag(ViewHolder viewHolder)`
  - `private Void UpdateDxDy(Point pos, Int32 directionPolicy)`
  - `private LayoutManager LayoutManager { get; }`

### class `ItemTouchHelperCallback`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `protected .ctor()`
  - `public Boolean CanDropOver(RecyclerView recyclerView, ViewHolder current, ViewHolder target)`
  - `public ViewHolder ChooseDropTarget(ViewHolder selected, List<ViewHolder> dropTargets, Point currentPosition)`
  - `public Single get_BoundingBoxMargin()`
  - `public Boolean get_IsLongpressDragEnabled()`
  - `public Int32 GetMovementPolicy(RecyclerView recyclerView, ViewHolder viewholder)`
  - `public Single GetSMoveThreshold(ViewHolder viewholder)`
  - `public Boolean HasDragPolicy(RecyclerView recyclerView, ViewHolder viewholder)`
  - `public Single InterpolateOutOfBoundsScroll(RecyclerView recyclerView, Single viewSize, Single viewSizeOutOfBounds, Single totalSize, Int64 msSinceStartScroll)`
  - `public static Int32 MakePolicy(TouchActionState actionState, TouchDirection directions)`
  - `public Boolean OnMove(RecyclerView recyclerView, ViewHolder viewholder, ViewHolder target)`
  - `public Void OnMoved(RecyclerView recyclerView, ViewHolder viewholder, Int32 fromPos, ViewHolder target, Int32 toPos, Point currentPosition)`
  - `public Void OnSelectedChanged(ViewHolder viewholder, TouchActionState actionState)`
  - `protected Void set_BoundingBoxMargin(Single value)`
  - `protected Void set_IsLongpressDragEnabled(Boolean value)`
  - `public Single BoundingBoxMargin { get; set; }`
  - `public Boolean IsLongpressDragEnabled { get; set; }`

### class `ItemTouchListener`

- Base: `System.Object`
- Members:
  - `public .ctor(EventHandler<TouchEventArgs> onInterceptTouch, EventHandler<TouchEventArgs> onTouch)`
  - `public Void OnInterceptTouchEvent(Object touched, TouchEventArgs eventArgs)`
  - `public Void OnTouchEvent(Object touched, TouchEventArgs eventArgs)`

### class `LayoutManager`

- Base: `System.Object`
- Members:
  - `protected .ctor()`
  - `public Int32 ComputeHorizontalScrollExtent(Int32 first, Int32 last)`
  - `public Int32 ComputeHorizontalScrollOffset(Int32 first, Int32 last)`
  - `public Int32 ComputeHorizontalScrollRange(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollExtent(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollOffset(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollRange(Int32 first, Int32 last)`
  - `public Boolean get_CanScrollHorizontally()`
  - `public Boolean get_CanScrollVertically()`
  - `public Rect get_LayoutViewPort()`
  - `public RecyclerView get_RecyclerView()`
  - `public Void Initialize()`
  - `public Void LayoutPosition(Int32 position)`
  - `public Size MeasureUpdate(View itemView)`
  - `public Void OnChildrenChanged(NotifyCollectionChangedEventArgs e)`
  - `public Void OnLayoutChildren()`
  - `public Void RecycleOutOfSightView()`
  - `public Int32 ScrollHorizontallyBy(Int32 dx)`
  - `public Int32 ScrollVerticallyBy(Int32 dy)`
  - `public Void set_LayoutViewPort(Rect value)`
  - `public Void set_RecyclerView(RecyclerView value)`
  - `public Boolean CanScrollHorizontally { get; }`
  - `public Boolean CanScrollVertically { get; }`
  - `public Rect LayoutViewPort { get; set; }`
  - `public RecyclerView RecyclerView { get; set; }`

### class `LinearLayoutManager`

- Base: `Tizen.UI.Components.Recycler.LayoutManager`
- Interfaces: `Tizen.UI.Components.Recycler.IChildSeizable`
- Members:
  - `public .ctor()`
  - `private ValueTuple<Int32, Int32> AdjustArea(Int32 first, Int32 last)`
  - `protected Rect ArrangeChild(View view, Size measured)`
  - `private Void ClearSnapShot()`
  - `public Int32 ComputeHorizontalScrollExtent(Int32 first, Int32 last)`
  - `public Int32 ComputeHorizontalScrollOffset(Int32 first, Int32 last)`
  - `public Int32 ComputeHorizontalScrollRange(Int32 first, Int32 last)`
  - `private Int32 ComputeScrollExtent(Int32 first, Int32 last)`
  - `private Int32 ComputeScrollOffset(Int32 first, Int32 last)`
  - `private Int32 ComputeScrollRange(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollExtent(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollOffset(Int32 first, Int32 last)`
  - `public Int32 ComputeVerticalScrollRange(Int32 first, Int32 last)`
  - `public Int32 Fill(Int32 requested, Int32 position)`
  - `private IGroupedAdapter get__groupAdapter()`
  - `private Boolean get__isGrouped()`
  - `private Boolean get__isLooping()`
  - `private ItemTemplateAdapter get__itemAdapter()`
  - `private ILoopedAdapter get__loopedAdapter()`
  - `public Boolean get_CanScrollHorizontally()`
  - `public Boolean get_CanScrollVertically()`
  - `private Single get_Height()`
  - `public Boolean get_IsHorizontal()`
  - `public IItemAnimator get_ItemAnimator()`
  - `public Rect get_LayoutViewPort()`
  - `private Single get_Width()`
  - `public ViewHolder GetSeized(Int32 position)`
  - `public Int32 GetSeizedCount()`
  - `public Void Initialize()`
  - `private Boolean IsDividerPosition(Int32 position)`
  - `public Boolean IsSeized(Int32 position)`
  - `private Void ItemRealize(View item)`
  - `private Void ItemUnrealize(View item)`
  - `protected Single LayoutChunk(View view)`
  - `public Void LayoutPosition(Int32 position)`
  - `public Size MeasureUpdate(View itemView)`
  - `private View Next()`
  - `public Void OnChildrenChanged(NotifyCollectionChangedEventArgs e)`
  - `private Void OnDirtyChildrenUpdate(Object sender, EventArgs e)`
  - `private Void OnItemMeasureInvalidated(Object sender, EventArgs e)`
  - `public Void OnLayoutChildren()`
  - `private Void OnLayoutChildren(Int32 start)`
  - `protected Void RecycleCreatedView()`
  - `public Void RecycleOutOfSightView()`
  - `public Void ReleaseChild(ViewHolder viewHolder)`
  - `protected Void RequestDirtyChildrenUpdate()`
  - `private Void RunSnapChangedChildren()`
  - `public Int32 ScrollHorizontallyBy(Int32 dx)`
  - `public Int32 ScrollVerticallyBy(Int32 dy)`
  - `public Void SeizeChild(ViewHolder viewHolder)`
  - `public Void set_IsHorizontal(Boolean value)`
  - `public Void set_ItemAnimator(IItemAnimator value)`
  - `public Void set_LayoutViewPort(Rect value)`
  - `private Void SnapChildrenBounds()`
  - `protected Void UpdateDirtyChlidren()`
  - `private Void UpdateGroupBody()`
  - `private Void UpdateGroupBodyInfo(Int32 position, Rect frame)`
  - `private Void UpdateScrollSize(Single measured)`
  - `private Void UpdateViewportSize(Single measured)`
  - `private IGroupedAdapter _groupAdapter { get; }`
  - `private Boolean _isGrouped { get; }`
  - `private Boolean _isLooping { get; }`
  - `private ItemTemplateAdapter _itemAdapter { get; }`
  - `private ILoopedAdapter _loopedAdapter { get; }`
  - `public Boolean CanScrollHorizontally { get; }`
  - `public Boolean CanScrollVertically { get; }`
  - `private Single Height { get; }`
  - `public Boolean IsHorizontal { get; set; }`
  - `public IItemAnimator ItemAnimator { get; set; }`
  - `public Rect LayoutViewPort { get; set; }`
  - `private Single Width { get; }`

### class `LinearSwapCallback`

- Base: `Tizen.UI.Components.Recycler.ItemTouchHelperCallback`
- Members:
  - `protected .ctor()`
  - `public Int32 GetMovementPolicy(RecyclerView recyclerView, ViewHolder viewholder)`

### class `LoopedGroupItemAdapter`

- Base: `Tizen.UI.Components.Recycler.GroupItemTemplateAdapter`
- Interfaces: `Tizen.UI.Components.Recycler.ILoopedAdapter`
- Members:
  - `public .ctor()`
  - `public Int32 get_BaseItemCount()`
  - `public Boolean get_IsLooping()`
  - `public Int32 get_ItemCount()`
  - `public IList get_ItemsSource()`
  - `public ValueTuple<Int32, Int32> GetGroupAndIndex(Int32 position)`
  - `protected Object GetItem(Int32 position)`
  - `public Int32 GetItemViewType(Int32 position)`
  - `public Boolean IsSticky(Int32 position)`
  - `public Void set_IsLooping(Boolean value)`
  - `public Int32 BaseItemCount { get; }`
  - `public Boolean IsLooping { get; set; }`
  - `public Int32 ItemCount { get; }`
  - `public IList ItemsSource { get; }`

### class `LoopedItemAdapter`

- Base: `Tizen.UI.Components.Recycler.ItemTemplateAdapter`
- Interfaces: `Tizen.UI.Components.Recycler.ILoopedAdapter`
- Members:
  - `public .ctor()`
  - `public Int32 get_BaseItemCount()`
  - `public Boolean get_IsLooping()`
  - `public Int32 get_ItemCount()`
  - `public IList get_ItemsSource()`
  - `protected Object GetItem(Int32 position)`
  - `public Void set_IsLooping(Boolean value)`
  - `public Int32 BaseItemCount { get; }`
  - `public Boolean IsLooping { get; set; }`
  - `public Int32 ItemCount { get; }`
  - `public IList ItemsSource { get; }`

### class `RecycledViewPool`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Void Clear()`
  - `public Int32 get_PoolSize()`
  - `public ViewHolder GetRecycledView(Int32 viewType)`
  - `public ViewHolder GetRecycledView(Int32 viewType, Object data)`
  - `public Boolean PutRecycledView(ViewHolder holder)`
  - `public Void set_PoolSize(Int32 value)`
  - `public Int32 PoolSize { get; set; }`

### class `RecyclerView`

- Base: `Tizen.UI.ViewGroup`
- Interfaces: `Tizen.UI.IDescendantFocusObserver`, `Tizen.UI.IScrollable`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public event EventHandler<DragEventArgs> DragFinished`
  - `public event EventHandler<DragEventArgs> Dragging`
  - `public event EventHandler<DragEventArgs> DragStarted`
  - `public event EventHandler<ScrollEventArgs> ScrollCanceled`
  - `public event EventHandler<ScrollEventArgs> ScrollFinished`
  - `public event EventHandler<ScrollEventArgs> Scrolling`
  - `public event EventHandler<ScrollEventArgs> ScrollStarted`
  - `private Void <get_ItemDecorations>b__103_0(Object _, NotifyCollectionChangedEventArgs _)`
  - `public Void add_DragFinished(EventHandler<DragEventArgs> value)`
  - `public Void add_Dragging(EventHandler<DragEventArgs> value)`
  - `public Void add_DragStarted(EventHandler<DragEventArgs> value)`
  - `public Void add_ScrollCanceled(EventHandler<ScrollEventArgs> value)`
  - `public Void add_ScrollFinished(EventHandler<ScrollEventArgs> value)`
  - `public Void add_Scrolling(EventHandler<ScrollEventArgs> value)`
  - `public Void add_ScrollStarted(EventHandler<ScrollEventArgs> value)`
  - `internal Void AddItemTouchListener(ItemTouchListener listener)`
  - `public Void ClearRecycledViewPool()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Int32 FirstCompletelyVisibleItemPosition()`
  - `public Int32 FirstRealizedPosition()`
  - `public Int32 FirstVisibleItemPosition()`
  - `private IGroupedAdapter get__groupAdapter()`
  - `private Boolean get__isGrouped()`
  - `private ILoopedAdapter get__loopAdapter()`
  - `public Adapter get_Adapter()`
  - `public Boolean get_HasSticky()`
  - `public IEdgeEffect get_HorizontalEdgeEffect()`
  - `public RecycleScroller get_InnerScroller()`
  - `public StickyArea get_InnerStickArea()`
  - `public Boolean get_IsScrolledToEnd()`
  - `public Boolean get_IsScrolledToStart()`
  - `public Boolean get_IsScrolling()`
  - `public IList<IItemDecoration> get_ItemDecorations()`
  - `public Rect get_LastViewPort()`
  - `public LayoutManager get_LayoutManager()`
  - `public OverScrollMode get_OverScrollMode()`
  - `public Single get_PrefetchBaseSize()`
  - `public Int32 get_RecycledPoolSize()`
  - `public ScrollDirection get_ScrollDirection()`
  - `public Point get_ScrollPosition()`
  - `public Boolean get_StickyCandidateVisible()`
  - `public IEdgeEffect get_VerticalEdgeEffect()`
  - `public Rect get_ViewPort()`
  - `public ViewHolder GetBodyForGroup(Int32 group)`
  - `protected ValueTuple<Int32, AlphaFunction> GetDurationAndAlpha(Point movement)`
  - `private Rect GetItemBounds(Int32 position)`
  - `private View GetItemViewFromFocusedView(View view)`
  - `public Int32 GetItemViewType(Int32 position)`
  - `public View GetRealized(Int32 position)`
  - `public ViewHolder GetViewHolderForPosition(Int32 position)`
  - `public Int32 LastCompletelyVisibleItemPosition()`
  - `public Int32 LastRealizedPosition()`
  - `public Int32 LastVisibleItemPosition()`
  - `public Size Measure(Single availableWidth, Single availableHeight)`
  - `private Void OnCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `protected Void OnInterceptTouchEvent(Object s, TouchEventArgs e)`
  - `private Void OnKeyEvent(Object sender, KeyEventArgs e)`
  - `private Void OnPan(Object sender, PanGestureDetectedEventArgs e)`
  - `private Void OnRelayout(Object sender, EventArgs e)`
  - `private Void OnScroll(Object sender, ScrollEventArgs e)`
  - `private Void OnScrollCanceled(Object sender, EventArgs e)`
  - `private Void OnScrollFinished(Object sender, EventArgs e)`
  - `private Void OnScrollStarted(Object sender, EventArgs e)`
  - `protected Void OnTouchEvent(Object s, TouchEventArgs e)`
  - `protected Boolean OnWheel(WheelEvent wheel)`
  - `private Void OnWheelEvent(Object sender, WheelEventArgs e)`
  - `public Void RecycleViewHolder(ViewHolder viewholder)`
  - `public Void remove_DragFinished(EventHandler<DragEventArgs> value)`
  - `public Void remove_Dragging(EventHandler<DragEventArgs> value)`
  - `public Void remove_DragStarted(EventHandler<DragEventArgs> value)`
  - `public Void remove_ScrollCanceled(EventHandler<ScrollEventArgs> value)`
  - `public Void remove_ScrollFinished(EventHandler<ScrollEventArgs> value)`
  - `public Void remove_Scrolling(EventHandler<ScrollEventArgs> value)`
  - `public Void remove_ScrollStarted(EventHandler<ScrollEventArgs> value)`
  - `internal Void RemoveItemTouchListener(ItemTouchListener listener)`
  - `public Task ScrollBy(Single x, Single y, Boolean animation)`
  - `private ValueTuple<Single, Single> ScrollBy(Single dx, Single dy)`
  - `public Task ScrollTo(Int32 position, ScrollToPosition scrollToPosition, Boolean animation)`
  - `public Void set_Adapter(Adapter value)`
  - `public Void set_HasSticky(Boolean value)`
  - `public Void set_HorizontalEdgeEffect(IEdgeEffect value)`
  - `protected Void set_IsScrolling(Boolean value)`
  - `public Void set_LastViewPort(Rect value)`
  - `public Void set_LayoutManager(LayoutManager value)`
  - `public Void set_OverScrollMode(OverScrollMode value)`
  - `public Void set_PrefetchBaseSize(Single value)`
  - `public Void set_RecycledPoolSize(Int32 value)`
  - `public Void set_StickyCandidateVisible(Boolean value)`
  - `public Void set_VerticalEdgeEffect(IEdgeEffect value)`
  - `private Task SmoothScrollBy(Single dx, Single dy)`
  - `private Void StopScroll()`
  - `private Void StopScrollAndRecycle()`
  - `private Void Tizen.UI.IDescendantFocusObserver.OnDescendantFocused(View descendant)`
  - `private Boolean Tizen.UI.IScrollable.get_CanScrollHorizontally()`
  - `private Boolean Tizen.UI.IScrollable.get_CanScrollVertically()`
  - `private Task Tizen.UI.IScrollable.ScrollTo(Single x, Single y, Boolean animation)`
  - `protected Point VelocityToMovement(Point velocity)`
  - `private IGroupedAdapter _groupAdapter { get; }`
  - `private Boolean _isGrouped { get; }`
  - `private ILoopedAdapter _loopAdapter { get; }`
  - `public Adapter Adapter { get; set; }`
  - `public Boolean HasSticky { get; set; }`
  - `public IEdgeEffect HorizontalEdgeEffect { get; set; }`
  - `public RecycleScroller InnerScroller { get; }`
  - `public StickyArea InnerStickArea { get; }`
  - `public Boolean IsScrolledToEnd { get; }`
  - `public Boolean IsScrolledToStart { get; }`
  - `public Boolean IsScrolling { get; set; }`
  - `public IList<IItemDecoration> ItemDecorations { get; }`
  - `public Rect LastViewPort { get; set; }`
  - `public LayoutManager LayoutManager { get; set; }`
  - `public OverScrollMode OverScrollMode { get; set; }`
  - `public Single PrefetchBaseSize { get; set; }`
  - `public Int32 RecycledPoolSize { get; set; }`
  - `public ScrollDirection ScrollDirection { get; }`
  - `public Point ScrollPosition { get; }`
  - `public Boolean StickyCandidateVisible { get; set; }`
  - `private Boolean Tizen.UI.IScrollable.CanScrollHorizontally { get; }`
  - `private Boolean Tizen.UI.IScrollable.CanScrollVertically { get; }`
  - `public IEdgeEffect VerticalEdgeEffect { get; set; }`
  - `public Rect ViewPort { get; }`

### class `RecycleScroller`

- Base: `Tizen.UI.ViewGroup`
- Members:
  - `private static .cctor()`
  - `public .ctor(RecyclerView recyclerView)`
  - `public event EventHandler ScrollAnimationCanceled`
  - `public event EventHandler ScrollAnimationFinished`
  - `public event EventHandler ScrollAnimationStarted`
  - `public event EventHandler<ScrollEventArgs> Scrolling`
  - `public Void add_ScrollAnimationCanceled(EventHandler value)`
  - `public Void add_ScrollAnimationFinished(EventHandler value)`
  - `public Void add_ScrollAnimationStarted(EventHandler value)`
  - `public Void add_Scrolling(EventHandler<ScrollEventArgs> value)`
  - `public Void AddBody(View child)`
  - `public Void AdjustChildPosition(Single dx, Single dy, Func<View, Boolean> condition)`
  - `public Void AdjustScrollPosition(Single dx, Single dy)`
  - `protected Void Dispose(Boolean disposing)`
  - `public Rect get_CurrentViewPort()`
  - `public Boolean get_IsScrollAnimationStarted()`
  - `public IList<View> get_RecycleBody()`
  - `private RecyclerView get_RecyclerView()`
  - `public Single get_ScrollX()`
  - `public Single get_ScrollY()`
  - `public Rect get_ViewPort()`
  - `public Void Initialize()`
  - `public Void MoveBy(Single x, Single y)`
  - `private Void OnAnimationFinished(Object sender, EventArgs e)`
  - `private Void OnScroll()`
  - `public Void remove_ScrollAnimationCanceled(EventHandler value)`
  - `public Void remove_ScrollAnimationFinished(EventHandler value)`
  - `public Void remove_ScrollAnimationStarted(EventHandler value)`
  - `public Void remove_Scrolling(EventHandler<ScrollEventArgs> value)`
  - `public Void RemoveBody(View child)`
  - `public Task ScrollBy(Single x, Single y, Int32 duration, AlphaFunction alpha)`
  - `public Void ScrollStop()`
  - `public Void set_IsScrollAnimationStarted(Boolean value)`
  - `public Rect CurrentViewPort { get; }`
  - `public Boolean IsScrollAnimationStarted { get; set; }`
  - `public IList<View> RecycleBody { get; }`
  - `private RecyclerView RecyclerView { get; }`
  - `public Single ScrollX { get; }`
  - `public Single ScrollY { get; }`
  - `public Rect ViewPort { get; }`

### class `SelectableModel`

- Base: `Tizen.UI.Components.Recycler.ViewModel`
- Interfaces: `System.ComponentModel.INotifyPropertyChanged`, `Tizen.UI.Components.Recycler.ISelectableModel`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsSelectable()`
  - `public Boolean get_IsSelected()`
  - `public Void set_IsSelectable(Boolean value)`
  - `public Void set_IsSelected(Boolean value)`
  - `public Boolean IsSelectable { get; set; }`
  - `public Boolean IsSelected { get; set; }`

### class `SelectionModelGroup`1`

- Base: `System.Collections.ObjectModel.ObservableCollection`1<T>`
- Interfaces: `System.Collections.Generic.ICollection`1<T>`, `System.Collections.Generic.IEnumerable`1<T>`, `System.Collections.Generic.IList`1<T>`, `System.Collections.ICollection`, `System.Collections.IEnumerable`, `System.Collections.IList`, `System.ComponentModel.INotifyPropertyChanged`, `Tizen.UI.Components.Recycler.ISelectableModel`, `Tizen.UI.Components.Recycler.ISelectionModelGroup`, `Tizen.UI.Components.Recycler.ISelectionModelGroup`1<T>`
- Members:
  - `protected .ctor()`
  - `private Void CheckSelectedAll()`
  - `protected TType Get(String name)`
  - `public Boolean get_IsSelectable()`
  - `public Boolean get_IsSelected()`
  - `public IReadOnlyCollection<T> get_SelectedChildren()`
  - `public Int32 get_SelectedCount()`
  - `private Void OnChildPropertyChanged(Object sender, PropertyChangedEventArgs e)`
  - `protected Void OnCollectionChanged(NotifyCollectionChangedEventArgs e)`
  - `protected Void OnPropertyChanged(String propertyName)`
  - `protected Void Set(TType value, String name)`
  - `public Void set_IsSelectable(Boolean value)`
  - `public Void set_IsSelected(Boolean value)`
  - `private Void UpdateSelection(T selected)`
  - `public Boolean IsSelectable { get; set; }`
  - `public Boolean IsSelected { get; set; }`
  - `public IReadOnlyCollection<T> SelectedChildren { get; }`
  - `public Int32 SelectedCount { get; }`

### class `StickyArea`

- Base: `Tizen.UI.ViewGroup`
- Members:
  - `public .ctor(RecyclerView recyclerView)`
  - `public Void Add(ViewHolder holder)`
  - `public ViewHolder CreatePrecedingStickyView(Int32 position)`
  - `public Boolean Has(Int32 position)`
  - `public Void Initialize()`
  - `protected Void OnChildAdded(View view)`
  - `protected Void OnChildRemoved(View view)`
  - `public Void Sync()`

### enum `TouchActionState`

- Base: `System.Enum`
- Members:

### enum `TouchDirection`

- Base: `System.Enum`
- Members:

### class `ViewHolder`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Object get_BindingContext()`
  - `public Thickness get_BoundsModifier()`
  - `public Rect get_Frame()`
  - `public Boolean get_IsDivider()`
  - `public Boolean get_IsRecycled()`
  - `public Boolean get_IsSeized()`
  - `public Single get_ItemHeight()`
  - `public UInt32 get_ItemSpanSize()`
  - `public View get_ItemView()`
  - `public Int32 get_ItemViewType()`
  - `public Single get_ItemWidth()`
  - `public Int32 get_Position()`
  - `public Void set_BindingContext(Object value)`
  - `public Void set_BoundsModifier(Thickness value)`
  - `public Void set_Frame(Rect value)`
  - `public Void set_IsDivider(Boolean value)`
  - `public Void set_IsRecycled(Boolean value)`
  - `public Void set_IsSeized(Boolean value)`
  - `public Void set_ItemSpanSize(UInt32 value)`
  - `public Void set_ItemView(View value)`
  - `public Void set_ItemViewType(Int32 value)`
  - `public Void set_Position(Int32 value)`
  - `public Object BindingContext { get; set; }`
  - `public Thickness BoundsModifier { get; set; }`
  - `public Rect Frame { get; set; }`
  - `public Boolean IsDivider { get; set; }`
  - `public Boolean IsRecycled { get; set; }`
  - `public Boolean IsSeized { get; set; }`
  - `public Single ItemHeight { get; }`
  - `public UInt32 ItemSpanSize { get; set; }`
  - `public View ItemView { get; set; }`
  - `public Int32 ItemViewType { get; set; }`
  - `public Single ItemWidth { get; }`
  - `public Int32 Position { get; set; }`

### class `ViewModel`

- Base: `System.Object`
- Interfaces: `System.ComponentModel.INotifyPropertyChanged`
- Members:
  - `protected .ctor()`
  - `public event PropertyChangedEventHandler PropertyChanged`
  - `public Void add_PropertyChanged(PropertyChangedEventHandler value)`
  - `protected T Get(String name)`
  - `protected Void OnPropertyChanged(String propertyName)`
  - `public Void remove_PropertyChanged(PropertyChangedEventHandler value)`
  - `protected Void Set(T value, String name)`

### class `ViewModelGroup`1`

- Base: `System.Collections.ObjectModel.ObservableCollection`1<T>`
- Members:
  - `protected .ctor()`
  - `protected TType Get(String name)`
  - `protected Void OnPropertyChanged(String propertyName)`
  - `protected Void Set(TType value, String name)`

