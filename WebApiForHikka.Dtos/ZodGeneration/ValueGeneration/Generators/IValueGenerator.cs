namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Generators;

public interface IValueGenerator
{
    bool CanHandle(object? value);
    string Generate(object? value, int tabCount = 0);
}