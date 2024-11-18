using System.Numerics;
using System.Reflection;
using WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;
using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodTypeGeneration;

public class BaseZodTypeFactory(
    NullabilityInfoContext nullabilityContext,
    TypeRegistry typeRegistry)
{
    public IZodBuilder CreateBaseZodType(NullabilityInfo nullabilityInfo, List<Type> dependencies)
    {
        var type = nullabilityInfo.Type;

        if (type == typeof(Guid)) return ZodBuilder.String().Uuid();
        if (IsString(type)) return ZodBuilder.String();
        if (IsBoolean(type)) return ZodBuilder.Boolean();
        if (IsInteger(type)) return ZodBuilder.Number().Int();
        if (IsFloatingPoint(type)) return ZodBuilder.Number();
        if (IsDateType(type)) return ZodBuilder.Date();
        if (IsDictionary(type)) return CreateDictionaryZodType(nullabilityInfo, dependencies);
        if (IsCollection(type)) return CreateCollectionZodType(nullabilityInfo, dependencies);
        if (IsNullable(type)) return CreateNullableZodType(nullabilityInfo, dependencies);
        if (IsRegisteredType(type))
            return CreateRegisteredTypeZodType(type, nullabilityInfo, dependencies);

        return type.IsGenericParameter
            ? ZodBuilder.Custom(type.Name.ToCamelCase())
            : ZodBuilder.Unknown();
    }

    private static bool IsString(Type type)
    {
        return type.GenericIsSubclassOf(typeof(string));
    }

    private static bool IsBoolean(Type type)
    {
        return type.GenericIsSubclassOf(typeof(bool));
    }

    private static bool IsInteger(Type type)
    {
        return type.GenericIsSubclassOf(typeof(INumberBase<>)) &&
               !type.GenericIsSubclassOf(typeof(IFloatingPoint<>));
    }

    private static bool IsFloatingPoint(Type type)
    {
        return type.GenericIsSubclassOf(typeof(IFloatingPoint<>));
    }

    private static bool IsDateType(Type type)
    {
        return type.GenericIsSubclassOf(typeof(DateOnly)) ||
               type.GenericIsSubclassOf(typeof(DateTimeOffset)) ||
               type.GenericIsSubclassOf(typeof(TimeSpan)) ||
               type.GenericIsSubclassOf(typeof(DateTime));
    }

    private static bool IsDictionary(Type type)
    {
        return type.GenericIsSubclassOf(typeof(IDictionary<,>));
    }

    private static bool IsCollection(Type type)
    {
        return type.GenericIsSubclassOf(typeof(Array)) ||
               type.GenericIsSubclassOf(typeof(IEnumerable<>));
    }

    private static bool IsNullable(Type type)
    {
        return type.GenericIsSubclassOf(typeof(Nullable<>));
    }

    private bool IsRegisteredType(Type type)
    {
        return typeRegistry.Types.ContainsValue(type);
    }

    private IZodBuilder CreateDictionaryZodType(NullabilityInfo nullabilityInfo,
        List<Type> dependencies)
    {
        var dictionaryInfo =
            nullabilityContext.SubClassCreate(nullabilityInfo, typeof(IDictionary<,>));
        var key = dictionaryInfo.GenericTypeArguments[0];
        var value = dictionaryInfo.GenericTypeArguments[1];

        return key.Type.GenericIsSubclassOf(typeof(string))
            ? ZodBuilder.Record(CreateBaseZodType(value, dependencies))
            : ZodBuilder.Map(
                CreateBaseZodType(key, dependencies),
                CreateBaseZodType(value, dependencies));
    }

    private ArrayZodBuilder CreateCollectionZodType(NullabilityInfo nullabilityInfo,
        List<Type> dependencies)
    {
        var elementInfo = nullabilityInfo.ElementType ??
                          nullabilityContext
                              .SubClassCreate(nullabilityInfo, typeof(IEnumerable<>))
                              .GenericTypeArguments[0];
        return ZodBuilder.Array(CreateBaseZodType(elementInfo, dependencies));
    }

    private IZodBuilder CreateNullableZodType(NullabilityInfo nullabilityInfo,
        List<Type> dependencies)
    {
        var underlyingType = Nullable.GetUnderlyingType(nullabilityInfo.Type)!;
        return CreateBaseZodType(
            nullabilityContext.SubClassCreate(
                underlyingType,
                nullabilityInfo.GenericTypeArguments,
                true,
                nullabilityInfo.ElementType,
                underlyingType),
            dependencies);
    }

    private CustomZodBuilder CreateRegisteredTypeZodType(Type type, NullabilityInfo nullabilityInfo,
        List<Type> dependencies)
    {
        var definition = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        if (!dependencies.Contains(definition)) dependencies.Add(definition);

        var schemaName = TypeHelper.GetTypeName(type).ToCamelCase() + "Schema";

        if (!type.IsGenericType) return ZodBuilder.Custom(schemaName);

        var genericArgs = string.Join(", ",
            nullabilityInfo.GenericTypeArguments.Select(a =>
                CreateBaseZodType(a, dependencies).ToString()));

        return ZodBuilder.Custom($"{schemaName}({genericArgs})");
    }
}