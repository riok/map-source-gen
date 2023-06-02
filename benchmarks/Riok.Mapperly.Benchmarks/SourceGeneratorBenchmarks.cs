﻿using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Loggers;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;

namespace Riok.Mapperly.Benchmarks;

[MemoryDiagnoser]
[InProcess]
[HtmlExporter]
[JsonExporterAttribute.Full]
public class SourceGeneratorBenchmarks
{
    private const string SampleProjectPath = "../../../samples/Riok.Mapperly.Sample/Riok.Mapperly.Sample.csproj";
    private const string IntegrationTestProjectPath = "../../../test/Riok.Mapperly.IntegrationTests/Riok.Mapperly.IntegrationTests.csproj";

    private MSBuildWorkspace? _workspace;

    private CSharpGeneratorDriver? _sampleDriver;
    private Compilation? _sampleCompilation;

    private CSharpGeneratorDriver? _largeDriver;
    private Compilation? _largeCompilation;

    public SourceGeneratorBenchmarks()
    {
        try
        {
            MSBuildLocator.RegisterDefaults();
        }
        catch { }
    }

    private static string GetDirectoryRelativePath(string projectPath, [CallerFilePath] string callerFilePath = default!) =>
        Path.Combine(callerFilePath, projectPath);

    private async Task<(Compilation, CSharpGeneratorDriver)> SetupAsync(string projectPath)
    {
        _workspace = MSBuildWorkspace.Create();
        _workspace.WorkspaceFailed += (sender, args) =>
        {
            ConsoleLogger.Default.WriteLineError("-------------------------");
            ConsoleLogger.Default.WriteLineError(args.Diagnostic.ToString());
            ConsoleLogger.Default.WriteLineError("-------------------------");
        };

        var projectFile = GetDirectoryRelativePath(projectPath);
        if (!File.Exists(projectFile))
            throw new Exception("Project doesn't exist");

        ConsoleLogger.Default.WriteLine($"Project exists at {projectFile}");

        Project project;
        try
        {
            ConsoleLogger.Default.WriteLine("Loading project\n");
            project = await _workspace.OpenProjectAsync(projectFile);
            ConsoleLogger.Default.WriteLine("\nLoaded project");
        }
        catch (Exception ex)
        {
            ConsoleLogger.Default.WriteError(ex.Message);
            throw;
        }

        var compilation = await project.GetCompilationAsync();
        if (compilation == null)
            throw new InvalidOperationException("Compilation returned null");

        var generator = new MapperGenerator().AsSourceGenerator();

        var driver = CSharpGeneratorDriver.Create(new[] { generator }, parseOptions: (CSharpParseOptions)project.ParseOptions!);

        return (compilation, driver);
    }

    [GlobalSetup(Target = nameof(Compile))]
    public void SetupCompile() => (_sampleCompilation, _sampleDriver) = SetupAsync(SampleProjectPath).GetAwaiter().GetResult();

    [GlobalSetup(Target = nameof(LargeCompile))]
    public void SetupLargeCompile() => (_largeCompilation, _largeDriver) = SetupAsync(IntegrationTestProjectPath).GetAwaiter().GetResult();

    [Benchmark]
    public GeneratorDriver Compile() => _sampleDriver!.RunGeneratorsAndUpdateCompilation(_sampleCompilation!, out _, out _);

    [Benchmark]
    public GeneratorDriver LargeCompile() => _largeDriver!.RunGeneratorsAndUpdateCompilation(_largeCompilation!, out _, out _);

    [GlobalCleanup]
    public void Cleanup()
    {
        _workspace?.Dispose();
    }
}
