using Mono.Cecil;

namespace AssemblyInspector.Cli.Formatting;

public sealed class SignatureFormatter
{
    public string FormatMethod(MethodDefinition method)
    {
        var visibility = IsExplicitInterfaceImplementation(method) ? string.Empty : ResolveVisibility(method);
        var staticToken = method.IsStatic ? "static " : string.Empty;
        var returnType = method.IsConstructor ? string.Empty : $"{FormatType(method.ReturnType)} ";
        var genericParameters = method.HasGenericParameters
            ? $"<{string.Join(", ", method.GenericParameters.Select(parameter => parameter.Name))}>"
            : string.Empty;
        var parameters = string.Join(", ", method.Parameters.Select(p => $"{FormatType(p.ParameterType)} {p.Name}"));
        var constraints = FormatGenericConstraints(method.GenericParameters);
        var constraintsSuffix = constraints.Count > 0 ? $" {string.Join(" ", constraints)}" : string.Empty;

        return $"{visibility} {staticToken}{returnType}{method.Name}{genericParameters}({parameters}){constraintsSuffix}".Trim();
    }

    public string FormatProperty(PropertyDefinition property)
    {
        var getter = property.GetMethod;
        var setter = property.SetMethod;
        var accessor = getter ?? setter;

        var visibility = accessor is null || IsExplicitInterfaceImplementation(accessor) ? string.Empty : ResolveVisibility(accessor);
        var accessors = string.Join(" ", new[]
        {
            getter is null ? null : "get;",
            setter is null ? null : "set;"
        }.Where(token => token is not null));

        return $"{visibility} {FormatType(property.PropertyType)} {property.Name} {{ {accessors} }}".Trim();
    }

    public string FormatEvent(EventDefinition @event)
    {
        var visibility = @event.AddMethod is null || IsExplicitInterfaceImplementation(@event.AddMethod)
            ? string.Empty
            : ResolveVisibility(@event.AddMethod);
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
            return FormatGenericInstanceType(genericInstance);
        }

        if (type is ArrayType array)
        {
            return $"{FormatType(array.ElementType)}[]";
        }

        if (type is ByReferenceType byReference)
        {
            return $"{FormatType(byReference.ElementType)}&";
        }

        if (type is PointerType pointer)
        {
            return $"{FormatType(pointer.ElementType)}*";
        }

        if (type is GenericParameter genericParameter)
        {
            return genericParameter.Name;
        }

        var typeName = FormatNamedType(type);
        if (!type.HasGenericParameters)
        {
            return typeName;
        }

        var genericParameters = string.Join(", ", type.GenericParameters.Select(parameter => parameter.Name));
        return $"{typeName}<{genericParameters}>";
    }

    private static string FormatNamedType(TypeReference type)
    {
        var name = SimplifyName(type.Name);
        if (type.DeclaringType is null)
        {
            return name;
        }

        return $"{FormatNamedType(type.DeclaringType)}.{name}";
    }

    private static string FormatGenericInstanceType(GenericInstanceType type)
    {
        var chain = new Stack<TypeReference>();
        TypeReference? cursor = type;
        while (cursor is not null)
        {
            chain.Push(cursor);
            cursor = cursor.DeclaringType;
        }

        var genericArguments = type.GenericArguments;
        var argumentIndex = 0;
        var segments = new List<string>(chain.Count);

        foreach (var segmentType in chain)
        {
            var segment = SimplifyName(segmentType.Name);
            var arity = ResolveGenericArity(segmentType.Name);

            if (arity > 0 && argumentIndex < genericArguments.Count)
            {
                var argumentCount = Math.Min(arity, genericArguments.Count - argumentIndex);
                var formattedArguments = string.Join(", ", genericArguments.Skip(argumentIndex).Take(argumentCount).Select(FormatType));
                segment = $"{segment}<{formattedArguments}>";
                argumentIndex += argumentCount;
            }

            segments.Add(segment);
        }

        return string.Join(".", segments);
    }

    private static int ResolveGenericArity(string name)
    {
        var tickIndex = name.IndexOf('`');
        if (tickIndex < 0 || tickIndex == name.Length - 1)
        {
            return 0;
        }

        return int.TryParse(name[(tickIndex + 1)..], out var arity) ? arity : 0;
    }

    private static bool IsExplicitInterfaceImplementation(MethodDefinition method)
    {
        return method.HasOverrides || method.Name.Contains('.');
    }

    private static IReadOnlyList<string> FormatGenericConstraints(IEnumerable<GenericParameter> genericParameters)
    {
        var constraints = new List<string>();

        foreach (var parameter in genericParameters)
        {
            var constraintTokens = new List<string>();
            if (parameter.HasReferenceTypeConstraint)
            {
                constraintTokens.Add("class");
            }

            if (parameter.HasNotNullableValueTypeConstraint)
            {
                constraintTokens.Add("struct");
            }

            constraintTokens.AddRange(parameter.Constraints
                .Select(constraint => FormatType(constraint.ConstraintType))
                .Where(typeName => typeName != "ValueType"));

            if (parameter.HasDefaultConstructorConstraint)
            {
                constraintTokens.Add("new()");
            }

            if (constraintTokens.Count > 0)
            {
                constraints.Add($"where {parameter.Name} : {string.Join(", ", constraintTokens)}");
            }
        }

        return constraints;
    }

    private static string SimplifyName(string name)
    {
        var tickIndex = name.IndexOf('`');
        return tickIndex > 0 ? name[..tickIndex] : name;
    }
}
