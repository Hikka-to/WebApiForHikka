using System.Numerics;
using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.ZodGeneration;

public class TypeSynchronizer(TypeRegistry typeRegistry)
{
    public void SyncTypes()
    {
        var defaultTypes = typeRegistry.Types.Values.ToArray();
        foreach (var type in typeRegistry.Types.ToArray())
            SyncType(defaultTypes, type.Key[..type.Key.LastIndexOf('/')], type.Value);
    }

    private void SyncType(Type[] defaultTypes, string outputDir, Type? type)
    {
        if (ShouldSkipType(type)) return;

        SyncType(defaultTypes, outputDir, type!.BaseType);

        foreach (var interfaceType in type.GetInterfaces())
            SyncType(defaultTypes, outputDir, interfaceType);

        foreach (var property in type.GetProperties())
            SyncType(defaultTypes, outputDir, property.PropertyType);

        if (defaultTypes.Contains(type)) return;

        var typeName = type.IsGenericTypeDefinition
            ? type.Name[..type.Name.IndexOf('`')]
            : type.Name;

        var output = $"{outputDir}/{typeName.ToKebabCase()}.ts";
        typeRegistry.Types.TryAdd(output, type);
    }

    private static bool ShouldSkipType(Type? type)
    {
        return type is null ||
               type == typeof(object) ||
               type.GenericIsSubclassOf(typeof(string)) ||
               type.GenericIsSubclassOf(typeof(bool)) ||
               type == typeof(Guid) ||
               type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) ||
               type == typeof(TimeSpan) ||
               type.GenericIsSubclassOf(typeof(INumber<>)) ||
               type.GenericIsSubclassOf(typeof(IEnumerable<>));
    }
}