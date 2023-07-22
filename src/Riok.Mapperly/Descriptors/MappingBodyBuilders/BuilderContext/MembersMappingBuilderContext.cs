using Riok.Mapperly.Abstractions;
using Riok.Mapperly.Configuration;
using Riok.Mapperly.Descriptors.Mappings;
using Riok.Mapperly.Diagnostics;
using Riok.Mapperly.Helpers;
using Riok.Mapperly.Symbols;

namespace Riok.Mapperly.Descriptors.MappingBodyBuilders.BuilderContext;

/// <summary>
/// An abstract base implementation of <see cref="IMembersBuilderContext{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the mapping.</typeparam>
public abstract class MembersMappingBuilderContext<T> : IMembersBuilderContext<T>
    where T : IMapping
{
    private readonly HashSet<string> _unmappedSourceMemberNames;
    private readonly IReadOnlyCollection<string> _ignoredUnmatchedTargetMemberNames;
    private readonly IReadOnlyCollection<string> _ignoredUnmatchedSourceMemberNames;

    protected MembersMappingBuilderContext(MappingBuilderContext builderContext, T mapping)
    {
        BuilderContext = builderContext;
        Mapping = mapping;
        MemberConfigsByRootTargetName = GetMemberConfigurations();

        _unmappedSourceMemberNames = GetSourceMemberNames();
        TargetMembers = GetTargetMembers();

        IgnoredSourceMemberNames = builderContext.Configuration.Properties.IgnoredSources
            .Concat(GetIgnoredObsoleteSourceMembers())
            .ToHashSet();
        var ignoredTargetMemberNames = builderContext.Configuration.Properties.IgnoredTargets
            .Concat(GetIgnoredObsoleteTargetMembers())
            .ToHashSet();

        _ignoredUnmatchedSourceMemberNames = InitIgnoredUnmatchedProperties(IgnoredSourceMemberNames, _unmappedSourceMemberNames);
        _ignoredUnmatchedTargetMemberNames = InitIgnoredUnmatchedProperties(
            builderContext.Configuration.Properties.IgnoredTargets,
            TargetMembers.Keys
        );

        _unmappedSourceMemberNames.ExceptWith(IgnoredSourceMemberNames);

        MemberConfigsByRootTargetName = GetMemberConfigurations();

        // remove explicitly mapped ignored targets from ignoredTargetMemberNames
        // then remove all ignored targets from TargetMembers, leaving unignored and explicitly mapped ignored members
        ignoredTargetMemberNames.ExceptWith(MemberConfigsByRootTargetName.Keys);
        TargetMembers.RemoveRange(ignoredTargetMemberNames);
    }

    public MappingBuilderContext BuilderContext { get; }

    public T Mapping { get; }

    public IReadOnlyCollection<string> IgnoredSourceMemberNames { get; }

    public Dictionary<string, IMappableMember> TargetMembers { get; }

    public Dictionary<string, List<PropertyMappingConfiguration>> MemberConfigsByRootTargetName { get; }

    public void AddDiagnostics()
    {
        AddUnmatchedIgnoredTargetMembersDiagnostics();
        AddUnmatchedIgnoredSourceMembersDiagnostics();
        AddUnmatchedTargetMembersDiagnostics();
        AddUnmatchedSourceMembersDiagnostics();
    }

    protected void SetSourceMemberMapped(MemberPath sourcePath) => _unmappedSourceMemberNames.Remove(sourcePath.Path.First().Name);

    private HashSet<string> InitIgnoredUnmatchedProperties(IEnumerable<string> allProperties, IEnumerable<string> mappedProperties)
    {
        var unmatched = new HashSet<string>(allProperties);
        unmatched.ExceptWith(mappedProperties);
        return unmatched;
    }

    private IEnumerable<string> GetIgnoredObsoleteTargetMembers()
    {
        var obsoleteStrategy = BuilderContext.Configuration.Properties.IgnoreObsoleteMembersStrategy;

        if (!obsoleteStrategy.HasFlag(IgnoreObsoleteMembersStrategy.Target))
            return Enumerable.Empty<string>();

        return BuilderContext.SymbolAccessor
            .GetAllAccessibleMappableMembers(Mapping.TargetType)
            .Where(x => BuilderContext.SymbolAccessor.HasAttribute<ObsoleteAttribute>(x.MemberSymbol))
            .Select(x => x.Name);
    }

    private IEnumerable<string> GetIgnoredObsoleteSourceMembers()
    {
        var obsoleteStrategy = BuilderContext.Configuration.Properties.IgnoreObsoleteMembersStrategy;

        if (!obsoleteStrategy.HasFlag(IgnoreObsoleteMembersStrategy.Source))
            return Enumerable.Empty<string>();

        return BuilderContext.SymbolAccessor
            .GetAllAccessibleMappableMembers(Mapping.SourceType)
            .Where(x => BuilderContext.SymbolAccessor.HasAttribute<ObsoleteAttribute>(x.MemberSymbol))
            .Select(x => x.Name);
    }

    private HashSet<string> GetSourceMemberNames()
    {
        return BuilderContext.SymbolAccessor.GetAllAccessibleMappableMembers(Mapping.SourceType).Select(x => x.Name).ToHashSet();
    }

    private Dictionary<string, IMappableMember> GetTargetMembers()
    {
        return BuilderContext.SymbolAccessor
            .GetAllAccessibleMappableMembers(Mapping.TargetType)
            .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
    }

    private Dictionary<string, List<PropertyMappingConfiguration>> GetMemberConfigurations()
    {
        var simpleMappings = BuilderContext.Configuration.Properties.ExplicitMappings
            .Where(x => x.Target.FullName != ".")
            .GroupBy(x => x.Target.Path.First());

        var wildcardMappings = BuilderContext.Configuration.Properties.ExplicitMappings
            .Where(x => x.Target.FullName == ".")
            .SelectMany(x =>
            {
                return Mapping.SourceType
                    .GetAccessibleMappableMembers()
                    .Where(i => i.Name == x.Source.Path.Last())
                    .SelectMany(i => i.Type.GetAccessibleMappableMembers())
                    .Select(i =>
                    {
                        var list = x.Source.Path.ToList();
                        list.Add(i.Name);
                        return new PropertyMappingConfiguration(new StringMemberPath(list), new StringMemberPath(new[] { i.Name }));
                    });
            })
            .GroupBy(x => x.Target.Path.Last());

        return simpleMappings.Union(wildcardMappings).ToDictionary(x => x.Key, x => x.ToList());
    }

    private void AddUnmatchedIgnoredTargetMembersDiagnostics()
    {
        foreach (var notFoundIgnoredMember in _ignoredUnmatchedTargetMemberNames)
        {
            BuilderContext.ReportDiagnostic(DiagnosticDescriptors.IgnoredTargetMemberNotFound, notFoundIgnoredMember, Mapping.TargetType);
        }
    }

    private void AddUnmatchedIgnoredSourceMembersDiagnostics()
    {
        foreach (var notFoundIgnoredMember in _ignoredUnmatchedSourceMemberNames)
        {
            BuilderContext.ReportDiagnostic(DiagnosticDescriptors.IgnoredSourceMemberNotFound, notFoundIgnoredMember, Mapping.SourceType);
        }
    }

    private void AddUnmatchedTargetMembersDiagnostics()
    {
        foreach (var memberConfig in MemberConfigsByRootTargetName.Values.SelectMany(x => x))
        {
            BuilderContext.ReportDiagnostic(
                DiagnosticDescriptors.ConfiguredMappingTargetMemberNotFound,
                memberConfig.Target.FullName,
                Mapping.TargetType
            );
        }
    }

    private void AddUnmatchedSourceMembersDiagnostics()
    {
        foreach (var sourceMemberName in _unmappedSourceMemberNames)
        {
            BuilderContext.ReportDiagnostic(
                DiagnosticDescriptors.SourceMemberNotMapped,
                sourceMemberName,
                Mapping.SourceType,
                Mapping.TargetType
            );
        }
    }
}
