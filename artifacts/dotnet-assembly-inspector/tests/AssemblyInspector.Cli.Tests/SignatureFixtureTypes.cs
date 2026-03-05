using System;

namespace AssemblyInspector.Cli.Tests.SignatureFixtureTypes;

public sealed class SignatureContainer<T>
{
    public sealed class Nested;
}

public sealed class NestedTypeConsumer
{
    public SignatureContainer<string>.Nested Wrap(SignatureContainer<string>.Nested value)
    {
        return value;
    }
}

public sealed class ConstraintType
{
    public TResult Transform<TResult, TInput>(TInput input)
        where TResult : class, new()
        where TInput : System.Collections.Generic.IEnumerable<TResult>
    {
        return new TResult();
    }
}

public interface IExplicitContract
{
    void Run();
    string Title { get; }
    event EventHandler Changed;
}

public sealed class ExplicitImplementationType : IExplicitContract
{
    void IExplicitContract.Run()
    {
    }

    string IExplicitContract.Title => string.Empty;

    event EventHandler IExplicitContract.Changed
    {
        add
        {
        }
        remove
        {
        }
    }
}
