using System.Diagnostics.CodeAnalysis;

namespace WebApiForHikka.SharedFunction.Extensions;

public static class TypeExtensions
{
    public static Type? GetSubclassType(this Type type, Type subclassType)
    {
        if (subclassType.IsInterface && subclassType.IsGenericTypeDefinition)
        {
            Type[] types =
            [
                type,
                ..type.GetInterfaces()
            ];

            return types.FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == subclassType);
        }

        if (subclassType.IsInterface)
        {
            Type[] types =
            [
                type,
                ..type.GetInterfaces()
            ];

            return types.FirstOrDefault(t => t == subclassType);
        }

        if (subclassType.IsGenericTypeDefinition)
        {
            for (var baseType = type; baseType != null; baseType = baseType.BaseType)
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == subclassType)
                    return baseType;

            return null;
        }

        for (var baseType = type; baseType != null; baseType = baseType.BaseType)
            if (baseType == subclassType)
                return baseType;

        return null;
    }

    public static bool TryGetSubclassType(this Type type, Type subclassType, [NotNullWhen(true)] out Type? result)
    {
        result = type.GetSubclassType(subclassType);
        return result != null;
    }

    public static bool GenericIsSubclassOf(this Type type, Type subclassType)
    {
        return type.GetSubclassType(subclassType) != null;
    }

    public static bool GenericIsAssignableFrom(this Type type, Type subclassType)
    {
        return subclassType.GenericIsSubclassOf(type);
    }
}