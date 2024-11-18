using WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Generators;

public class ZodGenerator : IValueGenerator
{
    public bool CanHandle(object? value)
    {
        return value is IZodBuilder;
    }

    public string Generate(object? value, int tabCount = 0)
    {
        return ((IZodBuilder)value!).Build();
    }
}