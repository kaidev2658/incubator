# API Summary: Tizen.UI.Scene3D

Source: `/var/folders/hz/_qhnnrpx00nbls9m60h83lym0000gp/T/dotnet-assembly-inspector/ff782852a3434a9fbcd07a0d69397c3b/lib/net8.0-tizen10.0/Tizen.UI.Scene3D.dll`
Generated (UTC): 2026-03-04T14:04:50.3303290+00:00

## Namespace `Tizen.UI.Scene3D`

### class `Animatable3DPropertyValue`1`

- Base: `System.Object`
- Interfaces: `System.IDisposable`
- Members:
  - `protected .ctor()`
  - `public static Animatable3DPropertyValue<T> CreateCustomValue(String propertyName, PropertyValueHandle value)`
  - `public static AnimatablePropertyValue CreateCustomValue(String propertyName, Object value, Boolean useVector3)`
  - `public static Animatable3DPropertyValue<T> CreateFieldOfViewValue(Single filedOfView)`
  - `public static Animatable3DPropertyValue<T> CreateOpacityValue(Single opacity)`
  - `public static Animatable3DPropertyValue<T> CreateOrthographicSizeValue(Single orthographicSize)`
  - `public static Animatable3DPropertyValue<T> CreatePosition3DValue(Single x, Single y, Single z)`
  - `protected static AnimatablePropertyHandle CreatePropertyHandle(T view, String propertyName)`
  - `public static Animatable3DPropertyValue<T> CreateRotation3DValue(Quaternion quaternion)`
  - `public static Animatable3DPropertyValue<T> CreateScale3DValue(Size3D scale)`
  - `public static Animatable3DPropertyValue<T> CreateScale3DValue(Single scaleX, Single scaleY, Single scaleZ)`
  - `public static Animatable3DPropertyValue<T> CreateSize3DValue(Size3D size)`
  - `public static Animatable3DPropertyValue<T> CreateSize3DValue(Single x, Single y, Single z)`
  - `public Void Dispose()`
  - `protected Void Dispose(Boolean disposing)`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public PropertyValueHandle Value { get; }`

### class `Animation3D`

- Base: `Tizen.UI.Animation`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Void AnimateBy(T target, Animatable3DPropertyValue<T> prop, Int32 delayMs, Int32 durationMs, AlphaFunction alpha)`
  - `public Void AnimateTo(T target, Animatable3DPropertyValue<T> prop, Int32 delayMs, Int32 durationMs, AlphaFunction alpha)`

### class `BlendShapeIndex`

- Base: `Tizen.UI.Scene3D.MotionIndex`
- Members:
  - `internal .ctor(String nodeName, String blendShapeName)`
  - `internal .ctor(String nodeName, Int32 blendShapeIndex)`
  - `internal MotionIndexHandle GenerateNativeHandle()`

### class `Camera`

- Base: `Tizen.UI.Scene3D.SceneObject`
- Members:
  - `public .ctor()`
  - `internal .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Single get_AspectRatio()`
  - `public Single get_FarPlaneDistance()`
  - `public Single get_FieldOfView()`
  - `public Single get_NearPlaneDistance()`
  - `public Single get_OrthographicSize()`
  - `public ProjectionDirection get_ProjectionDirection()`
  - `public Boolean get_UseOrthographicProjection()`
  - `public Void set_AspectRatio(Single value)`
  - `public Void set_FarPlaneDistance(Single value)`
  - `public Void set_FieldOfView(Single value)`
  - `public Void set_NearPlaneDistance(Single value)`
  - `public Void set_OrthographicSize(Single value)`
  - `public Void set_ProjectionDirection(ProjectionDirection value)`
  - `public Void set_UseOrthographicProjection(Boolean value)`
  - `public Single AspectRatio { get; set; }`
  - `public Single FarPlaneDistance { get; set; }`
  - `public Single FieldOfView { get; set; }`
  - `public Single NearPlaneDistance { get; set; }`
  - `public Single OrthographicSize { get; set; }`
  - `public ProjectionDirection ProjectionDirection { get; set; }`
  - `public Boolean UseOrthographicProjection { get; set; }`

### class `CameraExtensions`

- Base: `System.Object`
- Members:
  - `public static Single ConvertFovFromHorizontalToVertical(Camera camera, Single aspect, Single fieldOfView)`
  - `public static Single ConvertFovFromVerticalToHorizontal(Camera camera, Single aspect, Single fieldOfView)`

### class `CustomPropertyValue`1`

- Base: `Tizen.UI.Scene3D.Animatable3DPropertyValue`1<T>`
- Members:
  - `public .ctor(String propertyName, PropertyValueHandle value)`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public PropertyValueHandle Value { get; }`

### class `FieldOfViewPropertyValue`1`

- Base: `Tizen.UI.Scene3D.Animatable3DPropertyValue`1<T>`
- Members:
  - `public .ctor(Single fieldOfView)`
  - `public Single get_FieldOfView()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public Single FieldOfView { get; }`
  - `public PropertyValueHandle Value { get; }`

### interface `IMotionData`

- Members:
  - `public MotionDataHandle CreateNativeHandle()`

### class `Interop`

- Base: `System.Object`
- Members:

### class `Light`

- Base: `Tizen.UI.Scene3D.SceneObject`
- Members:
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Boolean get_IsShadowEnabled()`
  - `public Boolean get_IsShadowSoftFilteringEnabled()`
  - `public Color get_MultipliedColor()`
  - `public Single get_ShadowIntensity()`
  - `public Void set_IsShadowEnabled(Boolean value)`
  - `public Void set_IsShadowSoftFilteringEnabled(Boolean value)`
  - `public Void set_MultipliedColor(Color value)`
  - `public Void set_ShadowIntensity(Single value)`
  - `public Boolean IsShadowEnabled { get; set; }`
  - `public Boolean IsShadowSoftFilteringEnabled { get; set; }`
  - `public Color MultipliedColor { get; set; }`
  - `public Single ShadowIntensity { get; set; }`

### class `Material`

- Base: `Tizen.UI.NObject`
- Members:
  - `internal .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public .ctor()`
  - `public MaterialAlphaMode get_AlphaMode()`
  - `public Color get_BaseColorFactor()`
  - `public String get_BaseColorUrl()`
  - `public Single get_MetallicFactor()`
  - `public String get_MetallicRoughnessUrl()`
  - `public String get_Name()`
  - `public Single get_NormalScale()`
  - `public String get_NormalUrl()`
  - `public Single get_RoughnessFactor()`
  - `private PropertyValueHandle GetProperty(Int32 index)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void set_AlphaMode(MaterialAlphaMode value)`
  - `public Void set_BaseColorFactor(Color value)`
  - `public Void set_BaseColorUrl(String value)`
  - `public Void set_MetallicFactor(Single value)`
  - `public Void set_MetallicRoughnessUrl(String value)`
  - `public Void set_Name(String value)`
  - `public Void set_NormalScale(Single value)`
  - `public Void set_NormalUrl(String value)`
  - `public Void set_RoughnessFactor(Single value)`
  - `private Void SetProperty(Int32 index, PropertyValueHandle propertyValue)`
  - `public MaterialAlphaMode AlphaMode { get; set; }`
  - `public Color BaseColorFactor { get; set; }`
  - `public String BaseColorUrl { get; set; }`
  - `public Single MetallicFactor { get; set; }`
  - `public String MetallicRoughnessUrl { get; set; }`
  - `public String Name { get; set; }`
  - `public Single NormalScale { get; set; }`
  - `public String NormalUrl { get; set; }`
  - `public Single RoughnessFactor { get; set; }`

### enum `MaterialAlphaMode`

- Base: `System.Enum`
- Members:

### class `Model3D`

- Base: `Tizen.UI.Scene3D.SceneObjectGroup`1<Tizen.UI.Scene3D.SceneObject>`
- Members:
  - `public .ctor()`
  - `public .ctor(String modelUrl, String resourceDirectoryUrl)`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private event EventHandler _resourceReady`
  - `private event EventHandler<TouchEventArgs> _touchEvent`
  - `public event EventHandler<KeyEventArgs> KeyEvent`
  - `public event EventHandler ResourceReady`
  - `public event EventHandler<TouchEventArgs> TouchEvent`
  - `private Void add__resourceReady(EventHandler value)`
  - `private Void add__touchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void add_KeyEvent(EventHandler<KeyEventArgs> value)`
  - `public Void add_ResourceReady(EventHandler value)`
  - `public Void add_TouchEvent(EventHandler<TouchEventArgs> value)`
  - `public Boolean ApplyCamera(Int32 index, Camera camera)`
  - `public Boolean get_IsResourceReady()`
  - `public Color get_MultipliedColor()`
  - `public ModelNode get_RootNode()`
  - `public Animation3D GetAnimation(Int32 index)`
  - `public Animation3D GetAnimation(String name)`
  - `public Int32 GetAnimationCount()`
  - `public Int32 GetCameraCount()`
  - `private ModelNode GetRootNode()`
  - `public Animation3D LoadBvhAnimation(String bvhFilename, Vector3D scale, Boolean translateRootFromModelNode)`
  - `public Animation3D LoadMotionDataAnimation(IMotionData motionData)`
  - `private Void OnResourceReady(IntPtr view)`
  - `private Boolean OnTouchEvent(IntPtr sceneObject, IntPtr evt)`
  - `private Void remove__resourceReady(EventHandler value)`
  - `private Void remove__touchEvent(EventHandler<TouchEventArgs> value)`
  - `public Void remove_KeyEvent(EventHandler<KeyEventArgs> value)`
  - `public Void remove_ResourceReady(EventHandler value)`
  - `public Void remove_TouchEvent(EventHandler<TouchEventArgs> value)`
  - `internal Void SendKeyEvent(KeyEventArgs e)`
  - `public Void set_MultipliedColor(Color value)`
  - `public Boolean IsResourceReady { get; }`
  - `public Color MultipliedColor { get; set; }`
  - `public ModelNode RootNode { get; }`

### class `Model3DExtensions`

- Base: `System.Object`
- Members:
  - `public static Rect GetCalculateCurrentScreenRect(Model3D model)`

### class `ModelNode`

- Base: `Tizen.UI.Scene3D.SceneObjectGroup`1<Tizen.UI.Scene3D.ModelNode>`
- Members:
  - `public .ctor()`
  - `internal .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private ObservableCollection<ModelPrimitive> <.ctor>b__3_0()`
  - `public IList<ModelPrimitive> get_ModelPrimitives()`
  - `public Color get_MultipliedColor()`
  - `private ModelNode GetChildModelNodeAt(UInt32 index)`
  - `private ModelPrimitive GetModelPrimitiveAt(UInt32 index)`
  - `protected Void InitializeObservableCollection(ObservableCollection<ModelNode> collection)`
  - `private Void OnChildPrimitiveAdded(ModelPrimitive primitive)`
  - `private Void OnChildPrimitiveRemoved(ModelPrimitive primitive)`
  - `private Void OnModelPrimitivesChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void set_MultipliedColor(Color value)`
  - `public IList<ModelPrimitive> ModelPrimitives { get; }`
  - `public Color MultipliedColor { get; set; }`

### class `ModelPrimitive`

- Base: `Tizen.UI.NObject`
- Members:
  - `public .ctor()`
  - `internal .ctor(IntPtr cPtr, Boolean cMemoryOwn)`
  - `public Geometry get_Geometry()`
  - `public Material get_Material()`
  - `protected Void ReleaseHandle(IntPtr handle)`
  - `public Void set_Geometry(Geometry value)`
  - `public Void set_Material(Material value)`
  - `public Geometry Geometry { get; set; }`
  - `public Material Material { get; set; }`

### class `MotionData`

- Base: `System.Object`
- Interfaces: `Tizen.UI.Scene3D.IMotionData`
- Members:
  - `protected .ctor()`
  - `public MotionDataHandle CreateNativeHandle()`
  - `public Int32 get_Duration()`
  - `internal IEnumerable<ValueTuple<MotionIndexHandle, MotionValueHandle>> GetMotionValues()`
  - `public Void set_Duration(Int32 value)`
  - `public Int32 Duration { get; set; }`

### class `MotionData`1`

- Base: `Tizen.UI.Scene3D.MotionData`
- Members:
  - `public .ctor()`
  - `public MotionIndex get_MotionIndex()`
  - `public IList<ValueTuple<Single, T>> get_MotionValue()`
  - `internal IEnumerable<ValueTuple<MotionIndexHandle, MotionValueHandle>> GetMotionValues()`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_MotionIndex(MotionIndex value)`
  - `public MotionIndex MotionIndex { get; set; }`
  - `public IList<ValueTuple<Single, T>> MotionValue { get; }`

### class `MotionDataSet`

- Base: `Tizen.UI.Scene3D.MotionData`
- Members:
  - `public .ctor()`
  - `public IList<MotionData> get_DataSet()`
  - `internal IEnumerable<ValueTuple<MotionIndexHandle, MotionValueHandle>> GetMotionValues()`
  - `public IList<MotionData> DataSet { get; }`

### class `MotionIndex`

- Base: `System.Object`
- Members:
  - `protected .ctor()`
  - `public static MotionIndex CreateBlendShapeIndex(String blendShapeName)`
  - `public static MotionIndex CreateBlendShapeIndex(String nodeName, String blendShapeName)`
  - `public static MotionIndex CreateBlendShapeIndex(String nodeName, Int32 blendShapeIndex)`
  - `public static MotionIndex CreateTransformIndex(TransformTypes type)`
  - `public static MotionIndex CreateTransformIndex(String nodeName, TransformTypes type)`
  - `internal MotionIndexHandle GenerateNativeHandle()`

### class `OpacityPropertyValue`1`

- Base: `Tizen.UI.Scene3D.Animatable3DPropertyValue`1<T>`
- Members:
  - `public .ctor(Single opacity)`
  - `public Single get_Opacity()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public Single Opacity { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `OrthographicSizePropertyValue`1`

- Base: `Tizen.UI.Scene3D.Animatable3DPropertyValue`1<T>`
- Members:
  - `public .ctor(Single orthographicSize)`
  - `public Single get_OrthographicSize()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public Single OrthographicSize { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `Panel`

- Base: `Tizen.UI.Scene3D.SceneObject`
- Interfaces: `Tizen.UI.IParentObject`
- Members:
  - `public .ctor()`
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public View get_Content()`
  - `public Void Remove(View view)`
  - `public Void set_Content(View value)`
  - `public View Content { get; set; }`

### struct `Point3D`

- Base: `System.ValueType`
- Members:
  - `private static .cctor()`
  - `public .ctor(Single x, Single y, Single z)`
  - `public .ctor(Size3D sz)`
  - `public .ctor(Vector3D v)`
  - `public Void Deconstruct(Single& x, Single& y, Single& z)`
  - `public Double Distance(Point3D other)`
  - `public Boolean Equals(Object o)`
  - `public Boolean get_IsEmpty()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Single get_Z()`
  - `public Int32 GetHashCode()`
  - `public Point3D Offset(Single dx, Single dy, Single dz)`
  - `public static Point3D op_Addition(Point3D pt1, Point3D pt2)`
  - `public static Point3D op_Addition(Point3D pt, Size3D sz)`
  - `public static Point3D op_Addition(Point3D pt, Vector3D v)`
  - `public static Boolean op_Equality(Point3D ptA, Point3D ptB)`
  - `public static Size3D op_Explicit(Point3D pt)`
  - `public static Vector3D op_Explicit(Point3D pt)`
  - `public static Boolean op_Inequality(Point3D ptA, Point3D ptB)`
  - `public static Point3D op_Subtraction(Point3D pt1, Point3D pt2)`
  - `public static Point3D op_Subtraction(Point3D pt, Size3D sz)`
  - `public static Point3D op_Subtraction(Point3D pt, Vector3D v)`
  - `public Point3D Round()`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public Void set_Z(Single value)`
  - `public String ToString()`
  - `public Boolean IsEmpty { get; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`
  - `public Single Z { get; set; }`

### class `Position3DPropertyValue`1`

- Base: `Tizen.UI.Scene3D.Animatable3DPropertyValue`1<T>`
- Members:
  - `public .ctor(Single x, Single y, Single z)`
  - `public PropertyValueHandle get_Value()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Single get_Z()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public PropertyValueHandle Value { get; }`
  - `public Single X { get; }`
  - `public Single Y { get; }`
  - `public Single Z { get; }`

### enum `ProjectionDirection`

- Base: `System.Enum`
- Members:

### struct `Quaternion`

- Base: `System.ValueType`
- Members:
  - `public .ctor(Single x, Single y, Single z, Single w)`
  - `public .ctor(Vector3D axis, Single angleInDegree)`
  - `public static Quaternion Concatenate(Quaternion value1, Quaternion value2)`
  - `public static Quaternion Conjugate(Quaternion value)`
  - `public static Quaternion CreateFromAxisAngle(Vector3 axis, Single angle)`
  - `public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)`
  - `public static Quaternion CreateFromYawPitchRoll(Single yaw, Single pitch, Single roll)`
  - `public static Single Dot(Quaternion quaternion1, Quaternion quaternion2)`
  - `public Boolean Equals(Quaternion other)`
  - `public Boolean Equals(Object obj)`
  - `public Single get_Angle()`
  - `public Vector3D get_Axis()`
  - `public static Quaternion get_Identity()`
  - `public Boolean get_IsIdentity()`
  - `public Single get_Length()`
  - `public Single get_LengthSquared()`
  - `public Int32 GetHashCode()`
  - `public static Quaternion Inverse(Quaternion value)`
  - `public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, Single amount)`
  - `public static Quaternion Normalize(Quaternion value)`
  - `public static Quaternion op_Addition(Quaternion value1, Quaternion value2)`
  - `public static Quaternion op_Division(Quaternion value1, Quaternion value2)`
  - `public static Boolean op_Equality(Quaternion value1, Quaternion value2)`
  - `public static Boolean op_Inequality(Quaternion value1, Quaternion value2)`
  - `public static Quaternion op_Multiply(Quaternion value1, Quaternion value2)`
  - `public static Quaternion op_Multiply(Quaternion value1, Single value2)`
  - `public static Quaternion op_Subtraction(Quaternion value1, Quaternion value2)`
  - `public static Quaternion op_UnaryNegation(Quaternion value)`
  - `public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, Single amount)`
  - `public String ToString()`
  - `public Single Angle { get; }`
  - `public Vector3D Axis { get; }`
  - `public Quaternion Identity { get; }`
  - `public Boolean IsIdentity { get; }`
  - `public Single Length { get; }`
  - `public Single LengthSquared { get; }`

### class `QuaternionExtensions`

- Base: `System.Object`
- Members:
  - `public static Vector3D Rotate(Quaternion quaternion, Vector3D vector3D)`
  - `internal static Rotation ToRotation(Quaternion quaternion)`

### class `Rotation3DPropertyValue`1`

- Base: `Tizen.UI.Scene3D.Animatable3DPropertyValue`1<T>`
- Members:
  - `public .ctor(Quaternion quaternion)`
  - `public Quaternion get_Quaternion()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public Quaternion Quaternion { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `Scale3DPropertyValue`1`

- Base: `Tizen.UI.Scene3D.Animatable3DPropertyValue`1<T>`
- Members:
  - `public .ctor(Single x, Single y, Single z)`
  - `public Size3D get_Scale()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public Size3D Scale { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `Scene3DView`

- Base: `Tizen.UI.ViewGroup`
- Members:
  - `public .ctor()`
  - `public .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private event EventHandler _resourceReady`
  - `public event EventHandler CameraTransitionFinished`
  - `public event EventHandler ResourceReady`
  - `public Void Add(SceneObject sceneObject)`
  - `private Void add__resourceReady(EventHandler value)`
  - `public Void add_CameraTransitionFinished(EventHandler value)`
  - `public Void add_ResourceReady(EventHandler value)`
  - `public Void AnimateCameraTransition(Camera camera, Int32 duration, AlphaFunction alpha)`
  - `private Void AnimateCameraTransitionInternal(Camera sourceCamera, Camera destinationCamera, Int32 duration, AlphaFunction alpha)`
  - `protected Void Dispose(Boolean disposing)`
  - `public IReadOnlyList<Camera> get_Cameras()`
  - `public Camera get_DefaultCamera()`
  - `public Single get_ImageBasedLightScaleFactor()`
  - `public IReadOnlyList<Light> get_Lights()`
  - `public IReadOnlyList<Model3D> get_Models()`
  - `public Boolean get_UseFramebuffer()`
  - `public Camera GetCurrentCamera()`
  - `private Boolean IsChildModel(Model3D model3D)`
  - `private Void OnKeyEvent(Object sender, KeyEventArgs e)`
  - `private Void OnResourceReady(IntPtr view)`
  - `private Void OnTouchEvent(Object sender, TouchEventArgs e)`
  - `public Void Remove(SceneObject sceneObject)`
  - `private Void remove__resourceReady(EventHandler value)`
  - `public Void remove_CameraTransitionFinished(EventHandler value)`
  - `public Void remove_ResourceReady(EventHandler value)`
  - `public Void set_ImageBasedLightScaleFactor(Single value)`
  - `public Void set_UseFramebuffer(Boolean value)`
  - `public Void SetCurrentCamera(Camera camera)`
  - `public Void SetImageBasedLightingSource(String diffuseUrl, String specularUrl, Single scaleFactor)`
  - `public Void SetKeyEventRouteTarget(Model3D model)`
  - `public IReadOnlyList<Camera> Cameras { get; }`
  - `public Camera DefaultCamera { get; }`
  - `public Single ImageBasedLightScaleFactor { get; set; }`
  - `public IReadOnlyList<Light> Lights { get; }`
  - `public IReadOnlyList<Model3D> Models { get; }`
  - `public Boolean UseFramebuffer { get; set; }`

### class `SceneObject`

- Base: `Tizen.UI.NObject`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `public Boolean get_IsVisible()`
  - `public String get_Name()`
  - `public Single get_Opacity()`
  - `public SceneObject get_Parent()`
  - `public NObject get_ParentObject()`
  - `public Point3D get_ParentOrigin()`
  - `public Point3D get_PivotPoint()`
  - `public Point3D get_Position()`
  - `public Boolean get_PositionUsesPivotPoint()`
  - `public Quaternion get_Rotation()`
  - `public Size3D get_Scale()`
  - `public Size3D get_Size()`
  - `public Void LookAt(Vector3D target, Vector3D up, Vector3D localForward, Vector3D localUp)`
  - `public Void set_IsVisible(Boolean value)`
  - `public Void set_Name(String value)`
  - `public Void set_Opacity(Single value)`
  - `public Void set_ParentOrigin(Point3D value)`
  - `public Void set_PivotPoint(Point3D value)`
  - `public Void set_Position(Point3D value)`
  - `public Void set_PositionUsesPivotPoint(Boolean value)`
  - `public Void set_Rotation(Quaternion value)`
  - `public Void set_Scale(Size3D value)`
  - `public Void set_Size(Size3D value)`
  - `public Boolean IsVisible { get; set; }`
  - `public String Name { get; set; }`
  - `public Single Opacity { get; set; }`
  - `public SceneObject Parent { get; }`
  - `public NObject ParentObject { get; }`
  - `public Point3D ParentOrigin { get; set; }`
  - `public Point3D PivotPoint { get; set; }`
  - `public Point3D Position { get; set; }`
  - `public Boolean PositionUsesPivotPoint { get; set; }`
  - `public Quaternion Rotation { get; set; }`
  - `public Size3D Scale { get; set; }`
  - `public Size3D Size { get; set; }`

### class `SceneObjectExtensions`

- Base: `System.Object`
- Members:
  - `public static Int32 AddRenderer(SceneObject sceneObject, Renderer renderer)`
  - `public static Renderer GetRendererAt(SceneObject sceneObject, Int32 index)`
  - `public static Int32 GetRendererCount(SceneObject sceneObject)`
  - `public static Void RemoveRenderer(SceneObject sceneObject, Renderer renderer)`

### class `SceneObjectGroup`1`

- Base: `Tizen.UI.Scene3D.SceneObject`
- Interfaces: `System.Collections.Generic.ICollection`1<T>`, `System.Collections.Generic.IEnumerable`1<T>`, `System.Collections.Generic.IList`1<T>`, `System.Collections.IEnumerable`
- Members:
  - `protected .ctor(IntPtr handle, Boolean ownsHandle)`
  - `private ObservableCollection<T> <.ctor>b__4_0()`
  - `public Void Add(T item)`
  - `public Void Clear()`
  - `public Boolean Contains(T item)`
  - `public Void CopyTo(T[] array, Int32 arrayIndex)`
  - `protected Void Dispose(Boolean disposing)`
  - `private Lazy<ObservableCollection<T>> get__children()`
  - `public IList<T> get_Children()`
  - `public Int32 get_Count()`
  - `public Boolean get_IsReadOnly()`
  - `public T get_Item(Int32 index)`
  - `public IEnumerator<T> GetEnumerator()`
  - `public Int32 IndexOf(T item)`
  - `protected Void InitializeObservableCollection(ObservableCollection<T> collection)`
  - `public Void Insert(Int32 index, T item)`
  - `protected Void OnChildAdded(T sceneObject)`
  - `protected Void OnChildRemoved(T sceneObject)`
  - `private Void OnChildrenChanged(Object sender, NotifyCollectionChangedEventArgs e)`
  - `public Boolean Remove(T item)`
  - `public Void RemoveAt(Int32 index)`
  - `private Void modreq(System.Runtime.CompilerServices.IsExternalInit) set__children(Lazy<ObservableCollection<T>> value)`
  - `public Void set_Item(Int32 index, T value)`
  - `private IEnumerator System.Collections.IEnumerable.GetEnumerator()`
  - `private Lazy<ObservableCollection<T>> _children { get; set; }`
  - `public IList<T> Children { get; }`
  - `public Int32 Count { get; }`
  - `public Boolean IsReadOnly { get; }`
  - `public T Item { get; set; }`

### struct `Size3D`

- Base: `System.ValueType`
- Members:
  - `public .ctor(Single x, Single y, Single z)`
  - `public Void Deconstruct(Single& x, Single& y, Single& z)`
  - `public Boolean Equals(Size3D other)`
  - `public Boolean Equals(Object obj)`
  - `public Boolean get_IsZero()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Single get_Z()`
  - `public Int32 GetHashCode()`
  - `public static Size3D op_Addition(Size3D s1, Size3D s2)`
  - `public static Boolean op_Equality(Size3D s1, Size3D s2)`
  - `public static Point3D op_Explicit(Size3D size)`
  - `public static Boolean op_Inequality(Size3D s1, Size3D s2)`
  - `public static Size3D op_Multiply(Size3D s1, Single value)`
  - `public static Size3D op_Subtraction(Size3D s1, Size3D s2)`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public Void set_Z(Single value)`
  - `public String ToString()`
  - `public Boolean IsZero { get; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`
  - `public Single Z { get; set; }`

### class `Size3DPropertyValue`1`

- Base: `Tizen.UI.Scene3D.Animatable3DPropertyValue`1<T>`
- Members:
  - `public .ctor(Single x, Single y, Single z)`
  - `public .ctor(Size3D size)`
  - `public Size3D get_Size()`
  - `public PropertyValueHandle get_Value()`
  - `public AnimatablePropertyHandle GetTargetProperty(T view)`
  - `public Size3D Size { get; }`
  - `public PropertyValueHandle Value { get; }`

### class `TouchExtensions`

- Base: `System.Object`
- Members:
  - `public static SceneObject GetHitSceneObject(Touch touch)`

### class `TransformIndex`

- Base: `Tizen.UI.Scene3D.MotionIndex`
- Members:
  - `internal .ctor(String nodeName, TransformTypes type)`
  - `internal MotionIndexHandle GenerateNativeHandle()`

### enum `TransformTypes`

- Base: `System.Enum`
- Members:

### struct `Vector3D`

- Base: `System.ValueType`
- Members:
  - `public .ctor(Single x, Single y, Single z)`
  - `public .ctor(Point3D v)`
  - `public static Single AngleBetween(Vector3D v1, Vector3D v2)`
  - `public static Vector3D Cross(Vector3D v1, Vector3D v2)`
  - `internal static Void Cross(Vector3D& v1, Vector3D& v2, Vector3D& result)`
  - `public static Single Dot(Vector3D v1, Vector3D v2)`
  - `internal static Single Dot(Vector3D& v1, Vector3D& v2)`
  - `public Boolean Equals(Object o)`
  - `public Single get_Length()`
  - `public Single get_LengthSquared()`
  - `public Single get_X()`
  - `public Single get_Y()`
  - `public Single get_Z()`
  - `public Int32 GetHashCode()`
  - `public Void Normalize()`
  - `public static Vector3D op_Addition(Vector3D v1, Vector3D v2)`
  - `public static Vector3D op_Addition(Vector3D v1, Point3D v2)`
  - `public static Vector3D op_Division(Vector3D v, Single scalar)`
  - `public static Boolean op_Equality(Vector3D vA, Vector3D vB)`
  - `public static Point3D op_Explicit(Vector3D v)`
  - `public static Boolean op_Inequality(Vector3D vA, Vector3D vB)`
  - `public static Vector3D op_Multiply(Single scalar, Vector3D v)`
  - `public static Vector3D op_Subtraction(Vector3D v, Vector3D pt)`
  - `public static Vector3D op_Subtraction(Vector3D v, Point3D pt)`
  - `public static Vector3D op_UnaryNegation(Vector3D v)`
  - `public Vector3D Round()`
  - `public Void set_X(Single value)`
  - `public Void set_Y(Single value)`
  - `public Void set_Z(Single value)`
  - `public String ToString()`
  - `public Single Length { get; }`
  - `public Single LengthSquared { get; }`
  - `public Single X { get; set; }`
  - `public Single Y { get; set; }`
  - `public Single Z { get; set; }`

### class `Vector3DExtensions`

- Base: `System.Object`
- Members:
  - `internal static ObjectPool<Vector3Handle> ToNative(Vector3D v)`

### class `ViewExtensions`

- Base: `System.Object`
- Members:
  - `public static Quaternion GetRotation3D(NObject obj)`
  - `public static Quaternion GetWorldRotation3D(NObject obj)`
  - `public static Void LookAt(View view, Vector3D target, Vector3D up, Vector3D localForward, Vector3D localUp)`
  - `public static Void SetParentOrigin3D(View view, Point3D point)`
  - `public static Void SetPivotPoint3D(View view, Point3D point)`
  - `public static Void SetPosition3D(View view, Point3D point)`
  - `public static Void SetPosition3D(View view, Single x, Single y, Single z)`
  - `public static Void SetPositionZ(View view, Single positionZ)`
  - `public static Void SetRotation3D(View view, Quaternion quaternion)`
  - `public static Void SetScale3D(View view, Size3D scale)`
  - `public static Void SetScale3D(View view, Single x, Single y, Single z)`
  - `public static Void SetScaleZ(View view, Single scaleZ)`
  - `public static Void SetSize3D(View view, Size3D size)`

## Namespace `Tizen.UI.Scene3D.NativeHandle`

### class `AnimationHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `CameraHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `MaterialHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `ModelNodeHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `ModelPrimitiveHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `MotionDataHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `MotionIndexHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `MotionValueHandle`

- Base: `System.Runtime.InteropServices.SafeHandle`
- Members:
  - `public .ctor()`
  - `public Boolean get_IsInvalid()`
  - `protected Boolean ReleaseHandle()`
  - `public Boolean IsInvalid { get; }`

### class `PropertyValueHandleExtensions`

- Base: `System.Object`
- Members:
  - `public static PropertyValueHandle ToValue(Object value)`
  - `public static PropertyValueHandle ToValue(Point3D point3d)`
  - `public static PropertyValueHandle ToValue(Size3D size3d)`
  - `public static PropertyValueHandle ToValue(Quaternion quaternion)`

