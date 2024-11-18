using WebApiForHikka.Dtos.ZodGeneration.ValueGeneration;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class ObjectZodBuilder : BaseZodBuilder<ObjectZodBuilder>
{
    internal ObjectZodBuilder(IDictionary<string, IZodBuilder> content) : base(
        $"z.object({TypeScriptValueGenerator.Generate(content)})")
    {
    }

    public ObjectZodBuilder Strict()
    {
        return AppendChain("strict()");
    }

    public ObjectZodBuilder Passthrough()
    {
        return AppendChain("passthrough()");
    }

    public ObjectZodBuilder Partial()
    {
        return AppendChain("partial()");
    }
}