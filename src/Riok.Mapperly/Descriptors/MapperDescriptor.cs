using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Riok.Mapperly.Descriptors.Mappings;
using Riok.Mapperly.Helpers;
using Riok.Mapperly.Symbols;
using Riok.Mapperly.Templates;

namespace Riok.Mapperly.Descriptors;

public class MapperDescriptor
{
    private readonly MapperDeclaration _declaration;
    private readonly List<MethodMapping> _methodMappings = new();
    private readonly HashSet<TemplateReference> _requiredTemplates = new();

    public MapperDescriptor(MapperDeclaration declaration, UniqueNameBuilder nameBuilder)
    {
        _declaration = declaration;
        NameBuilder = nameBuilder;
        Name = BuildName(declaration.Symbol);

        if (!Symbol.ContainingNamespace.IsGlobalNamespace)
        {
            Namespace = Symbol.ContainingNamespace.ToDisplayString();
        }
    }

    public string Name { get; }

    public string? Namespace { get; }

    public ClassDeclarationSyntax Syntax => _declaration.Syntax;

    public INamedTypeSymbol Symbol => _declaration.Symbol;

    public UniqueNameBuilder NameBuilder { get; }

    public IReadOnlyCollection<TemplateReference> RequiredTemplates => _requiredTemplates;

    public IReadOnlyCollection<MethodMapping> MethodTypeMappings => _methodMappings;

    public void AddTypeMapping(MethodMapping mapping) => _methodMappings.Add(mapping);

    public void AddRequiredTemplate(TemplateReference template) => _requiredTemplates.Add(template);

    private string BuildName(INamedTypeSymbol symbol)
    {
        if (symbol.ContainingType == null)
            return symbol.Name;

        var sb = new StringBuilder(symbol.Name);
        var containingType = symbol.ContainingType;
        while (containingType != null)
        {
            sb.Insert(0, '.').Insert(0, containingType.Name);
            containingType = containingType.ContainingType;
        }

        return sb.ToString();
    }
}
