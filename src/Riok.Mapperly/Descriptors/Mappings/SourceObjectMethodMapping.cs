using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Riok.Mapperly.Emit.Syntax.SyntaxFactoryHelper;

namespace Riok.Mapperly.Descriptors.Mappings;

/// <summary>
/// Represents a mapping which works by invoking an instance method on the source object
/// and then mapping the result with an optional mapper.
/// <code>
/// target = source.ToArray();
/// target = Map(source.ToArray());
/// </code>
/// </summary>
public class SourceObjectMethodMapping : NewInstanceMapping
{
    private readonly string _methodName;
    private readonly INewInstanceMapping? _delegateMapping;

    public SourceObjectMethodMapping(
        ITypeSymbol sourceType,
        ITypeSymbol targetType,
        string methodName,
        INewInstanceMapping? delegateMapping = null
    )
        : base(sourceType, targetType)
    {
        _methodName = methodName;
        this._delegateMapping = delegateMapping;
    }

    public override ExpressionSyntax Build(TypeMappingBuildContext ctx)
    {
        var sourceExpression = InvocationExpression(MemberAccess(ctx.Source, _methodName));
        return _delegateMapping == null ? sourceExpression : _delegateMapping.Build(ctx.WithSource(sourceExpression));
    }
}
