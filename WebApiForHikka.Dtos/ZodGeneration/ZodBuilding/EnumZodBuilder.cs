namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class EnumZodBuilder : TypeBaseZodBuilder<Enum, EnumZodBuilder>
{
    internal EnumZodBuilder(string enumName) : base($"z.nativeEnum({enumName})")
    {
    }
}