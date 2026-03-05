using AssemblyInspector.Cli.Domain;
using AssemblyInspector.Cli.Formatting;
using Mono.Cecil;

namespace AssemblyInspector.Cli.App;

public sealed class CecilAssemblyInspector : IAssemblyInspector
{
    private const string ExtensionAttributeFullName = "System.Runtime.CompilerServices.ExtensionAttribute";
    private readonly SignatureFormatter _signatureFormatter = new();

    public ApiIndex Inspect(string assemblyPath, IEnumerable<string>? dependencySearchPaths = null)
    {
        var resolver = new DefaultAssemblyResolver();
        foreach (var directory in ResolveSearchDirectories(assemblyPath, dependencySearchPaths))
        {
            resolver.AddSearchDirectory(directory);
        }

        var readerParameters = new ReaderParameters
        {
            AssemblyResolver = resolver,
            ReadSymbols = false,
            InMemory = true
        };

        using var assembly = AssemblyDefinition.ReadAssembly(assemblyPath, readerParameters);

        var types = assembly.Modules
            .SelectMany(module => module.Types)
            .Where(type => type.Name != "<Module>")
            .OrderBy(type => type.Namespace)
            .ThenBy(type => type.Name)
            .ToList();

        var namespaces = types
            .GroupBy(type => string.IsNullOrWhiteSpace(type.Namespace) ? "(global)" : type.Namespace)
            .OrderBy(group => group.Key)
            .Select(group => new NamespaceIndex(
                group.Key,
                group.Select(MapType).ToList()))
            .ToList();

        var extensionMethods = types
            .SelectMany(MapExtensionMethods)
            .OrderBy(method => method.TargetType)
            .ThenBy(method => method.DeclaringNamespace)
            .ThenBy(method => method.DeclaringType)
            .ThenBy(method => method.MethodName)
            .ToList();

        return new ApiIndex(
            assembly.Name.Name,
            assemblyPath,
            DateTimeOffset.UtcNow,
            namespaces,
            extensionMethods);
    }

    private static IEnumerable<string> ResolveSearchDirectories(string assemblyPath, IEnumerable<string>? dependencySearchPaths)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        var assemblyDirectory = Path.GetDirectoryName(Path.GetFullPath(assemblyPath));
        if (!string.IsNullOrWhiteSpace(assemblyDirectory))
        {
            var fullAssemblyDirectory = Path.GetFullPath(assemblyDirectory);
            if (Directory.Exists(fullAssemblyDirectory) && seen.Add(fullAssemblyDirectory))
            {
                yield return fullAssemblyDirectory;
            }
        }

        if (dependencySearchPaths is null)
        {
            yield break;
        }

        foreach (var candidate in dependencySearchPaths.Where(path => !string.IsNullOrWhiteSpace(path)))
        {
            var fullPath = Path.GetFullPath(candidate);
            if (Directory.Exists(fullPath) && seen.Add(fullPath))
            {
                yield return fullPath;
            }
        }
    }

    private TypeIndex MapType(TypeDefinition type)
    {
        var members = new List<MemberSignature>();

        members.AddRange(type.Methods
            .Where(method => method.IsConstructor)
            .Select(constructor => new MemberSignature("constructor", constructor.Name, _signatureFormatter.FormatMethod(constructor))));

        members.AddRange(type.Properties
            .Select(property => new MemberSignature("property", property.Name, _signatureFormatter.FormatProperty(property))));

        members.AddRange(type.Methods
            .Where(method => !method.IsConstructor)
            .Select(method => new MemberSignature("method", method.Name, _signatureFormatter.FormatMethod(method))));

        members.AddRange(type.Events
            .Select(@event => new MemberSignature("event", @event.Name, _signatureFormatter.FormatEvent(@event))));

        members = members
            .OrderBy(member => member.Kind)
            .ThenBy(member => member.Name)
            .ToList();

        return new TypeIndex(
            type.Name,
            type.FullName,
            ResolveKind(type),
            type.BaseType?.FullName,
            type.Interfaces.Select(i => i.InterfaceType.FullName).OrderBy(name => name).ToList(),
            members);
    }

    private IEnumerable<ExtensionMethodIndex> MapExtensionMethods(TypeDefinition type)
    {
        var declaringNamespace = string.IsNullOrWhiteSpace(type.Namespace) ? "(global)" : type.Namespace;

        return type.Methods
            .Where(IsExtensionMethod)
            .Where(method => method.Parameters.Count > 0)
            .Select(method => new ExtensionMethodIndex(
                declaringNamespace,
                type.FullName,
                method.Parameters[0].ParameterType.FullName,
                method.Name,
                _signatureFormatter.FormatMethod(method)));
    }

    private static bool IsExtensionMethod(MethodDefinition method)
    {
        return method.IsStatic && method.CustomAttributes.Any(attribute => attribute.AttributeType.FullName == ExtensionAttributeFullName);
    }

    private static string ResolveKind(TypeDefinition type)
    {
        if (type.IsInterface) return "interface";
        if (type.IsEnum) return "enum";
        if (type.IsValueType) return "struct";
        if (type.IsClass) return "class";
        return "type";
    }
}
