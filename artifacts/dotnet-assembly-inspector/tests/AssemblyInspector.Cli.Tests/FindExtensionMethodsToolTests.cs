using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssemblyInspector.Cli.App;
using AssemblyInspector.Cli.Domain;
using AssemblyInspector.Cli.Mcp;
using Xunit;

namespace AssemblyInspector.Cli.Tests;

public sealed class FindExtensionMethodsToolTests
{
    [Fact]
    public async Task ExecuteAsync_WithTargetTypeFilter_ReturnsMatchingExtensionMethods()
    {
        var assemblyPath = typeof(CecilAssemblyInspector).Assembly.Location;
        var tool = new FindExtensionMethodsTool(
            new StaticAssemblyInspector(
                CreateApiIndex(
                    assemblyPath,
                    new ExtensionMethodIndex("Alpha.Extensions", "Alpha.StringExtensions", "System.String", "AsSafeText", "public static string AsSafeText(this string value)"),
                    new ExtensionMethodIndex("Beta.Extensions", "Beta.IntExtensions", "System.Int32", "ClampToZero", "public static int ClampToZero(this int value)"))));

        var response = await tool.ExecuteAsync(
            new FindExtensionMethodsRequest(assemblyPath, TargetTypeContains: "string"));

        var match = Assert.Single(response.Matches);
        Assert.Equal("System.String", match.TargetType);
        Assert.Equal("AsSafeText", match.MethodName);
    }

    [Fact]
    public async Task ExecuteAsync_WithNamespaceAndMethodFilters_AppliesBothFilters()
    {
        var assemblyPath = typeof(CecilAssemblyInspector).Assembly.Location;
        var tool = new FindExtensionMethodsTool(
            new StaticAssemblyInspector(
                CreateApiIndex(
                    assemblyPath,
                    new ExtensionMethodIndex("Alpha.Extensions", "Alpha.StringExtensions", "System.String", "ToSlug", "public static string ToSlug(this string value)"),
                    new ExtensionMethodIndex("Alpha.Extensions", "Alpha.StringExtensions", "System.String", "NormalizeWhitespace", "public static string NormalizeWhitespace(this string value)"),
                    new ExtensionMethodIndex("Beta.Extensions", "Beta.StringExtensions", "System.String", "ToSlug", "public static string ToSlug(this string value)"))));

        var response = await tool.ExecuteAsync(
            new FindExtensionMethodsRequest(
                assemblyPath,
                TargetTypeContains: "String",
                DeclaringNamespaceContains: "Alpha",
                MethodNameContains: "slug"));

        var match = Assert.Single(response.Matches);
        Assert.Equal("Alpha.Extensions", match.DeclaringNamespace);
        Assert.Equal("ToSlug", match.MethodName);
    }

    private static ApiIndex CreateApiIndex(string sourcePath, params ExtensionMethodIndex[] extensionMethods)
    {
        return new ApiIndex(
            "Fixture",
            sourcePath,
            DateTimeOffset.UtcNow,
            [],
            extensionMethods);
    }

    private sealed class StaticAssemblyInspector : IAssemblyInspector
    {
        private readonly ApiIndex _apiIndex;

        public StaticAssemblyInspector(ApiIndex apiIndex)
        {
            _apiIndex = apiIndex;
        }

        public ApiIndex Inspect(string assemblyPath, IEnumerable<string>? dependencySearchPaths = null)
        {
            return _apiIndex with { SourcePath = assemblyPath };
        }
    }
}
