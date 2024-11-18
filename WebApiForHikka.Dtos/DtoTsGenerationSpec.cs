using System.Reflection;
using TypeGen.Core.SpecGeneration;
using TypeGen.Core.SpecGeneration.Builders;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.ZodGeneration;
using WebApiForHikka.Dtos.ZodGeneration.ZodTypeGeneration;
using Type = System.Type;

namespace WebApiForHikka.Dtos;

public class DtoTsGenerationSpec : GenerationSpec
{
    private const string OutputDir = "./../HikkaTsDtos";
    private static readonly Assembly Assembly = typeof(DtoTsGenerationSpec).Assembly;
    private static readonly string CurrentDir = Path.GetFullPath("./WebApiForHikka.Dtos");
    private readonly FileGenerator _fileGenerator;

    private readonly TypeRegistry _typeRegistry;
    private readonly TypeSynchronizer _typeSynchronizer;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly ZodTypeGenerator _zodTypeGenerator;

    public DtoTsGenerationSpec()
    {
        _typeRegistry = new TypeRegistry();
        _typeSynchronizer = new TypeSynchronizer(_typeRegistry);
        _zodTypeGenerator = new ZodTypeGenerator(new NullabilityInfoContext(), _typeRegistry);
        _fileGenerator = new FileGenerator(CurrentDir, _typeRegistry, _zodTypeGenerator);

        InitializeTypes();
    }

    private void InitializeTypes()
    {
        AddTypes<ExportTsClassAttribute>(AddClass);
        AddTypes<ExportTsInterfaceAttribute>(AddInterface);
        AddTypes<ExportTsEnumAttribute>((t, o) => AddEnum(t, o));
        _typeSynchronizer.SyncTypes();
    }

    private void AddTypes<TAttribute>(Func<Type, string, SpecBuilderBase> method)
        where TAttribute : ExportAttribute
    {
        var types = Assembly.GetTypes()
            .Where(t => t.GetCustomAttributes(false)
                .Any(a => a.GetType() == typeof(TAttribute)))
            .ToArray();

        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<TAttribute>()!;
            var outputDir = GetOutputDirectory(type, attribute);
            method(type, outputDir);
            _typeRegistry.RegisterType(outputDir, type);
        }
    }

    private static string GetOutputDirectory(Type type, ExportAttribute attribute)
    {
        var fullPath = Path.GetFullPath(OutputDir, CurrentDir);
        var namespacePath =
            "./" + type.Namespace?.Replace("WebApiForHikka.Dtos.", "").Replace(".", "/");
        var outputDir = Path.Combine(fullPath, namespacePath, attribute.OutputDir ?? "");

        return Path.GetFullPath(outputDir)
            .Replace(fullPath, OutputDir)
            .Replace("\\", "/")
            .TrimEnd('/');
    }

    public override void OnAfterGeneration(OnAfterGenerationArgs args)
    {
        _fileGenerator.GenerateFiles(args.GeneratedFiles, CurrentDir);
        ClearUnusedFiles(args.GeneratedFiles);
    }

    private void ClearUnusedFiles(IEnumerable<string> generatedFiles)
    {
        var files = generatedFiles.ToArray();
        var path = Path.GetFullPath(OutputDir, CurrentDir);
        var allFiles = Directory.GetFiles(path, "*.ts", SearchOption.AllDirectories);

        foreach (var file in allFiles)
        {
            if (files.Any(f => Path.GetFullPath(f, CurrentDir) == file) &&
                _typeRegistry.Types.Keys.Any(f => Path.GetFullPath(f, CurrentDir) == file))
                continue;

            File.Delete(file);
        }
    }
}