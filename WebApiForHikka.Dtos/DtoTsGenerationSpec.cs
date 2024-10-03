using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.SpecGeneration;
using TypeGen.Core.SpecGeneration.Builders;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.SharedFunction.Extensions;
using Zu.TypeScript;
using Zu.TypeScript.TsTypes;
using Type = System.Type;

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

        SyncTypes();
    }

    private NullabilityInfoContext NullabilityContext { get; } = new();

    private Dictionary<string, Type> Types { get; } = [];

    private static string CurrentDir { get; } = Path.GetFullPath("./WebApiForHikka.Dtos");

    private void SyncTypes()
    {
        var defaultTypes = Types.Values.ToArray();
        foreach (var type in Types.ToArray()) SyncType(defaultTypes, type.Key[..type.Key.LastIndexOf('/')], type.Value);
    }

    private void SyncType(Type[] defaultTypes, string outputDir, Type? type)
    {
        if (type is null ||
            type == typeof(object) ||
            type.GenericIsSubclassOf(typeof(string)) ||
            type.GenericIsSubclassOf(typeof(bool)) ||
            type == typeof(Guid) ||
            type == typeof(DateTime) ||
            type == typeof(DateTimeOffset) ||
            type == typeof(TimeSpan) ||
            type.GenericIsSubclassOf(typeof(INumber<>)) ||
            type.GenericIsSubclassOf(typeof(IEnumerable<>))) return;

        SyncType(defaultTypes, outputDir, type.BaseType);

        foreach (var interfaceType in type.GetInterfaces()) SyncType(defaultTypes, outputDir, interfaceType);

        foreach (var property in type.GetProperties()) SyncType(defaultTypes, outputDir, property.PropertyType);

        if (defaultTypes.Contains(type)) return;

        var typeName = type.IsGenericTypeDefinition ? type.Name[..type.Name.IndexOf('`')] : type.Name;
        var output = outputDir + "/" + ToKebabCase(typeName) + ".ts";
        Types.TryAdd(output, type);
    }

    private void AddTypes<TAttribute>(Func<Type, string, SpecBuilderBase> method)
        where TAttribute : ExportAttribute
    {
        var types = Assembly.GetTypes().Where(t =>
            t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(TAttribute))
        ).ToArray();
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<TAttribute>()!;
            var fullPath = Path.GetFullPath(OutputDir, CurrentDir);
            var namespacePath = "./" + type.Namespace?.Replace("WebApiForHikka.Dtos.", "").Replace(".", "/");
            var outputDir = Path.Combine(fullPath, namespacePath, attribute.OutputDir ?? "");
            outputDir = Path.GetFullPath(outputDir)
                .Replace(fullPath, OutputDir)
                .Replace("\\", "/")
                .TrimEnd('/');
            method(type, outputDir);

            var typeName = type.IsGenericTypeDefinition ? type.Name[..type.Name.IndexOf('`')] : type.Name;
            Types.Add(outputDir + "/" + ToKebabCase(typeName) + ".ts", type);
        }
    }

    private static string ToKebabCase(string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x : x.ToString())).ToLower();
    }

    public override void OnAfterGeneration(OnAfterGenerationArgs args)
    {
        foreach (var file in args.GeneratedFiles)
        {
            var filePath = Path.GetFullPath(file, CurrentDir);
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            using var sr = new StreamReader(fs);
            var source = sr.ReadToEnd();
            var ast = new TypeScriptAST(source, filePath);

            if (ast.GetDescendants().OfType<InterfaceDeclaration>().FirstOrDefault() is { } interfaceDeclaration)
                source = source.TrimEnd().Remove(interfaceDeclaration.NodeStart,
                    interfaceDeclaration.End!.Value - interfaceDeclaration.NodeStart);
            if (ast.GetDescendants().OfType<ClassDeclaration>().FirstOrDefault() is { } classDeclaration)
                source = source.TrimEnd().Remove(classDeclaration.NodeStart,
                    classDeclaration.End!.Value - classDeclaration.NodeStart);

            var type = Types.FirstOrDefault(t => filePath == Path.GetFullPath(t.Key, CurrentDir)).Value;

            if (type is null) continue;
            List<Type> dependencies = [];
            var zodOutput = GetZod(type, dependencies);
            source += zodOutput;

            var imports = ast.GetDescendants().OfType<ImportDeclaration>().ToArray();
            foreach (var import in imports.Reverse())
            {
                var original = import.ImportClause.NamedBindings.Children.First().GetText();
                if (dependencies.All(d => d.Name != original))
                {
                    source = source.Remove(import.NodeStart, import.End!.Value - import.NodeStart + 2);
                    continue;
                }

                var name = StringToLowerCase(original) + "Schema";
                var path = import.ModuleSpecifier.GetText();
                var importOutput = $"import {{ {name} }} from {path};";
                var start = import.NodeStart;
                var end = import.End;
                source = source.Remove(start, end!.Value - start).Insert(start, importOutput);
                dependencies.Remove(dependencies.First(d => d.Name == original));
            }

            ast = new TypeScriptAST(source, filePath);

            var newImports = ast.GetDescendants().OfType<ImportDeclaration>().ToArray();
            var lastIndex = newImports.Length > 0 ? newImports.Max(i => i.End!.Value) + 1 : 131;
            foreach (var importOutput in from dependency in dependencies
                     where dependency != type
                     let typePath = new Uri(filePath, UriKind.Absolute)
                     let dependencyPath =
                         new Uri(
                             Types.Select(t => (Path.GetFullPath(t.Key, CurrentDir), t.Value))
                                 .First(t => t.Value == dependency && args.GeneratedFiles
                                     .Select(f => Path.GetFullPath(f, CurrentDir)).Contains(t.Item1))
                                 .Item1,
                             UriKind.Absolute)
                     let path = typePath.MakeRelativeUri(dependencyPath).ToString()
                     let dependencyName = dependency.IsGenericTypeDefinition
                         ? dependency.Name[..dependency.Name.IndexOf('`')]
                         : dependency.Name
                     let name = StringToLowerCase(dependencyName) + "Schema"
                     select $"import {{ {name} }} from '{path.Replace(".ts", "")}';\n")
            {
                source = source.Insert(lastIndex, importOutput);
                lastIndex += importOutput.Length;
            }

            source = source.Insert(lastIndex,
                "import { z } from 'zod';" +
                (imports.Length + dependencies.Count == 0 ? "\n" : ""));

            fs.Position = 0;
            fs.SetLength(0);
            fs.Write(Encoding.UTF8.GetBytes(source));
        }

        ClearUnusedFiles(args.GeneratedFiles);
    }

    private void ClearUnusedFiles(IEnumerable<string> generatedFiles)
    {
        generatedFiles = generatedFiles.ToArray();

        var path = Path.GetFullPath(OutputDir, CurrentDir);
        var files = Directory.GetFiles(path, "*.ts", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            if (generatedFiles.Any(f => Path.GetFullPath(f, CurrentDir) == file) &&
                Types.Keys.Any(f => Path.GetFullPath(f, CurrentDir) == file)) continue;
            File.Delete(file);
        }
    }

    private string GetZod(Type type, List<Type> dependencies)
    {
        if (type.GetCustomAttribute<ExportTsClassAttribute>() is not null ||
            (type.GetCustomAttribute<ExportTsInterfaceAttribute>() is null && type.IsClass))
        {
            var properties = type.GetProperties();
            var fields = type.GetFields();
            var genericArguments = type.IsGenericTypeDefinition ? type.GetGenericArguments() : [];
            var genericPart = genericArguments.Length > 0
                ? $"<{string.Join(", ", genericArguments.Select(a => $"{a.Name} extends z.ZodTypeAny"))}>" +
                  $"({string.Join(", ", genericArguments.Select(a => $"{StringToLowerCase(a.Name)}: {a.Name}"))}) => "
                : "";
            var typeName = type.IsGenericTypeDefinition ? type.Name[..type.Name.IndexOf('`')] : type.Name;
            var typeOutput = type.IsGenericTypeDefinition
                ? $"export type {typeName}<{string.Join(", ", genericArguments.Select(a => $"{a.Name}"))}> = " +
                  "z.infer<" +
                  "ReturnType<" +
                  $"typeof {StringToLowerCase(typeName)}Schema<" +
                  $"{string.Join(", ", genericArguments.Select(a => $"z.ZodType<{a.Name}>"))}" +
                  ">>>;\n"
                : $"export type {typeName} = z.infer<typeof {StringToLowerCase(typeName)}Schema>;\n";
            return $"export const {StringToLowerCase(typeName)}Schema = {genericPart}z.object({{\n" +
                   string.Join(",\n",
                       properties.Select(p => $"    {StringToLowerCase(p.Name)}: {GetZodType(p, dependencies)}")) +
                   string.Join(",\n",
                       fields.Select(f => $"    {StringToLowerCase(f.Name)}: {GetZodType(f, dependencies)}")) +
                   $"{(properties.Length + fields.Length > 0 ? "\n" : "")}}});\n" +
                   "\n" +
                   typeOutput;
        }

        if (type.GetCustomAttribute<ExportTsInterfaceAttribute>() is not null ||
            (type.GetCustomAttribute<ExportTsClassAttribute>() is null && type.IsInterface))
        {
            var properties = type.GetProperties();
            var fields = type.GetFields();
            var genericArguments = type.IsGenericTypeDefinition ? type.GetGenericArguments() : [];
            var genericPart = genericArguments.Length > 0
                ? $"<{string.Join(", ", genericArguments.Select(a => $"{a.Name} extends z.ZodTypeAny"))}>" +
                  $"({string.Join(", ", genericArguments.Select(a => $"{StringToLowerCase(a.Name)}: {a.Name}"))}) => "
                : "";
            var typeName = type.IsGenericTypeDefinition ? type.Name[..type.Name.IndexOf('`')] : type.Name;
            var typeOutput = type.IsGenericTypeDefinition
                ? $"export type {typeName}<{string.Join(", ", genericArguments.Select(a => $"{a.Name}"))}> = " +
                  "z.infer<" +
                  "ReturnType<" +
                  $"typeof {StringToLowerCase(typeName)}Schema<" +
                  $"{string.Join(", ", genericArguments.Select(a => $"z.ZodType<{a.Name}>"))}" +
                  ">>>;\n"
                : $"export type {typeName} = z.infer<typeof {StringToLowerCase(typeName)}Schema>;\n";
            return $"export const {StringToLowerCase(typeName)}Schema = {genericPart}z.object({{\n" +
                   string.Join(",\n",
                       properties.Select(p => $"    {StringToLowerCase(p.Name)}: {GetZodType(p, dependencies)}")) +
                   string.Join(",\n",
                       fields.Select(f => $"    {StringToLowerCase(f.Name)}: {GetZodType(f, dependencies)}")) +
                   $"{(properties.Length + fields.Length > 0 ? "\n" : "")}}});\n" +
                   "\n" +
                   typeOutput;
        }

        return $"\nexport const {StringToLowerCase(type.Name)}Schema = z.nativeEnum({type.Name});\n";
    }

    private string GetZodType(NullabilityInfo nullabilityInfo, bool addAdditions, List<Type> dependencies)
    {
        return GetZodType(nullabilityInfo.Type, nullabilityInfo.GenericTypeArguments, nullabilityInfo.ElementType,
            addAdditions,
            nullabilityInfo.WriteState == NullabilityState.Nullable, dependencies);
    }

    private string GetZodType(Type type, NullabilityInfo[] genericArguments, NullabilityInfo? elementType,
        bool addAdditions,
        bool nullable, List<Type> dependencies)
    {
        string result;

        if (type == typeof(Guid))
        {
            result = "z.string().uuid()";
        }
        else if (type.GenericIsSubclassOf(typeof(string)))
        {
            result = "z.string()";
        }
        else if (type.GenericIsSubclassOf(typeof(bool)))
        {
            result = "z.boolean()";
        }
        else if (type.GenericIsSubclassOf(typeof(INumberBase<>)) && !type.GenericIsSubclassOf(typeof(IFloatingPoint<>)))
        {
            result = "z.number().int()";
        }
        else if (type.GenericIsSubclassOf(typeof(INumberBase<>)))
        {
            result = "z.number()";
        }
        else if (type.GenericIsSubclassOf(typeof(DateOnly)) || type.GenericIsSubclassOf(typeof(DateTimeOffset)) ||
                 type.GenericIsSubclassOf(typeof(TimeSpan)) || type.GenericIsSubclassOf(typeof(DateTime)))
        {
            result = "z.date()";
        }

        else if (type.TryGetSubclassType(typeof(IDictionary<,>), out var dictionary))
        {
            Console.WriteLine($"{type.Name}");
            var key = NullabilityContext.Create(dictionary.GetProperty("Keys")!).GenericTypeArguments[0];
            var value = NullabilityContext.Create(dictionary.GetProperty("Values")!).GenericTypeArguments[0];
            result = key.Type.GenericIsSubclassOf(typeof(string))
                ? $"z.record({GetZodType(value, true, dependencies)})"
                : $"z.map({GetZodType(key, true, dependencies)}, {GetZodType(value, true, dependencies)})";
        }
        else if (type.GenericIsSubclassOf(typeof(Array)))
        {
            result = $"z.array({GetZodType(elementType!, true, dependencies)})";
        }
        else if (type.TryGetSubclassType(typeof(IEnumerable<>), out var enumerable))
        {
            var value = NullabilityContext.Create(enumerable.GetMethod("GetEnumerator")!.ReturnParameter)
                .GenericTypeArguments[0];
            result = $"z.array({GetZodType(value, true, dependencies)})";
        }
        else if (type.GenericIsSubclassOf(typeof(Nullable<>)))
        {
            var value = Nullable.GetUnderlyingType(type);
            result = GetZodType(value!, genericArguments, elementType, true, false, dependencies);
        }
        else if (Types.ContainsValue(type))
        {
            var definition = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
            if (!dependencies.Contains(definition)) dependencies.Add(definition);
            result = StringToLowerCase(type.Name) + "Schema";
            if (type.IsGenericType)
                result +=
                    $"({string.Join(", ", genericArguments.Select(a => GetZodType(a, addAdditions, dependencies)))})";
        }
        else if (type.IsGenericParameter)
        {
            result = StringToLowerCase(type.Name);
            return result;
        }
        else
        {
            result = "z.unknown()";
        }

        if (addAdditions && type.GenericIsSubclassOf(typeof(IMinMaxValue<>)))
        {
            var min = GetTypeMin(type);

            var max = GetTypeMax(type);

            if (min != null && max != null)
                result += $".min({min}).max({max})";
            else if (min != null)
                result += $".min({min})";
            else if (max != null)
                result += $".max({max})";
        }

        if (addAdditions && nullable)
            result += ".nullish()";

        return result;
    }

    private string GetZodType(MemberInfo member, List<Type> dependencies)
    {
        var propertyInfo = member as PropertyInfo;
        var fieldInfo = member as FieldInfo;
        var type = propertyInfo != null ? propertyInfo.PropertyType : fieldInfo!.FieldType;
        var nullabilityInfo = propertyInfo != null
            ? NullabilityContext.Create(propertyInfo)
            : NullabilityContext.Create(fieldInfo!);
        var result = GetZodType(nullabilityInfo, false, dependencies);

        if (GetCustomAttribute<StringLengthAttribute>(member) is { } stringLength)
            result += $".max({stringLength.MaximumLength})";

        if (GetCustomAttribute<RangeAttribute>(member) is { } range)
        {
            var min = range.Minimum;
            var max = range.Maximum;
            if (double.NegativeInfinity.Equals(min))
                min = null;
            if (float.NegativeInfinity.Equals(min))
                min = null;
            if (double.PositiveInfinity.Equals(max))
                max = null;
            if (float.PositiveInfinity.Equals(max))
                max = null;

            min ??= GetTypeMin(type);
            max ??= GetTypeMax(type);

            if (min != null && max != null)
                result += $".min({min}).max({max})";
            else if (min != null)
                result += $".min({min})";
            else if (max != null)
                result += $".max({max})";
        }

        if (GetCustomAttribute<EmailAddressAttribute>(member) is not null)
            result += ".email()";

        if (GetCustomAttribute<UrlAttribute>(member) is not null)
            result += ".url()";

        if (GetCustomAttribute<RequiredAttribute>(member) is { AllowEmptyStrings: false } &&
            type.GenericIsSubclassOf(typeof(string)))
            result += ".regex(/\\S/)";

        if (GetCustomAttribute<MinLengthAttribute>(member) is { } minLength)
            result += $".min({minLength.Length})";

        if (GetCustomAttribute<MaxLengthAttribute>(member) is { } maxLength)
            result += $".max({maxLength.Length})";

        if (GetCustomAttribute<RegularExpressionAttribute>(member) is { } regex)
            result += $".regex(/{regex.Pattern}/)";

        if (nullabilityInfo.WriteState == NullabilityState.Nullable)
            result += ".nullish()";

        return result;
    }

    private static string StringToLowerCase(string str)
    {
        return str[..1].ToLower() + str[1..];
    }

    private static TAttribute? GetCustomAttribute<TAttribute>(MemberInfo member) where TAttribute : Attribute
    {
        while (true)
        {
            if (member.GetCustomAttribute<TAttribute>() is { } attribute) return attribute;

            if (member.DeclaringType?.GetCustomAttribute<ModelMetadataTypeAttribute>() is { } modelMetadataType)
            {
                var metadataType = modelMetadataType.MetadataType;
                var metadataProperty = metadataType.GetProperty(member.Name);
                if (metadataProperty is not null)
                {
                    member = metadataProperty;
                    continue;
                }
            }

            for (var baseType = member is PropertyInfo property ? property.PropertyType : ((FieldInfo)member).FieldType;
                 baseType != null;
                 baseType = baseType.BaseType)
            {
                var baseProperty = baseType.GetProperty(member.Name);
                if (baseProperty?.GetCustomAttribute<TAttribute>() is { } baseAttribute) return baseAttribute;
            }

            return null;
        }
    }

    private static object MinValueGetter<T>() where T : IMinMaxValue<T>
    {
        return T.MinValue;
    }

    private static object MaxValueGetter<T>() where T : IMinMaxValue<T>
    {
        return T.MaxValue;
    }

    private static object? GetTypeMin(Type type)
    {
        var min = typeof(DtoTsGenerationSpec).GetMethod(nameof(MinValueGetter),
                BindingFlags.NonPublic | BindingFlags.Static)
            !.MakeGenericMethod(type)
            .Invoke(null, null);

        if (double.NegativeInfinity.Equals(min))
            min = null;
        if (float.NegativeInfinity.Equals(min))
            min = null;

        return min;
    }

    private static object? GetTypeMax(Type type)
    {
        var max = typeof(DtoTsGenerationSpec).GetMethod(nameof(MaxValueGetter),
                BindingFlags.NonPublic | BindingFlags.Static)
            !.MakeGenericMethod(type)
            .Invoke(null, null);

        if (double.PositiveInfinity.Equals(max))
            max = null;
        if (float.PositiveInfinity.Equals(max))
            max = null;

        return max;
    }
}