using Microsoft.CodeAnalysis;
using Riok.Mapperly.Descriptors.Mappings;
using Riok.Mapperly.Descriptors.Mappings.ExistingTarget;
using Riok.Mapperly.Helpers;

namespace Riok.Mapperly.Descriptors.MappingBuilders;

public static class NewInstanceObjectPropertyMappingBuilder
{
    public static TypeMapping? TryBuildMapping(MappingBuilderContext ctx)
    {
        if (ctx.Target.SpecialType != SpecialType.None || ctx.Source.SpecialType != SpecialType.None)
            return null;

        if (ctx.ObjectFactories.TryFindObjectFactory(ctx.Source, ctx.Target, out var objectFactory))
            return new NewInstanceObjectFactoryPropertyMapping(ctx.Source, ctx.Target.NonNullable(), objectFactory, ctx.MapperConfiguration.UseReferenceHandling);

        if (ctx.Target is not INamedTypeSymbol namedTarget || namedTarget.Constructors.All(x => !x.IsAccessible()))
            return null;

        if (ctx.Source.IsEnum() || ctx.Target.IsEnum())
            return null;

        return new NewInstanceObjectPropertyMapping(ctx.Source, ctx.Target.NonNullable(), ctx.MapperConfiguration.UseReferenceHandling);
    }

    public static IExistingTargetMapping? TryBuildExistingTargetMapping(MappingBuilderContext ctx)
    {
        if (ctx.Target.SpecialType != SpecialType.None || ctx.Source.SpecialType != SpecialType.None)
            return null;

        if (ctx.Source.IsEnum() || ctx.Target.IsEnum())
            return null;

        return new ObjectPropertyExistingTargetMapping(ctx.Source, ctx.Target);
    }
}
