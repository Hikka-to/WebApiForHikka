namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Generators;

public class ObjectGenerator : IValueGenerator
{
    public bool CanHandle(object? value)
    {
        return value?.GetType().IsClass ?? false;
    }

    public string Generate(object? value, int tabCount = 0)
    {
        if (!value!.GetType().IsClass) return value.ToString()!;

        var properties = value.GetType().GetProperties()
            .ToDictionary(p => p.Name, p => p.GetValue(value));

        return TypeScriptValueGenerator.Generate(properties, tabCount);
    }
}