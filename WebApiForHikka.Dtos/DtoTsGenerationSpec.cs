using System.Reflection;
using TypeGen.Core.SpecGeneration;
using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos;

public class DtoTsGenerationSpec : GenerationSpec
{
    private const string OutputDir = "./../HikkaTsDtos";

    public DtoTsGenerationSpec()
    {
        var assembly = typeof(DtoTsGenerationSpec).Assembly;

        // Class
        var classTypes = assembly.GetTypes().Where(t =>
            t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(ExportTsClassAttribute))
        );
        foreach (var type in classTypes)
        {
            var exportTsClassAttribute = type.GetCustomAttribute<ExportTsClassAttribute>()!;
            var fullPath = Path.GetFullPath(OutputDir);
            var namespacePath = "./" + type.Namespace?.Replace("WebApiForHikka.Dtos.", "").Replace(".", "/");
            var outputDir = Path.Combine(fullPath, namespacePath, exportTsClassAttribute.OutputDir ?? "");
            outputDir = Path.GetFullPath(outputDir)
                .Replace(fullPath, OutputDir)
                .Replace("\\", "/")
                .TrimEnd('/');
            AddClass(type, outputDir);
        }

        // Interface
        var interfaceTypes = assembly.GetTypes().Where(t =>
            t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(ExportTsInterfaceAttribute))
        );
        foreach (var type in interfaceTypes)
        {
            var exportTsInterfaceAttribute = type.GetCustomAttribute<ExportTsInterfaceAttribute>()!;
            var fullPath = Path.GetFullPath(OutputDir);
            var namespacePath = "./" + type.Namespace?.Replace("WebApiForHikka.Dtos.", "").Replace(".", "/");
            var outputDir = Path.Combine(fullPath, namespacePath, exportTsInterfaceAttribute.OutputDir ?? "");
            outputDir = Path.GetFullPath(outputDir)
                .Replace(fullPath, OutputDir)
                .Replace("\\", "/")
                .TrimEnd('/');
            AddInterface(type, outputDir);
        }

        // Enum
        var enumTypes = assembly.GetTypes().Where(t =>
            t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(ExportTsEnumAttribute))
        );
        foreach (var type in enumTypes)
        {
            var exportTsEnumAttribute = type.GetCustomAttribute<ExportTsEnumAttribute>()!;
            var fullPath = Path.GetFullPath(OutputDir);
            var namespacePath = "./" + type.Namespace?.Replace("WebApiForHikka.Dtos.", "").Replace(".", "/");
            var outputDir = Path.Combine(fullPath, namespacePath, exportTsEnumAttribute.OutputDir ?? "");
            outputDir = Path.GetFullPath(outputDir)
                .Replace(fullPath, OutputDir)
                .Replace("\\", "/")
                .TrimEnd('/');
            AddEnum(type, outputDir);
        }
    }
}