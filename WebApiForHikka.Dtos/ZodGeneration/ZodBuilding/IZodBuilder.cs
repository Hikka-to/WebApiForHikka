namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public interface IZodBuilder
{
    string Build();
    IZodBuilder Nullish();
    IZodBuilder Default(object value);
}