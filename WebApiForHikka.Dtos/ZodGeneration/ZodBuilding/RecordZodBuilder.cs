namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class RecordZodBuilder : TypeBaseZodBuilder<IDictionary<string, object>, RecordZodBuilder>
{
    internal RecordZodBuilder(IZodBuilder valueType) : base($"z.record({valueType.Build()})")
    {
    }

    public RecordZodBuilder KeySchema(string keySchema)
    {
        return AppendChain($"keyof({keySchema})");
    }

    public RecordZodBuilder MinLength(int length)
    {
        return AppendChain($"min({length})");
    }

    public RecordZodBuilder MaxLength(int length)
    {
        return AppendChain($"max({length})");
    }
}