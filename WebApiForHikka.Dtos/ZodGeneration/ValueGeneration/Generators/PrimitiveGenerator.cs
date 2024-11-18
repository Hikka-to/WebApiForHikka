using WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Utils;

namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Generators;

public class PrimitiveGenerator : IValueGenerator
{
    public bool CanHandle(object? value)
    {
        return value switch
        {
            string or bool or int or double or float or decimal => true,
            _ => false
        };
    }

    public string Generate(object? value, int tabCount = 0)
    {
        return value switch
        {
            string str => $"'{StringEscaper.Escape(str)}'",
            bool boolVal => boolVal.ToString().ToLower(),
            int or double or float or decimal => value.ToString()!,
            _ => throw new ArgumentException("Unsupported primitive type")
        };
    }
}