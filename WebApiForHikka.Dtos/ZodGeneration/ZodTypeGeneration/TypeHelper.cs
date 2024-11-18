using System.Reflection;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodTypeGeneration;

public static class TypeHelper
{
    public static bool IsClassOrInterface(Type type)
    {
        return type.GetCustomAttribute<ExportTsClassAttribute>() is not null ||
               type.GetCustomAttribute<ExportTsInterfaceAttribute>() is not null ||
               (type.GetCustomAttribute<ExportTsClassAttribute>() is null && type.IsClass) ||
               (type.GetCustomAttribute<ExportTsInterfaceAttribute>() is null && type.IsInterface);
    }

    public static string GetTypeName(Type type)
    {
        return type.IsGenericTypeDefinition
            ? type.Name[..type.Name.IndexOf('`')]
            : type.Name;
    }

    public static string GetGenericPart(Type[] genericArguments)
    {
        return genericArguments.Length > 0
            ? $"<{string.Join(", ", genericArguments.Select(a => $"{a.Name} extends z.ZodTypeAny"))}>" +
              $"({string.Join(", ", genericArguments.Select(a => $"{a.Name.ToCamelCase()}: {a.Name}"))}) => "
            : "";
    }
}