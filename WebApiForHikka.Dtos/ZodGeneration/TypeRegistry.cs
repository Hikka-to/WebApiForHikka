using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.ZodGeneration;

public class TypeRegistry
{
    public Dictionary<string, Type> Types { get; } = [];

    public void RegisterType(string outputDir, Type type)
    {
        var typeName = GetTypeName(type);
        Types.Add($"{outputDir}/{typeName.ToKebabCase()}.ts", type);
    }

    private static string GetTypeName(Type type)
    {
        return type.IsGenericTypeDefinition
            ? type.Name[..type.Name.IndexOf('`')]
            : type.Name;
    }
}