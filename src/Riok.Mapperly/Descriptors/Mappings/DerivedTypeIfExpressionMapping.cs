using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Riok.Mapperly.Emit.SyntaxFactoryHelper;

namespace Riok.Mapperly.Descriptors.Mappings;

/// <summary>
/// A derived type mapping maps one base type or interface to another
/// by implementing a if with instance checks over known types and performs the provided mapping for each type.
/// </summary>
public class DerivedTypeIfExpressionMapping : TypeMapping
{
    private readonly IReadOnlyCollection<ITypeMapping> _typeMappings;

    public DerivedTypeIfExpressionMapping(ITypeSymbol sourceType, ITypeSymbol targetType, IReadOnlyCollection<ITypeMapping> typeMappings)
        : base(sourceType, targetType)
    {
        _typeMappings = typeMappings;
    }

    public override ExpressionSyntax Build(TypeMappingBuildContext ctx)
    {
        // source is A x ? MapToA(x) : <other cases>
        var typeExpressions = _typeMappings
            .Reverse()
            .Aggregate<ITypeMapping, ExpressionSyntax>(DefaultLiteral(), (aggregate, current) => BuildConditional(ctx, aggregate, current));

        // cast to target type, to ensure the compiler picks the correct type
        // (B)(<ifs...>
        return CastExpression(FullyQualifiedIdentifier(TargetType), ParenthesizedExpression(typeExpressions));
    }

    private ConditionalExpressionSyntax BuildConditional(TypeMappingBuildContext ctx, ExpressionSyntax notMatched, ITypeMapping mapping)
    {
        // cannot use is pattern matching is operator due to expression limitations
        // use is with a cast instead
        // source is A ? MapToB((A)x) : <other cases>
        var castedSourceContext = ctx.WithSource(
            ParenthesizedExpression(CastExpression(FullyQualifiedIdentifier(mapping.SourceType), ctx.Source))
        );
        return ConditionalExpression(
            BinaryExpression(SyntaxKind.IsExpression, ctx.Source, FullyQualifiedIdentifier(mapping.SourceType)),
            mapping.Build(castedSourceContext),
            notMatched
        );
    }
}
