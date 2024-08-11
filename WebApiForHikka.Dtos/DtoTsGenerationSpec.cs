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

    private Dictionary<Type, string> Types { get; } = [];

    private static string CurrentDir { get; } = Path.GetFullPath("./WebApiForHikka.Dtos");

    private void SyncTypes()
    {
        foreach (var type in Types.ToArray()) SyncType(type.Value[..type.Value.LastIndexOf('/')], type.Key);
    }

    private void SyncType(string outputDir, Type? type)
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

        SyncType(outputDir, type.BaseType);

        foreach (var interfaceType in type.GetInterfaces()) SyncType(outputDir, interfaceType);

        foreach (var property in type.GetProperties()) SyncType(outputDir, property.PropertyType);

        if (Types.ContainsKey(type)) return;

        Types.Add(type, outputDir + "/" + ToKebabCase(type.Name) + ".ts");
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
            Types.Add(type, outputDir + "/" + ToKebabCase(type.Name) + ".ts");
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
            var generatedInterface = ast.GetDescendants().OfType<InterfaceDeclaration>().FirstOrDefault();
            if (generatedInterface is not null)
            {
                var propertiesOutput = GetInterfaceProperties(generatedInterface);
                fs.Write(Encoding.UTF8.GetBytes(propertiesOutput));
            }

            var type = Types.FirstOrDefault(t => filePath == Path.GetFullPath(t.Value, CurrentDir)).Key;
            if (type is null) continue;
            List<Type> dependencies = [];
            var zodOutput = GetZod(type, dependencies);
            fs.Write(Encoding.UTF8.GetBytes(zodOutput));

            fs.Position = 0;
            source = sr.ReadToEnd();

            var imports = ast.GetDescendants().OfType<ImportDeclaration>().ToArray();
            foreach (var import in imports.Reverse())
            {
                var original = import.ImportClause.NamedBindings.Children.First().GetText();
                if (dependencies.All(d => d.Name != original)) continue;

                var name = StringToLowerCase(original) + "Schema";
                var path = import.ModuleSpecifier.GetText();
                var importOutput = $"import {{ {original}, {name} }} from {path};";
                var start = import.NodeStart;
                var end = import.End;
                source = source.Remove(start, end!.Value - start).Insert(start, importOutput);
                dependencies.Remove(dependencies.First(d => d.Name == original));
            }

            ast = new TypeScriptAST(source, filePath);

            imports = ast.GetDescendants().OfType<ImportDeclaration>().ToArray();
            var lastIndex = imports.Length > 0 ? imports.Max(i => i.End!.Value) + 1 : 131;
            foreach (var importOutput in from dependency in dependencies
                     where dependency != type
                     let typePath = new Uri(filePath, UriKind.Absolute)
                     let dependencyPath = new Uri(Path.GetFullPath(Types[dependency], CurrentDir), UriKind.Absolute)
                     let path = typePath.MakeRelativeUri(dependencyPath).ToString()
                     let name = StringToLowerCase(dependency.Name) + "Schema"
                     select $"import {{ {name} }} from '{path.Replace(".ts", "")}';\n")
            {
                source = source.Insert(lastIndex, importOutput);
                lastIndex += importOutput.Length;
            }

            source = source.Insert(lastIndex, "import { z } from 'zod';" + (imports.Length == 0 ? "\n" : ""));

            fs.Position = 0;
            fs.Write(Encoding.UTF8.GetBytes(source));
        }
    }

    private string GetZod(Type type, List<Type> dependencies)
    {
        if (type.GetCustomAttribute<ExportTsClassAttribute>() is not null)
        {
            var properties = type.GetProperties();
            var fields = type.GetFields();
            var genericArguments = type.IsGenericTypeDefinition ? type.GetGenericArguments() : [];
            var genericPart = genericArguments.Length > 0
                ? $"({string.Join(", ", genericArguments.Select(a => a.Name))}) => "
                : "";
            return $"\nexport const {StringToLowerCase(type.Name)}Schema = {genericPart}z.object({{\n" +
                   string.Join(",\n",
                       properties.Select(p => $"    {StringToLowerCase(p.Name)}: {GetZodType(p, dependencies)}")) +
                   string.Join(",\n",
                       fields.Select(f => $"    {StringToLowerCase(f.Name)}: {GetZodType(f, dependencies)}")) +
                   $"{(properties.Length + fields.Length > 0 ? "\n" : "")}}});\n";
        }

        if (type.GetCustomAttribute<ExportTsInterfaceAttribute>() is not null)
        {
            var properties = type.GetProperties();
            var fields = type.GetFields();
            var genericArguments = type.IsGenericTypeDefinition ? type.GetGenericArguments() : [];
            var genericPart = genericArguments.Length > 0
                ? $"({string.Join(", ", genericArguments.Select(a => a.Name))}) => "
                : "";
            return $"\nexport const {StringToLowerCase(type.Name)}Schema = {genericPart}z.object({{\n" +
                   string.Join(",\n",
                       properties.Select(p => $"    {StringToLowerCase(p.Name)}: {GetZodType(p, dependencies)}")) +
                   string.Join(",\n",
                       fields.Select(f => $"    {StringToLowerCase(f.Name)}: {GetZodType(f, dependencies)}")) +
                   $"{(properties.Length + fields.Length > 0 ? "\n" : "")}}});\n";
        }

        return
            $"\nexport const {StringToLowerCase(type.Name)}Keys = Object.keys({type.Name}) as [keyof typeof {type.Name}]\n" +
            $"\nexport const {StringToLowerCase(type.Name)}Schema = z.enum({StringToLowerCase(type.Name)}Keys)\n";
    }

    private static string GetInterfaceProperties(InterfaceDeclaration generatedInterface)
    {
        var properties = generatedInterface.Members.OfType<PropertySignature>();
        var propertyNames = properties.Select(p => p.Name.GetText()).ToArray();
        return
            $"\nexport const {StringToLowerCase(generatedInterface.Name.GetText())}Properties: (keyof {generatedInterface.Name.GetText()})[] = [\n"
            + string.Join(",\n", propertyNames.Select(p => $"    '{p}'"))
            + $"{(propertyNames.Length > 0 ? "\n" : "")}];\n";
    }

    private string GetZodType(NullabilityInfo nullabilityInfo, in List<Type> dependencies)
    {
        return GetZodType(nullabilityInfo.Type, nullabilityInfo.GenericTypeArguments, nullabilityInfo.ElementType,
            nullabilityInfo.WriteState == NullabilityState.Nullable, dependencies);
    }

    private string GetZodType(Type type, NullabilityInfo[] genericArguments, NullabilityInfo? elementType,
        bool nullable, List<Type> dependencies)
    {
        var result = "";

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

        else if (type.GenericIsSubclassOf(typeof(IDictionary<,>)))
        {
            var key = genericArguments[0];
            var value = genericArguments[1];
            result = key.Type.GenericIsSubclassOf(typeof(string))
                ? $"z.record({GetZodType(value, dependencies)})"
                : $"z.map({GetZodType(key, dependencies)}, {GetZodType(value, dependencies)})";
        }
        else if (type.GenericIsSubclassOf(typeof(Array)))
        {
            result = $"z.array({GetZodType(elementType!, dependencies)})";
        }
        else if (type.GenericIsSubclassOf(typeof(IEnumerable<>)))
        {
            var value = genericArguments[0];
            result = $"z.array({GetZodType(value, dependencies)})";
        }
        else if (type.GenericIsSubclassOf(typeof(Nullable<>)))
        {
            var value = Nullable.GetUnderlyingType(type);
            result = GetZodType(value!, genericArguments, elementType, false, dependencies);
        }
        else if (Types.ContainsKey(type))
        {
            if (!dependencies.Contains(type)) dependencies.Add(type);
            result = StringToLowerCase(type.Name) + "Schema";
            if (type.IsGenericType)
                result += $"({string.Join(", ", genericArguments.Select(a => GetZodType(a, dependencies)))})";
        }
        else if (type.IsGenericParameter)
        {
            result = type.Name;
            return result;
        }
        else
        {
            result = "z.unknown()";
        }

        if (type.TryGetSubclassType(typeof(IMinMaxValue<>), out var minMaxType))
        {
            object? min = null;
            try
            {
                min = minMaxType.GetField("MinValue")?.GetValue(null);
            }
            catch
            {
                // ignored
            }

            if (min == null)
                try
                {
                    min = minMaxType.GetProperty("MinValue")?.GetValue(null);
                }
                catch
                {
                    // ignored
                }

            object? max = null;
            try
            {
                max = minMaxType.GetField("MaxValue")?.GetValue(null);
            }
            catch
            {
                // ignored
            }

            if (max == null)
                try
                {
                    max = minMaxType.GetProperty("MaxValue")?.GetValue(null);
                }
                catch
                {
                    // ignored
                }

            if (min != null && max != null)
                result += $".min({min}).max({max})";
            else if (min != null)
                result += $".min({min})";
            else if (max != null)
                result += $".max({max})";
        }

        if (nullable)
            result += ".nullable()";

        return result;
    }

    private string GetZodType(MemberInfo member, List<Type> dependencies)
    {
        var nullabilityContext = new NullabilityInfoContext();
        NullabilityInfo nullabilityInfo;
        if (member is PropertyInfo propertyInfo)
            nullabilityInfo = nullabilityContext.Create(propertyInfo);
        else
            nullabilityInfo = nullabilityContext.Create((FieldInfo)member);
        var result = GetZodType(nullabilityInfo, dependencies);

        if (GetCustomAttribute<StringLengthAttribute>(member) is { } stringLength)
            result += $".length({stringLength.MaximumLength})";

        if (GetCustomAttribute<RangeAttribute>(member) is { } range)
            result += $".min({range.Minimum}).max({range.Maximum})";

        if (GetCustomAttribute<EmailAddressAttribute>(member) is not null)
            result += ".email()";

        if (GetCustomAttribute<UrlAttribute>(member) is not null)
            result += ".url()";

        if (GetCustomAttribute<RequiredAttribute>(member) is { AllowEmptyStrings: false })
            result += ".regex(/\\S/)";

        if (GetCustomAttribute<MinLengthAttribute>(member) is { } minLength)
            result += $".min({minLength.Length})";

        if (GetCustomAttribute<MaxLengthAttribute>(member) is { } maxLength)
            result += $".max({maxLength.Length})";

        if (GetCustomAttribute<RegularExpressionAttribute>(member) is { } regex)
            result += $".regex(/{regex.Pattern}/)";

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
}