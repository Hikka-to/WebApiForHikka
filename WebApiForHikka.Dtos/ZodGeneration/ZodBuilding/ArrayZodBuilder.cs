using System.Collections;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class ArrayZodBuilder : TypeBaseZodBuilder<IEnumerable, ArrayZodBuilder>
{
    internal ArrayZodBuilder(IZodBuilder elementType) : base($"z.array({elementType.Build()})")
    {
    }

    public ArrayZodBuilder Min(int length)
    {
        return AppendChain($"min({length})");
    }

    public ArrayZodBuilder Max(int length)
    {
        return AppendChain($"max({length})");
    }

    public ArrayZodBuilder Length(int length)
    {
        return AppendChain($"length({length})");
    }

    public ArrayZodBuilder Nonempty()
    {
        return AppendChain("nonempty()");
    }
}