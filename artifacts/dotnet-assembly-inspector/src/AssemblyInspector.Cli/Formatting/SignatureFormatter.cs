using Mono.Cecil;

namespace AssemblyInspector.Cli.Formatting;

public sealed class SignatureFormatter
{
    public string FormatMethod(MethodDefinition method)
    {
        var visibility = ResolveVisibility(method);
        var staticToken = method.IsStatic ? "static " : string.Empty;
        var returnType = method.IsConstructor ? string.Empty : $"{FormatType(method.ReturnType)} ";
        var parameters = string.Join(", ", method.Parameters.Select(p => $"{FormatType(p.ParameterType)} {p.Name}"));

        return $"{visibility} {staticToken}{returnType}{method.Name}({parameters})".Trim();
    }

    public string FormatProperty(PropertyDefinition property)
    {
        var getter = property.GetMethod;
        var setter = property.SetMethod;
        var accessor = getter ?? setter;

        var visibility = accessor is null ? "" : ResolveVisibility(accessor);
        var accessors = string.Join(" ", new[]
        {
            getter is null ? null : "get;",
            setter is null ? null : "set;"
        }.Where(token => token is not null));

        return $"{visibility} {FormatType(property.PropertyType)} {property.Name} {{ {accessors} }}".Trim();
    }

    public string FormatEvent(EventDefinition @event)
    {
        var visibility = @event.AddMethod is null ? "" : ResolveVisibility(@event.AddMethod);
        return $"{visibility} event {FormatType(@event.EventType)} {@event.Name}".Trim();
    }

    private static string ResolveVisibility(MethodDefinition method)
    {
        if (method.IsPublic) return "public";
        if (method.IsFamily) return "protected";
        if (method.IsAssembly) return "internal";
        if (method.IsFamilyOrAssembly) return "protected internal";
        if (method.IsFamilyAndAssembly) return "private protected";
        return "private";
    }

    private static string FormatType(TypeReference type)
    {
        if (type is GenericInstanceType genericInstance)
        {
            var arguments = string.Join(", ", genericInstance.GenericArguments.Select(FormatType));
            return $"{SimplifyName(genericInstance.Name)}<{arguments}>";
        }

        if (type is ArrayType array)
        {
            return $"{FormatType(array.ElementType)}[]";
        }

        return SimplifyName(type.Name);
    }

    private static string SimplifyName(string name)
    {
        var tickIndex = name.IndexOf('`');
        return tickIndex > 0 ? name[..tickIndex] : name;
    }
}
