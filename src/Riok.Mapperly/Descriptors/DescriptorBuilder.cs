using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Riok.Mapperly.Abstractions.ReferenceHandling;
using Riok.Mapperly.Configuration;
using Riok.Mapperly.Descriptors.MappingBodyBuilders;
using Riok.Mapperly.Descriptors.MappingBuilders;
using Riok.Mapperly.Descriptors.ObjectFactories;
using Riok.Mapperly.Helpers;

namespace Riok.Mapperly.Descriptors;

public class DescriptorBuilder
{
    private readonly MapperDescriptor _mapperDescriptor;
    private readonly SymbolAccessor _symbolAccessor;

    private readonly MappingCollection _mappings = new();
    private readonly MethodNameBuilder _methodNameBuilder = new();
    private readonly MappingBodyBuilder _mappingBodyBuilder;
    private readonly SimpleMappingBuilderContext _builderContext;
    private readonly List<Diagnostic> _diagnostics = new();

    private ObjectFactoryCollection _objectFactories = ObjectFactoryCollection.Empty;

    public DescriptorBuilder(
        Compilation compilation,
        ClassDeclarationSyntax mapperSyntax,
        INamedTypeSymbol mapperSymbol,
        WellKnownTypes wellKnownTypes,
        SymbolAccessor symbolAccessor
    )
    {
        _mapperDescriptor = new MapperDescriptor(mapperSyntax, mapperSymbol, _methodNameBuilder);
        _symbolAccessor = symbolAccessor;
        _mappingBodyBuilder = new MappingBodyBuilder(_mappings);
        _builderContext = new SimpleMappingBuilderContext(
            compilation,
            new MapperConfiguration(symbolAccessor, mapperSymbol),
            wellKnownTypes,
            _symbolAccessor,
            _mapperDescriptor,
            _diagnostics,
            new MappingBuilder(_mappings),
            new ExistingTargetMappingBuilder(_mappings)
        );
    }

    public (MapperDescriptor descriptor, List<Diagnostic> diagnostics) Build()
    {
        ReserveMethodNames();
        ExtractObjectFactories();
        ExtractUserMappings();
        _mappingBodyBuilder.BuildMappingBodies();
        BuildMappingMethodNames();
        BuildReferenceHandlingParameters();
        AddMappingsToDescriptor();
        return (_mapperDescriptor, _diagnostics);
    }

    private void ExtractObjectFactories()
    {
        _objectFactories = ObjectFactoryBuilder.ExtractObjectFactories(_builderContext, _mapperDescriptor.Symbol);
    }

    private void ExtractUserMappings()
    {
        foreach (var userMapping in UserMethodMappingExtractor.ExtractUserMappings(_builderContext, _mapperDescriptor.Symbol))
        {
            var ctx = new MappingBuilderContext(
                _builderContext,
                _objectFactories,
                userMapping.Method,
                userMapping.SourceType,
                userMapping.TargetType
            );

            _mappings.Add(userMapping);
            _mappings.EnqueueToBuildBody(userMapping, ctx);
        }
    }

    private void ReserveMethodNames()
    {
        foreach (var methodSymbol in _symbolAccessor.GetAllMembers(_mapperDescriptor.Symbol))
        {
            _methodNameBuilder.Reserve(methodSymbol.Name);
        }
    }

    private void BuildMappingMethodNames()
    {
        foreach (var methodMapping in _mappings.MethodMappings)
        {
            methodMapping.SetMethodNameIfNeeded(_methodNameBuilder.Build);
        }
    }

    private void BuildReferenceHandlingParameters()
    {
        if (!_builderContext.MapperConfiguration.UseReferenceHandling)
            return;

        foreach (var methodMapping in _mappings.MethodMappings)
        {
            methodMapping.EnableReferenceHandling(_builderContext.Types.Get<IReferenceHandler>());
        }
    }

    private void AddMappingsToDescriptor()
    {
        // add generated mappings to the mapper
        foreach (var mapping in _mappings.MethodMappings)
        {
            _mapperDescriptor.AddTypeMapping(mapping);
        }
    }
}
