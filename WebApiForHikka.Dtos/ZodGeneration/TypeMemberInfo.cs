using System.Reflection;
using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.ZodGeneration;

public class TypeMemberInfo
{
    public TypeMemberInfo(Type type)
    {
        Type = type;
        Properties = type.GetProperties();
        Fields = GetUniqueFields(type, Properties);
        GenericArguments = type.IsGenericTypeDefinition ? type.GetGenericArguments() : [];
    }

    public Type Type { get; }
    public PropertyInfo[] Properties { get; }
    public FieldInfo[] Fields { get; }
    public Type[] GenericArguments { get; }

    private static FieldInfo[] GetUniqueFields(Type type, PropertyInfo[] properties)
    {
        return type.GetFields()
            .Where(f => Array.TrueForAll(properties, p =>
                p.Name.ToCamelCase() != f.Name.ToCamelCase()))
            .ToArray();
    }
}