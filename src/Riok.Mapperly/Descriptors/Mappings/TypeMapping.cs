using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Riok.Mapperly.Descriptors.Mappings;

/// <inheritdoc cref="ITypeMapping"/>
[DebuggerDisplay("{GetType()}({SourceType.Name} => {TargetType.Name})")]
public abstract class TypeMapping : ITypeMapping
{
    protected TypeMapping(ITypeSymbol sourceType, ITypeSymbol targetType)
    {
        SourceType = sourceType;
        TargetType = targetType;
    }

    public ITypeSymbol SourceType { get; }

    public ITypeSymbol TargetType { get; }

    /// <inheritdoc cref="ITypeMapping.CallableByOtherMappings"/>
    public virtual bool CallableByOtherMappings => true;

    /// <inheritdoc cref="ITypeMapping.IsSynthetic"/>
    public virtual bool IsSynthetic => false;

    public abstract ExpressionSyntax Build(TypeMappingBuildContext ctx);
}
