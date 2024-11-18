using System.Text;
using WebApiForHikka.Dtos.ZodGeneration.ZodTypeGeneration;
using Zu.TypeScript;
using Zu.TypeScript.TsTypes;
using Type = System.Type;

namespace WebApiForHikka.Dtos.ZodGeneration;

public class FileGenerator(
    string directory,
    TypeRegistry typeRegistry,
    ZodTypeGenerator zodTypeGenerator)
{
    private const int StartCount = 131;

    public void GenerateFiles(IEnumerable<string> generatedFiles, string currentDir)
    {
        generatedFiles = generatedFiles.ToArray();
        foreach (var file in generatedFiles)
        {
            var filePath = Path.GetFullPath(file, currentDir);
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            using var sr = new StreamReader(fs);
            var source = sr.ReadToEnd();

            ProcessFile(filePath, source, fs, generatedFiles);
        }
    }

    private IEnumerable<IGrouping<string, Type>> GroupTypesByFile(
        IEnumerable<string> generatedFiles, IEnumerable<Type> dependencies)
    {
        return generatedFiles
            .SelectMany(f => typeRegistry.Types
                .Where(t => Path.GetFullPath(t.Key, directory) == Path.GetFullPath(f, directory))
                .Where(t => dependencies.Contains(t.Value)))
            .GroupBy(t => t.Key, t => t.Value)
            .Where(g => g.Any());
    }

    private void ProcessFile(string filePath, string source, FileStream fs,
        IEnumerable<string> generatedFiles)
    {
        var ast = new TypeScriptAST(source, filePath);
        var builder = new StringBuilder(RemoveDeclarations(source, ast));

        var type = typeRegistry.Types
            .FirstOrDefault(t => filePath == Path.GetFullPath(t.Key, directory))
            .Value;

        if (type is null) return;

        var zodOutput = zodTypeGenerator.GetZod(type, out var dependencies);
        builder.AppendLine();
        builder.Append(zodOutput);

        ProcessImports(builder, filePath, ast,
            GroupTypesByFile(generatedFiles, dependencies).ToList());
        WriteOutput(fs, builder.ToString());
    }

    private static string RemoveDeclarations(string source, TypeScriptAST ast)
    {
        if (ast.GetDescendants().OfType<InterfaceDeclaration>().FirstOrDefault() is
            { } interfaceDeclaration)
            source = source.Remove(interfaceDeclaration.NodeStart,
                    interfaceDeclaration.End!.Value - interfaceDeclaration.NodeStart)
                .Trim() + "\n";

        if (ast.GetDescendants().OfType<ClassDeclaration>().FirstOrDefault() is
            { } classDeclaration)
            source = source.Remove(classDeclaration.NodeStart,
                    classDeclaration.End!.Value - classDeclaration.NodeStart)
                .Trim() + "\n";

        return source;
    }

    private void ProcessImports(StringBuilder builder, string filePath, TypeScriptAST ast,
        List<IGrouping<string, Type>> dependencies)
    {
        var imports = ast.GetDescendants().OfType<ImportDeclaration>().ToArray();
        var isEnum = ast.GetDescendants().OfType<EnumDeclaration>().Any();
        var currentPath = new Uri(filePath, UriKind.Absolute);
        ProcessExistingImports(builder, imports, isEnum);
        AddNewImports(builder, currentPath, dependencies);
    }

    private static void ProcessExistingImports(StringBuilder builder, ImportDeclaration[] imports,
        bool isEnum)
    {
        foreach (var import in imports.Reverse())
            builder.Remove(import.NodeStart, import.End!.Value - import.NodeStart + 2);

        if (imports.Length != 0) builder.Remove(StartCount, 1);
        else if (!isEnum) builder.Insert(StartCount, "\n");
    }

    private void AddNewImports(StringBuilder builder, Uri currentPath,
        List<IGrouping<string, Type>> dependencies)
    {
        var lastImportIndex = StartCount;

        foreach (var importStatement in from dependency in dependencies
                 let dependencyPath =
                     new Uri(Path.GetFullPath(dependency.Key, directory), UriKind.Absolute)
                 let relativePath = MakeRelativePath(currentPath, dependencyPath)
                 let dependenciesName = string.Join(", ", dependency
                     .Select(GetDependencyName)
                     .Select(d => StringToLowerCase(d) + "Schema"))
                 select
                     $"import {{ {dependenciesName} }} from '{relativePath.Replace(".ts", "")}';\n")
        {
            builder.Insert(lastImportIndex, importStatement);
            lastImportIndex += importStatement.Length;
        }

        builder.Insert(lastImportIndex, "import { z } from 'zod';\n");
    }

    private static string MakeRelativePath(Uri currentPath, Uri dependencyPath)
    {
        var relativePath = currentPath.MakeRelativeUri(dependencyPath).ToString();
        if (!relativePath.StartsWith("../")) relativePath = "./" + relativePath;
        return relativePath;
    }


    private static string GetDependencyName(Type dependency)
    {
        return dependency.IsGenericTypeDefinition
            ? dependency.Name[..dependency.Name.IndexOf('`')]
            : dependency.Name;
    }

    private static void WriteOutput(FileStream fs, string content)
    {
        fs.Position = 0;
        fs.SetLength(0);
        fs.Write(Encoding.UTF8.GetBytes(content));
    }

    private static string StringToLowerCase(string str)
    {
        return str[..1].ToLower() + str[1..];
    }
}