using System.Reflection;
using System.Text;
using WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;
using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodTypeGeneration;

public class ZodTypeGenerator(NullabilityInfoContext nullabilityContext, TypeRegistry typeRegistry)
{
    private readonly ZodTypeBuilder _zodTypeBuilder = new(nullabilityContext, typeRegistry);

    public string GetZod(Type type, out List<Type> dependencies)
    {
        dependencies = [];
        return TypeHelper.IsClassOrInterface(type)
            ? GenerateClassOrInterfaceZod(type, dependencies)
            : GenerateEnumZod(type);
    }

    private string GenerateClassOrInterfaceZod(Type type, List<Type> dependencies)
    {
        var memberInfo = new TypeMemberInfo(type);
        var builder = new StringBuilder();

        AppendSchemaDefinition(builder, memberInfo, dependencies);
        AppendTypeDefinition(builder, memberInfo);

        return builder.ToString();
    }

    private void AppendSchemaDefinition(StringBuilder builder, TypeMemberInfo memberInfo,
        List<Type> dependencies)
    {
        var genericPart = TypeHelper.GetGenericPart(memberInfo.GenericArguments);
        var typeName = TypeHelper.GetTypeName(memberInfo.Type);
        var members = GetMembers(memberInfo.Properties, memberInfo.Fields, dependencies);

        builder.Append($"export const {typeName.ToCamelCase()}Schema = {genericPart}");
        builder.Append($"{ZodBuilder.Object(members)};\n\n");
    }

    private static void AppendTypeDefinition(StringBuilder builder, TypeMemberInfo memberInfo)
    {
        var typeName = TypeHelper.GetTypeName(memberInfo.Type);
        if (memberInfo.Type.IsGenericTypeDefinition)
            AppendGenericTypeDefinition(builder, typeName, memberInfo.GenericArguments);
        else
            AppendNonGenericTypeDefinition(builder, typeName);
    }

    private Dictionary<string, IZodBuilder> GetMembers(PropertyInfo[] properties,
        FieldInfo[] fields, List<Type> dependencies)
    {
        var dictionary = new Dictionary<string, IZodBuilder>();

        foreach (var property in properties)
            dictionary[property.Name.ToCamelCase()] =
                _zodTypeBuilder.GetZodType(property, dependencies);

        foreach (var field in fields)
            dictionary[field.Name.ToCamelCase()] = _zodTypeBuilder.GetZodType(field, dependencies);

        return dictionary;
    }

    private static string GenerateEnumZod(Type type)
    {
        return
            $"export const {type.Name.ToCamelCase()}Schema = {ZodBuilder.NativeEnum(type.Name).Build()};\n";
    }

    private static void AppendGenericTypeDefinition(StringBuilder builder, string typeName,
        Type[] genericArguments)
    {
        builder.Append(
                $"export type {typeName}<{string.Join(", ", genericArguments.Select(a => a.Name))}> = ")
            .Append("z.infer<")
            .Append("ReturnType<")
            .Append($"typeof {typeName.ToCamelCase()}Schema<")
            .Append($"{string.Join(", ", genericArguments.Select(a => $"z.ZodType<{a.Name}>"))}")
            .Append(">>>;\n");
    }

    private static void AppendNonGenericTypeDefinition(StringBuilder builder, string typeName)
    {
        builder.Append(
            $"export type {typeName} = z.infer<typeof {typeName.ToCamelCase()}Schema>;\n");
    }
}