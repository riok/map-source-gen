using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Riok.Mapperly.Descriptors.Constructors;
using Riok.Mapperly.Symbols.Members;

namespace Riok.Mapperly.Descriptors.Mappings.MemberMappings;

/// <summary>
/// A member initializer which initializes null members to new objects.
/// </summary>
[DebuggerDisplay("MemberNullAssignmentInitializerMapping({_pathToInitialize} ??= new())")]
public class MemberNullAssignmentInitializerMapping(MemberPathSetter pathToInitialize, IInstanceConstructor constructor)
    : MemberAssignmentMappingContainer
{
    private readonly MemberPathSetter _pathToInitialize = pathToInitialize;

    public override IEnumerable<StatementSyntax> Build(TypeMappingBuildContext ctx, ExpressionSyntax targetAccess)
    {
        // source.Value ??= new();
        var newObj = constructor.CreateInstance(ctx);
        var initializer = ctx.SyntaxFactory.ExpressionStatement(_pathToInitialize.BuildAssignment(targetAccess, newObj, true));
        return base.Build(ctx, targetAccess).Prepend(initializer);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        var other = (MemberNullAssignmentInitializerMapping)obj;
        return _pathToInitialize.Equals(other._pathToInitialize);
    }

    public override int GetHashCode() => _pathToInitialize.GetHashCode();
}
