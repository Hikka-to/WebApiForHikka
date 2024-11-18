namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Generators;

public class NullGenerator : IValueGenerator
{
    public bool CanHandle(object? value)
    {
        return value == null;
    }

    public string Generate(object? value, int tabCount = 0)
    {
        return "null";
    }
}