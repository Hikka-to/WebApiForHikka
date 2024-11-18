namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public static class ZodBuilder
{
    public static StringZodBuilder String()
    {
        return new StringZodBuilder();
    }

    public static NumberZodBuilder Number()
    {
        return new NumberZodBuilder();
    }


    public static BooleanZodBuilder Boolean()
    {
        return new BooleanZodBuilder();
    }

    public static DateZodBuilder Date()
    {
        return new DateZodBuilder();
    }

    public static ArrayZodBuilder Array(IZodBuilder elementType)
    {
        return new ArrayZodBuilder(elementType);
    }

    public static RecordZodBuilder Record(IZodBuilder valueType)
    {
        return new RecordZodBuilder(valueType);
    }

    public static MapZodBuilder Map(IZodBuilder keyType, IZodBuilder valueType)
    {
        return new MapZodBuilder(keyType, valueType);
    }

    public static EnumZodBuilder NativeEnum(string enumName)
    {
        return new EnumZodBuilder(enumName);
    }

    public static ObjectZodBuilder Object(IDictionary<string, IZodBuilder> content)
    {
        return new ObjectZodBuilder(content);
    }

    public static CustomZodBuilder Custom(string schema)
    {
        return new CustomZodBuilder(schema);
    }

    public static IZodBuilder Unknown()
    {
        return new CustomZodBuilder("z.unknown()");
    }
}