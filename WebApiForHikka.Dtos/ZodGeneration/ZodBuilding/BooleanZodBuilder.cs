namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class BooleanZodBuilder : TypeBaseZodBuilder<bool, BooleanZodBuilder>
{
    internal BooleanZodBuilder() : base("z.boolean()")
    {
    }

    public BooleanZodBuilder True()
    {
        return AppendChain("true()");
    }

    public BooleanZodBuilder False()
    {
        return AppendChain("false()");
    }
}