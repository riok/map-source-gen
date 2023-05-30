# Architecture

Mapperly is an incremental .NET source generator implementation.
Source generators are explained [here](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md)
and [here](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md).
However, the incremental source generator of Mapperly is not yet optimal (see [#72](https://github.com/riok/mapperly/issues/72)).

## Projects

Mapperly is structured in two projects.
`Riok.Mapperly.Abstractions` includes abstractions and attributes to be used by the application code,
this is referenced by the source generator.
`Riok.Mapperly` includes the implementation of the source generator.

## Flow

This describes the process implemented by Mapperly on a higher level.
For each discovered `MapperAttribute` a new `DescriptorBuilder` is created.
The `DescriptorBuilder` is responsible to build a `MapperDescriptor` which holds all the mappings.
The `DescriptorBuilder` does this by following this process:

1. Extracting the configuration from the attribute
2. Extracting user implemented object factories
3. Extracting user implemented and user defined mapping methods.
   It instantiates a `User*Mapping` (eg. `UserDefinedNewInstanceMethodMapping`) for each discovered mapping method and adds it to the queue of mappings to work on.
4. For each mapping in the queue the `DescriptorBuilder` tries to build its implementation bodies.
   This is done by a so called `*MappingBodyBuilder`.
   A mapping body builder tries to map each property from the source to the target.
   To do this, it asks the `DescriptorBuilder` to create mappings for the according types.
   To create a mapping from one type to another, the `DescriptorBuilder` loops through a set of `*MappingBuilder`s.
   Each of the mapping builders try to create a mapping (an `ITypeMapping` implementation) for the asked type mapping by using
   one approach on how to map types (eg. an explicit cast is implemented by the `ExplicitCastMappingBuilder`).
   These mappings are queued in the queue of mappings which need the body to be built (currently body builders are only used for object to object (property-based) mappings).
5. The `SourceEmitter` emits the code described by the `MapperDescriptor` and all its mappings.

## Roslyn multi targeting

Mapperly targets multiple Roslyn versions by building multiple NuGet packages
and merging them together into a single one.
Multi-targeting is needed to support new language features,
such as required members introduced in C# 11,
while still supporting older compiler versions.

See `build/package.sh` for details.

To introduce support for a new roslyn version see [common tasks](05-common-tasks.md#add-support-for-a-new-roslyn-version).
