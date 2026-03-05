# API Summary: Tizen.UI.Visuals

Source: `/var/folders/hz/_qhnnrpx00nbls9m60h83lym0000gp/T/dotnet-assembly-inspector/8e169e9ec9c94f6ebbc7a9ef0f8dfdc4/lib/net8.0-tizen10.0/Tizen.UI.Visuals.dll`
Generated (UTC): 2026-03-04T12:10:11.1411060+00:00

## Namespace `Tizen.UI.Visuals`

### class `ColorVisual`

- Base: `Tizen.UI.Visuals.RoundedVisual`
- Members:
  - `public .ctor()`
  - `protected ColorVisualMap CreateVisualMap()`
  - `public Single get_BlurRadius()`
  - `public VisualCutoutPolicy get_CutoutPolicy()`
  - `protected ColorVisualMap get_VisualMap()`
  - `public Void set_BlurRadius(Single value)`
  - `public Void set_CutoutPolicy(VisualCutoutPolicy value)`
  - `public Single BlurRadius { get; set; }`
  - `public VisualCutoutPolicy CutoutPolicy { get; set; }`
  - `protected ColorVisualMap VisualMap { get; }`

### class `ImageVisual`

- Base: `Tizen.UI.Visuals.RoundedVisual`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `protected ImageVisualMap CreateVisualMap()`
  - `public String get_AlphaMaskUrl()`
  - `public Boolean get_CropToMask()`
  - `public Single get_DesiredHeight()`
  - `public Single get_DesiredWidth()`
  - `public Boolean get_FastTrackUploading()`
  - `public FittingMode get_FittingMode()`
  - `public Boolean get_ImageLoadWithViewSize()`
  - `public ImageLoadPolicy get_LoadPolicy()`
  - `public Single get_MaskContentScale()`
  - `public ImageMaskingMode get_MaskingMode()`
  - `public Boolean get_OrientationCorrection()`
  - `public Rect get_PixelArea()`
  - `public Boolean get_PreMultipliedAlpha()`
  - `public ImageReleasePolicy get_ReleasePolicy()`
  - `public String get_ResourceUrl()`
  - `public Boolean get_SynchronousLoading()`
  - `protected ImageVisualMap get_VisualMap()`
  - `public ImageWrapMode get_WrapModeU()`
  - `public ImageWrapMode get_WrapModeV()`
  - `public Void Reload()`
  - `public Void set_AlphaMaskUrl(String value)`
  - `public Void set_CropToMask(Boolean value)`
  - `public Void set_DesiredHeight(Single value)`
  - `public Void set_DesiredWidth(Single value)`
  - `public Void set_FastTrackUploading(Boolean value)`
  - `public Void set_FittingMode(FittingMode value)`
  - `public Void set_ImageLoadWithViewSize(Boolean value)`
  - `public Void set_LoadPolicy(ImageLoadPolicy value)`
  - `public Void set_MaskContentScale(Single value)`
  - `public Void set_MaskingMode(ImageMaskingMode value)`
  - `public Void set_OrientationCorrection(Boolean value)`
  - `public Void set_PixelArea(Rect value)`
  - `public Void set_PreMultipliedAlpha(Boolean value)`
  - `public Void set_ReleasePolicy(ImageReleasePolicy value)`
  - `public Void set_ResourceUrl(String value)`
  - `public Void set_SynchronousLoading(Boolean value)`
  - `public Void set_WrapModeU(ImageWrapMode value)`
  - `public Void set_WrapModeV(ImageWrapMode value)`
  - `public Void Update()`
  - `protected Void UpdateVisualMap()`
  - `public String AlphaMaskUrl { get; set; }`
  - `public Boolean CropToMask { get; set; }`
  - `public Single DesiredHeight { get; set; }`
  - `public Single DesiredWidth { get; set; }`
  - `public Boolean FastTrackUploading { get; set; }`
  - `public FittingMode FittingMode { get; set; }`
  - `public Boolean ImageLoadWithViewSize { get; set; }`
  - `public ImageLoadPolicy LoadPolicy { get; set; }`
  - `public Single MaskContentScale { get; set; }`
  - `public ImageMaskingMode MaskingMode { get; set; }`
  - `public Boolean OrientationCorrection { get; set; }`
  - `public Rect PixelArea { get; set; }`
  - `public Boolean PreMultipliedAlpha { get; set; }`
  - `public ImageReleasePolicy ReleasePolicy { get; set; }`
  - `public String ResourceUrl { get; set; }`
  - `public Boolean SynchronousLoading { get; set; }`
  - `protected ImageVisualMap VisualMap { get; }`
  - `public ImageWrapMode WrapModeU { get; set; }`
  - `public ImageWrapMode WrapModeV { get; set; }`

### class `Interop`

- Base: `System.Object`
- Members:

### class `NPatchVisual`

- Base: `Tizen.UI.Visuals.ImageVisual`
- Members:
  - `public .ctor()`
  - `protected NPatchVisualMap CreateVisualMap()`
  - `public Single get_AuxiliaryImageAlpha()`
  - `public String get_AuxiliaryImageUrl()`
  - `public Thickness get_Border()`
  - `public Boolean get_BorderOnly()`
  - `protected NPatchVisualMap get_VisualMap()`
  - `public Void set_AuxiliaryImageAlpha(Single value)`
  - `public Void set_AuxiliaryImageUrl(String value)`
  - `public Void set_Border(Thickness value)`
  - `public Void set_BorderOnly(Boolean value)`
  - `public Single AuxiliaryImageAlpha { get; set; }`
  - `public String AuxiliaryImageUrl { get; set; }`
  - `public Thickness Border { get; set; }`
  - `public Boolean BorderOnly { get; set; }`
  - `protected NPatchVisualMap VisualMap { get; }`

### class `RoundedVisual`

- Base: `Tizen.UI.Visuals.VisualObject`
- Members:
  - `protected .ctor()`
  - `public Color get_BorderlineColor()`
  - `public Single get_BorderlineOffset()`
  - `public Single get_BorderlineWidth()`
  - `public CornerRadius get_CornerRadius()`
  - `public CornerRadius get_CornerSquareness()`
  - `protected RoundedVisualMap get_VisualMap()`
  - `public Void set_BorderlineColor(Color value)`
  - `public Void set_BorderlineOffset(Single value)`
  - `public Void set_BorderlineWidth(Single value)`
  - `public Void set_CornerRadius(CornerRadius value)`
  - `public Void set_CornerSquareness(CornerRadius value)`
  - `public Color BorderlineColor { get; set; }`
  - `public Single BorderlineOffset { get; set; }`
  - `public Single BorderlineWidth { get; set; }`
  - `public CornerRadius CornerRadius { get; set; }`
  - `public CornerRadius CornerSquareness { get; set; }`
  - `protected RoundedVisualMap VisualMap { get; }`

### class `ShadowExtensions`

- Base: `System.Object`
- Members:
  - `public static ColorVisual ToVisual(Shadow shadow)`
  - `public static ColorVisual ToVisual(Shadow shadow, CornerRadius cornerRadius)`

### class `TextVisual`

- Base: `Tizen.UI.Visuals.VisualObject`
- Interfaces: `Tizen.UI.IText`
- Members:
  - `public .ctor()`
  - `protected TextVisualMap CreateVisualMap()`
  - `public Boolean get_EnableMarkup()`
  - `public String get_FontFamily()`
  - `public Single get_FontSize()`
  - `public TextAlignment get_HorizontalAlignment()`
  - `public Boolean get_MultiLine()`
  - `public String get_Text()`
  - `public Color get_TextColor()`
  - `public TextAlignment get_VerticalAlignment()`
  - `protected TextVisualMap get_VisualMap()`
  - `public Void set_EnableMarkup(Boolean value)`
  - `public Void set_FontFamily(String value)`
  - `public Void set_FontSize(Single value)`
  - `public Void set_HorizontalAlignment(TextAlignment value)`
  - `public Void set_MultiLine(Boolean value)`
  - `public Void set_Text(String value)`
  - `public Void set_TextColor(Color value)`
  - `public Void set_VerticalAlignment(TextAlignment value)`
  - `public Boolean EnableMarkup { get; set; }`
  - `public String FontFamily { get; set; }`
  - `public Single FontSize { get; set; }`
  - `public TextAlignment HorizontalAlignment { get; set; }`
  - `public Boolean MultiLine { get; set; }`
  - `public String Text { get; set; }`
  - `public Color TextColor { get; set; }`
  - `public TextAlignment VerticalAlignment { get; set; }`
  - `protected TextVisualMap VisualMap { get; }`

### enum `TransformFlags`

- Base: `System.Enum`
- Members:

### class `ViewExtensions`

- Base: `System.Object`
- Members:
  - `public static T Add(T view, VisualObject visual)`
  - `public static Void Remove(View view, VisualObject visual)`
  - `public static VisualManager Visuals(View view)`

### enum `VisualAlign`

- Base: `System.Enum`
- Members:

### class `VisualManager`

- Base: `System.Object`
- Interfaces: `System.Collections.Generic.ICollection`1<Tizen.UI.Visuals.VisualObject>`, `System.Collections.Generic.IEnumerable`1<Tizen.UI.Visuals.VisualObject>`, `System.Collections.Generic.IList`1<Tizen.UI.Visuals.VisualObject>`, `System.Collections.IEnumerable`
- Members:
  - `public .ctor(View owner)`
  - `public Void Add(VisualObject item)`
  - `public Void Clear()`
  - `public Boolean Contains(VisualObject item)`
  - `public Void CopyTo(VisualObject[] array, Int32 arrayIndex)`
  - `public IList<VisualObject> get_BackgroundLayer()`
  - `public IList<VisualObject> get_ContentLayer()`
  - `public Int32 get_Count()`
  - `public IList<VisualObject> get_DecorationLayer()`
  - `public IList<VisualObject> get_ForegroundEffectLayer()`
  - `public Boolean get_IsReadOnly()`
  - `public VisualObject get_Item(Int32 index)`
  - `public View get_Owner()`
  - `public IList<VisualObject> get_ShadowLayer()`
  - `public IEnumerator<VisualObject> GetEnumerator()`
  - `public Int32 IndexOf(VisualObject item)`
  - `public Void Insert(Int32 index, VisualObject item)`
  - `private Void OnOwnerDisposing(Object sender, EventArgs e)`
  - `public Boolean Remove(VisualObject item)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_Item(Int32 index, VisualObject value)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `public IList<VisualObject> BackgroundLayer { get; }`
  - `public IList<VisualObject> ContentLayer { get; }`
  - `public Int32 Count { get; }`
  - `public IList<VisualObject> DecorationLayer { get; }`
  - `public IList<VisualObject> ForegroundEffectLayer { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public VisualObject Item { get; set; }`
  - `public View Owner { get; }`
  - `public IList<VisualObject> ShadowLayer { get; }`

### class `VisualObject`

- Base: `Tizen.UI.NObject`
- Members:
  - `protected .ctor()`
  - `protected Void ClearNeedUpdateMap()`
  - `protected Void ClearVisual()`
  - `protected VisualMap CreateVisualMap()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Color get_Color()`
  - `public Single get_Height()`
  - `protected Boolean get_IsVisualCreated()`
  - `public Single get_ModifyHeight()`
  - `public Single get_ModifyWidth()`
  - `public Single get_Opacity()`
  - `public VisualAlign get_Origin()`
  - `public VisualAlign get_PivotPoint()`
  - `public Int32 get_SiblingOrder()`
  - `public TransformFlags get_TransformFlags()`
  - `protected VisualMap get_VisualMap()`
  - `public Single get_Width()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `private Void OnLayoutIteration(Object sender, EventArgs e)`
  - `private Void OnUpdateRequired(Object sender, EventArgs e)`
  - `public Void set_Color(Color value)`
  - `public Void set_Height(Single value)`
  - `public Void set_ModifyHeight(Single value)`
  - `public Void set_ModifyWidth(Single value)`
  - `public Void set_Opacity(Single value)`
  - `public Void set_Origin(VisualAlign value)`
  - `public Void set_PivotPoint(VisualAlign value)`
  - `public Void set_SiblingOrder(Int32 value)`
  - `public Void set_TransformFlags(TransformFlags value)`
  - `public Void set_Width(Single value)`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `protected Void SetNeedUpdateMap()`
  - `protected Void UpdateVisualMap()`
  - `public Color Color { get; set; }`
  - `public Single Height { get; set; }`
  - `protected Boolean IsVisualCreated { get; }`
  - `public Single ModifyHeight { get; set; }`
  - `public Single ModifyWidth { get; set; }`
  - `public Single Opacity { get; set; }`
  - `public VisualAlign Origin { get; set; }`
  - `public VisualAlign PivotPoint { get; set; }`
  - `public Int32 SiblingOrder { get; set; }`
  - `public TransformFlags TransformFlags { get; set; }`
  - `protected VisualMap VisualMap { get; }`
  - `public Single Width { get; set; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`

## Namespace `Tizen.UI.Visuals.Internal`

### class `ColorVisualMap`

- Base: `Tizen.UI.Visuals.Internal.RoundedVisualMap`
- Members:
  - `public .ctor()`
  - `internal Void DirectUpdateBlurRadius(IntPtr visualObject)`
  - `internal Void DirectUpdateCutoutPolicy(IntPtr visualObject)`
  - `public Single get_BlurRadius()`
  - `public VisualCutoutPolicy get_CutoutPolicy()`
  - `public Void set_BlurRadius(Single value)`
  - `public Void set_CutoutPolicy(VisualCutoutPolicy value)`
  - `public Single BlurRadius { get; set; }`
  - `public VisualCutoutPolicy CutoutPolicy { get; set; }`

### enum `ContainerRange`

- Base: `System.Enum`
- Members:

### class `ImageVisualMap`

- Base: `Tizen.UI.Visuals.Internal.RoundedVisualMap`
- Members:
  - `public .ctor()`
  - `public String get_AlphaMaskUrl()`
  - `public Boolean get_CropToMask()`
  - `public Single get_DesiredHeight()`
  - `public Single get_DesiredWidth()`
  - `public Boolean get_FastTrackUploading()`
  - `public FittingMode get_FittingMode()`
  - `public Boolean get_ImageLoadWithViewSize()`
  - `public ImageLoadPolicy get_LoadPolicy()`
  - `public Single get_MaskContentScale()`
  - `public ImageMaskingMode get_MaskingMode()`
  - `public Boolean get_OrientationCorrection()`
  - `public Rect get_PixelArea()`
  - `public Boolean get_PreMultipliedAlpha()`
  - `public ImageReleasePolicy get_ReleasePolicy()`
  - `public String get_ResourceUrl()`
  - `public Boolean get_SynchronousLoading()`
  - `public ImageWrapMode get_WrapModeU()`
  - `public ImageWrapMode get_WrapModeV()`
  - `public Void set_AlphaMaskUrl(String value)`
  - `public Void set_CropToMask(Boolean value)`
  - `public Void set_DesiredHeight(Single value)`
  - `public Void set_DesiredWidth(Single value)`
  - `public Void set_FastTrackUploading(Boolean value)`
  - `public Void set_FittingMode(FittingMode value)`
  - `public Void set_ImageLoadWithViewSize(Boolean value)`
  - `public Void set_LoadPolicy(ImageLoadPolicy value)`
  - `public Void set_MaskContentScale(Single value)`
  - `public Void set_MaskingMode(ImageMaskingMode value)`
  - `public Void set_OrientationCorrection(Boolean value)`
  - `public Void set_PixelArea(Rect value)`
  - `public Void set_PreMultipliedAlpha(Boolean value)`
  - `public Void set_ReleasePolicy(ImageReleasePolicy value)`
  - `public Void set_ResourceUrl(String value)`
  - `public Void set_SynchronousLoading(Boolean value)`
  - `public Void set_WrapModeU(ImageWrapMode value)`
  - `public Void set_WrapModeV(ImageWrapMode value)`
  - `public String AlphaMaskUrl { get; set; }`
  - `public Boolean CropToMask { get; set; }`
  - `public Single DesiredHeight { get; set; }`
  - `public Single DesiredWidth { get; set; }`
  - `public Boolean FastTrackUploading { get; set; }`
  - `public FittingMode FittingMode { get; set; }`
  - `public Boolean ImageLoadWithViewSize { get; set; }`
  - `public ImageLoadPolicy LoadPolicy { get; set; }`
  - `public Single MaskContentScale { get; set; }`
  - `public ImageMaskingMode MaskingMode { get; set; }`
  - `public Boolean OrientationCorrection { get; set; }`
  - `public Rect PixelArea { get; set; }`
  - `public Boolean PreMultipliedAlpha { get; set; }`
  - `public ImageReleasePolicy ReleasePolicy { get; set; }`
  - `public String ResourceUrl { get; set; }`
  - `public Boolean SynchronousLoading { get; set; }`
  - `public ImageWrapMode WrapModeU { get; set; }`
  - `public ImageWrapMode WrapModeV { get; set; }`

### class `NPatchVisualMap`

- Base: `Tizen.UI.Visuals.Internal.ImageVisualMap`
- Members:
  - `public .ctor()`
  - `public Single get_AuxiliaryImageAlpha()`
  - `public String get_AuxiliaryImageUrl()`
  - `public Thickness get_Border()`
  - `public Boolean get_BorderOnly()`
  - `public Void set_AuxiliaryImageAlpha(Single value)`
  - `public Void set_AuxiliaryImageUrl(String value)`
  - `public Void set_Border(Thickness value)`
  - `public Void set_BorderOnly(Boolean value)`
  - `public Single AuxiliaryImageAlpha { get; set; }`
  - `public String AuxiliaryImageUrl { get; set; }`
  - `public Thickness Border { get; set; }`
  - `public Boolean BorderOnly { get; set; }`

### class `RoundedVisualMap`

- Base: `Tizen.UI.Visuals.Internal.VisualMap`
- Members:
  - `protected .ctor()`
  - `internal Void DirectUpdateBorderlineColor(IntPtr visualObject)`
  - `internal Void DirectUpdateBorderlineOffset(IntPtr visualObject)`
  - `internal Void DirectUpdateBorderlineWidth(IntPtr visualObject)`
  - `internal Void DirectUpdateCornerRadius(IntPtr visualObject)`
  - `internal Void DirectUpdateCornerSquareness(IntPtr visualObject)`
  - `public Color get_BorderlineColor()`
  - `public Single get_BorderlineOffset()`
  - `public Single get_BorderlineWidth()`
  - `public CornerRadius get_CornerRadius()`
  - `public CornerRadius get_CornerSquareness()`
  - `public Void set_BorderlineColor(Color value)`
  - `public Void set_BorderlineOffset(Single value)`
  - `public Void set_BorderlineWidth(Single value)`
  - `public Void set_CornerRadius(CornerRadius value)`
  - `public Void set_CornerSquareness(CornerRadius value)`
  - `public Color BorderlineColor { get; set; }`
  - `public Single BorderlineOffset { get; set; }`
  - `public Single BorderlineWidth { get; set; }`
  - `public CornerRadius CornerRadius { get; set; }`
  - `public CornerRadius CornerSquareness { get; set; }`

### class `TextVisualMap`

- Base: `Tizen.UI.Visuals.Internal.VisualMap`
- Members:
  - `public .ctor()`
  - `public String get_FontFamily()`
  - `public Single get_FontSize()`
  - `public TextAlignment get_HorizontalAlignment()`
  - `public Boolean get_IsMarkupEnabled()`
  - `public Boolean get_IsMultiline()`
  - `public String get_Text()`
  - `public Color get_TextColor()`
  - `public TextAlignment get_VerticalAlignment()`
  - `public Void set_FontFamily(String value)`
  - `public Void set_FontSize(Single value)`
  - `public Void set_HorizontalAlignment(TextAlignment value)`
  - `public Void set_IsMarkupEnabled(Boolean value)`
  - `public Void set_IsMultiline(Boolean value)`
  - `public Void set_Text(String value)`
  - `public Void set_TextColor(Color value)`
  - `public Void set_VerticalAlignment(TextAlignment value)`
  - `public String FontFamily { get; set; }`
  - `public Single FontSize { get; set; }`
  - `public TextAlignment HorizontalAlignment { get; set; }`
  - `public Boolean IsMarkupEnabled { get; set; }`
  - `public Boolean IsMultiline { get; set; }`
  - `public String Text { get; set; }`
  - `public Color TextColor { get; set; }`
  - `public TextAlignment VerticalAlignment { get; set; }`

### class `VisualMap`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `protected .ctor()`
  - `public event EventHandler UpdateRequired`
  - `public Void add_UpdateRequired(EventHandler value)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `public Color get_Color()`
  - `public PropertyMapHandle get_Handle()`
  - `public Single get_Height()`
  - `public Single get_ModifyHeight()`
  - `public Single get_ModifyWidth()`
  - `public Single get_Opacity()`
  - `public VisualAlign get_Origin()`
  - `public VisualAlign get_PivotPoint()`
  - `protected Boolean get_TransformDirty()`
  - `public TransformFlags get_TransformFlags()`
  - `private PropertyMapHandle get_TransformMap()`
  - `public Single get_Width()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Void remove_UpdateRequired(EventHandler value)`
  - `private Void SendTransformUpdateRequired()`
  - `protected Void SendUpdateRequired()`
  - `public Void set_Color(Color value)`
  - `private Void set_Handle(PropertyMapHandle value)`
  - `public Void set_Height(Single value)`
  - `public Void set_ModifyHeight(Single value)`
  - `public Void set_ModifyWidth(Single value)`
  - `public Void set_Opacity(Single value)`
  - `public Void set_Origin(VisualAlign value)`
  - `public Void set_PivotPoint(VisualAlign value)`
  - `protected Void set_TransformDirty(Boolean value)`
  - `public Void set_TransformFlags(TransformFlags value)`
  - `private Void set_TransformMap(PropertyMapHandle value)`
  - `public Void set_Width(Single value)`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public Void SetDirty()`
  - `public Void UpdateTransformMap()`
  - `public Color Color { get; set; }`
  - `public PropertyMapHandle Handle { get; set; }`
  - `public Single Height { get; set; }`
  - `public Single ModifyHeight { get; set; }`
  - `public Single ModifyWidth { get; set; }`
  - `public Single Opacity { get; set; }`
  - `public VisualAlign Origin { get; set; }`
  - `public VisualAlign PivotPoint { get; set; }`
  - `protected Boolean TransformDirty { get; set; }`
  - `public TransformFlags TransformFlags { get; set; }`
  - `private PropertyMapHandle TransformMap { get; set; }`
  - `public Single Width { get; set; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`

### class `VisualObjectContainer`

- Base: `Tizen.UI.NObject`
- Interfaces: `System.Collections.Generic.ICollection`1<Tizen.UI.Visuals.VisualObject>`, `System.Collections.Generic.IEnumerable`1<Tizen.UI.Visuals.VisualObject>`, `System.Collections.Generic.IList`1<Tizen.UI.Visuals.VisualObject>`, `System.Collections.IEnumerable`
- Members:
  - `public .ctor(View owner, ContainerRange range)`
  - `public Void Add(VisualObject item)`
  - `public Void Clear()`
  - `public Boolean Contains(VisualObject item)`
  - `public Void CopyTo(VisualObject[] array, Int32 arrayIndex)`
  - `protected Void Dispose(Boolean disposing)`
  - `public Int32 get_Count()`
  - `public Boolean get_IsReadOnly()`
  - `public VisualObject get_Item(Int32 index)`
  - `public IEnumerator<VisualObject> GetEnumerator()`
  - `public Int32 IndexOf(VisualObject item)`
  - `public Void Insert(Int32 index, VisualObject item)`
  - `private Void OnChildAdded(VisualObject child)`
  - `private Void OnChildRemoved(VisualObject child)`
  - `private Void OnCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `public Boolean Remove(VisualObject item)`
  - `public Void RemoveAt(Int32 index)`
  - `public Void set_Item(Int32 index, VisualObject value)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `public Int32 Count { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public VisualObject Item { get; set; }`

### enum `VisualTransformPropertyKey`

- Base: `System.Enum`
- Members:

