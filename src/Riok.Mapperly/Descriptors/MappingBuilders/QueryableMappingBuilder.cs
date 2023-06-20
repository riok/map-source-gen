using Riok.Mapperly.Abstractions;
using Riok.Mapperly.Descriptors.Mappings;
using Riok.Mapperly.Diagnostics;
using Riok.Mapperly.Helpers;

namespace Riok.Mapperly.Descriptors.MappingBuilders;

public static class QueryableMappingBuilder
{
    public static TypeMapping? TryBuildMapping(MappingBuilderContext ctx)
    {
        if (!ctx.IsConversionEnabled(MappingConversionType.Queryable))
            return null;

        if (!ctx.Source.ImplementsGeneric(ctx.Types.Get(typeof(IQueryable<>)), out var sourceQueryable))
            return null;

        if (!ctx.Target.ImplementsGeneric(ctx.Types.Get(typeof(IQueryable<>)), out var targetQueryable))
            return null;

        var sourceType = sourceQueryable.TypeArguments[0];
        var targetType = targetQueryable.TypeArguments[0];

        var inlineCtx = new InlineExpressionMappingBuilderContext(ctx, sourceType, targetType);
        var mapping = inlineCtx.BuildMapping(sourceType, targetType, MappingBuildingOptions.KeepUserSymbol);
        if (mapping == null)
            return null;

        if (ctx.MapperConfiguration.UseReferenceHandling)
        {
            ctx.ReportDiagnostic(DiagnosticDescriptors.QueryableProjectionMappingsDoNotSupportReferenceHandling);
        }

        return new QueryableProjectionMapping(ctx.Source, ctx.Target, mapping);
    }
}
