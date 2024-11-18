using WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Generators;

namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration;

public static class TypeScriptValueGenerator
{
    private static readonly IValueGenerator[] Generators =
    {
        new NullGenerator(),
        new PrimitiveGenerator(),
        new ZodGenerator(),
        new DictionaryGenerator(),
        new EnumerableGenerator(),
        new ObjectGenerator()
    };

    public static string Generate(object? value, int tabCount = 0)
    {
        var generator = Generators.FirstOrDefault(g => g.CanHandle(value));
        return generator?.Generate(value, tabCount) ?? value?.ToString() ?? "null";
    }
}