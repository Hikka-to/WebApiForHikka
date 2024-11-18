using System.Reflection;
using WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodTypeGeneration;

public class ZodTypeBuilder(NullabilityInfoContext nullabilityContext, TypeRegistry typeRegistry)
{
    private readonly BaseZodTypeFactory _baseZodTypeFactory = new(nullabilityContext, typeRegistry);

    public IZodBuilder GetZodType(MemberInfo member, List<Type> dependencies)
    {
        var (type, nullabilityInfo) = GetTypeInfo(member);
        var zodBuilder = _baseZodTypeFactory.CreateBaseZodType(nullabilityInfo, dependencies);

        ValidationAttributeHandler.ApplyValidationAttributes(zodBuilder, member, type);

        if (nullabilityInfo.WriteState == NullabilityState.Nullable) zodBuilder.Nullish();

        return zodBuilder;
    }

    private (Type type, NullabilityInfo nullabilityInfo) GetTypeInfo(MemberInfo member)
    {
        if (member is PropertyInfo propertyInfo)
            return (propertyInfo.PropertyType, nullabilityContext.Create(propertyInfo));

        var fieldInfo = (FieldInfo)member;
        return (fieldInfo.FieldType, nullabilityContext.Create(fieldInfo));
    }
}