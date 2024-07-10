using System.Reflection;
using TypeGen.Core.SpecGeneration;
using TypeGen.Core.SpecGeneration.Builders;
using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos;

public class DtoTsGenerationSpec : GenerationSpec
{
    private const string OutputDir = "./../HikkaTsDtos";
    private static readonly Assembly Assembly = typeof(DtoTsGenerationSpec).Assembly;

    public DtoTsGenerationSpec()
    {
        // Class
        AddTypes<ExportTsClassAttribute>(AddClass);

        // Interface
        AddTypes<ExportTsInterfaceAttribute>(AddInterface);

        // Enum
        AddTypes<ExportTsEnumAttribute>((t, o) => AddEnum(t, o));
    }

    private void AddTypes<TAttribute>(Func<Type, string, SpecBuilderBase> method)
        where TAttribute : Attribute
    {
        var types = Assembly.GetTypes().Where(t =>
            t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(TAttribute))
        );
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<TAttribute>()!;
            var fullPath = Path.GetFullPath(OutputDir);
            var namespacePath = "./" + type.Namespace?.Replace("WebApiForHikka.Dtos.", "").Replace(".", "/");
            var outputDir = Path.Combine(fullPath, namespacePath,
                attribute.GetType().Name.Replace("Attribute", "") + "s");
            outputDir = Path.GetFullPath(outputDir)
                .Replace(fullPath, OutputDir)
                .Replace("\\", "/")
                .TrimEnd('/');
            method(type, outputDir);
        }
    }
}