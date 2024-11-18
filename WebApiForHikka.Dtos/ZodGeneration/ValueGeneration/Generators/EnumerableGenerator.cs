using System.Collections;
using System.Text;

namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Generators;

public class EnumerableGenerator : IValueGenerator
{
    public bool CanHandle(object? value)
    {
        return value is IEnumerable && value is not string;
    }

    public string Generate(object? value, int tabCount = 0)
    {
        var enumerable = (IEnumerable)value!;
        var array = enumerable.Cast<object?>().ToArray();
        var sb = new StringBuilder("[");

        if (array.Length == 0)
        {
            sb.Append(']');
            return sb.ToString();
        }

        if (array.Length == 1)
        {
            sb.Append(TypeScriptValueGenerator.Generate(array[0], tabCount));
            sb.Append(']');
            return sb.ToString();
        }

        sb.Append('\n');
        for (var i = 0; i < array.Length; i++)
        {
            sb.Append('\t', tabCount + 1);
            sb.Append(TypeScriptValueGenerator.Generate(array[i], tabCount + 1));
            if (i != array.Length - 1)
                sb.Append(",\n");
        }

        sb.Append($"\n{new string('\t', tabCount)}]");
        return sb.ToString();
    }
}