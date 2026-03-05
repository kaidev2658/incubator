# API Summary: AssemblyInspector.Cli

Source: `input/AssemblyInspector.Cli.dll`
Generated (UTC): 2026-03-04T11:07:58.7193800+00:00

## Namespace `(global)`

### class `Program`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `private static Int32 <Main>(String[] args)`
  - `private static Task<Int32> <Main>$(String[] args)`

## Namespace `AssemblyInspector.Cli.App`

### class `CecilAssemblyInspector`

- Base: `System.Object`
- Interfaces: `AssemblyInspector.Cli.App.IAssemblyInspector`
- Members:
  - `public .ctor()`
  - `private NamespaceIndex <Inspect>b__1_6(IGrouping<String, TypeDefinition> group)`
  - `private MemberSignature <MapType>b__2_1(MethodDefinition constructor)`
  - `private MemberSignature <MapType>b__2_2(PropertyDefinition property)`
  - `private MemberSignature <MapType>b__2_4(MethodDefinition method)`
  - `private MemberSignature <MapType>b__2_5(EventDefinition event)`
  - `public ApiIndex Inspect(String assemblyPath)`
  - `private TypeIndex MapType(TypeDefinition type)`
  - `private static String ResolveKind(TypeDefinition type)`

### interface `IAssemblyInspector`

- Members:
  - `public ApiIndex Inspect(String assemblyPath)`

### class `InspectorApp`

- Base: `System.Object`
- Members:
  - `public .ctor(IAssemblyInspector inspector, JsonReportWriter jsonWriter, MarkdownReportWriter markdownWriter)`
  - `public Task RunAsync(String assemblyPath, String outputDirectory)`

### class `JsonReportWriter`

- Base: `System.Object`
- Members:
  - `private static .cctor()`
  - `public .ctor()`
  - `public Task WriteAsync(ApiIndex index, String outputPath)`

### class `MarkdownReportWriter`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public Task WriteAsync(ApiIndex index, String outputPath)`

## Namespace `AssemblyInspector.Cli.Domain`

### class `ApiIndex`

- Base: `System.Object`
- Interfaces: `System.IEquatable`1<AssemblyInspector.Cli.Domain.ApiIndex>`
- Members:
  - `public .ctor(String AssemblyName, String SourcePath, DateTimeOffset GeneratedAtUtc, IReadOnlyList<NamespaceIndex> Namespaces)`
  - `private .ctor(ApiIndex original)`
  - `public ApiIndex <Clone>$()`
  - `public Void Deconstruct(String& AssemblyName, String& SourcePath, DateTimeOffset& GeneratedAtUtc, IReadOnlyList Namespaces)`
  - `public Boolean Equals(Object obj)`
  - `public Boolean Equals(ApiIndex other)`
  - `public String get_AssemblyName()`
  - `private Type get_EqualityContract()`
  - `public DateTimeOffset get_GeneratedAtUtc()`
  - `public IReadOnlyList<NamespaceIndex> get_Namespaces()`
  - `public String get_SourcePath()`
  - `public Int32 GetHashCode()`
  - `public static Boolean op_Equality(ApiIndex left, ApiIndex right)`
  - `public static Boolean op_Inequality(ApiIndex left, ApiIndex right)`
  - `private Boolean PrintMembers(StringBuilder builder)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_AssemblyName(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_GeneratedAtUtc(DateTimeOffset value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Namespaces(IReadOnlyList<NamespaceIndex> value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_SourcePath(String value)`
  - `public String ToString()`
  - `public String AssemblyName { get; set; }`
  - `private Type EqualityContract { get; }`
  - `public DateTimeOffset GeneratedAtUtc { get; set; }`
  - `public IReadOnlyList<NamespaceIndex> Namespaces { get; set; }`
  - `public String SourcePath { get; set; }`

### class `MemberSignature`

- Base: `System.Object`
- Interfaces: `System.IEquatable`1<AssemblyInspector.Cli.Domain.MemberSignature>`
- Members:
  - `public .ctor(String Kind, String Name, String Signature)`
  - `private .ctor(MemberSignature original)`
  - `public MemberSignature <Clone>$()`
  - `public Void Deconstruct(String& Kind, String& Name, String& Signature)`
  - `public Boolean Equals(Object obj)`
  - `public Boolean Equals(MemberSignature other)`
  - `private Type get_EqualityContract()`
  - `public String get_Kind()`
  - `public String get_Name()`
  - `public String get_Signature()`
  - `public Int32 GetHashCode()`
  - `public static Boolean op_Equality(MemberSignature left, MemberSignature right)`
  - `public static Boolean op_Inequality(MemberSignature left, MemberSignature right)`
  - `private Boolean PrintMembers(StringBuilder builder)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Kind(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Name(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Signature(String value)`
  - `public String ToString()`
  - `private Type EqualityContract { get; }`
  - `public String Kind { get; set; }`
  - `public String Name { get; set; }`
  - `public String Signature { get; set; }`

### class `NamespaceIndex`

- Base: `System.Object`
- Interfaces: `System.IEquatable`1<AssemblyInspector.Cli.Domain.NamespaceIndex>`
- Members:
  - `public .ctor(String Name, IReadOnlyList<TypeIndex> Types)`
  - `private .ctor(NamespaceIndex original)`
  - `public NamespaceIndex <Clone>$()`
  - `public Void Deconstruct(String& Name, IReadOnlyList Types)`
  - `public Boolean Equals(Object obj)`
  - `public Boolean Equals(NamespaceIndex other)`
  - `private Type get_EqualityContract()`
  - `public String get_Name()`
  - `public IReadOnlyList<TypeIndex> get_Types()`
  - `public Int32 GetHashCode()`
  - `public static Boolean op_Equality(NamespaceIndex left, NamespaceIndex right)`
  - `public static Boolean op_Inequality(NamespaceIndex left, NamespaceIndex right)`
  - `private Boolean PrintMembers(StringBuilder builder)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Name(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Types(IReadOnlyList<TypeIndex> value)`
  - `public String ToString()`
  - `private Type EqualityContract { get; }`
  - `public String Name { get; set; }`
  - `public IReadOnlyList<TypeIndex> Types { get; set; }`

### class `TypeIndex`

- Base: `System.Object`
- Interfaces: `System.IEquatable`1<AssemblyInspector.Cli.Domain.TypeIndex>`
- Members:
  - `public .ctor(String Name, String FullName, String Kind, String BaseType, IReadOnlyList<String> Interfaces, IReadOnlyList<MemberSignature> Members)`
  - `private .ctor(TypeIndex original)`
  - `public TypeIndex <Clone>$()`
  - `public Void Deconstruct(String& Name, String& FullName, String& Kind, String& BaseType, IReadOnlyList Interfaces, IReadOnlyList Members)`
  - `public Boolean Equals(Object obj)`
  - `public Boolean Equals(TypeIndex other)`
  - `public String get_BaseType()`
  - `private Type get_EqualityContract()`
  - `public String get_FullName()`
  - `public IReadOnlyList<String> get_Interfaces()`
  - `public String get_Kind()`
  - `public IReadOnlyList<MemberSignature> get_Members()`
  - `public String get_Name()`
  - `public Int32 GetHashCode()`
  - `public static Boolean op_Equality(TypeIndex left, TypeIndex right)`
  - `public static Boolean op_Inequality(TypeIndex left, TypeIndex right)`
  - `private Boolean PrintMembers(StringBuilder builder)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_BaseType(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_FullName(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Interfaces(IReadOnlyList<String> value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Kind(String value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Members(IReadOnlyList<MemberSignature> value)`
  - `public Void modreq(System.Runtime.CompilerServices.IsExternalInit) set_Name(String value)`
  - `public String ToString()`
  - `public String BaseType { get; set; }`
  - `private Type EqualityContract { get; }`
  - `public String FullName { get; set; }`
  - `public IReadOnlyList<String> Interfaces { get; set; }`
  - `public String Kind { get; set; }`
  - `public IReadOnlyList<MemberSignature> Members { get; set; }`
  - `public String Name { get; set; }`

## Namespace `AssemblyInspector.Cli.Formatting`

### class `SignatureFormatter`

- Base: `System.Object`
- Members:
  - `public .ctor()`
  - `public String FormatEvent(EventDefinition event)`
  - `public String FormatMethod(MethodDefinition method)`
  - `public String FormatProperty(PropertyDefinition property)`
  - `private static String FormatType(TypeReference type)`
  - `private static String ResolveVisibility(MethodDefinition method)`
  - `private static String SimplifyName(String name)`

