using System.Collections;
using System.Text;
using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Generators;

public class DictionaryGenerator : IValueGenerator
{
    public bool CanHandle(object? value)
    {
        return value is IDictionary;
    }

    public string Generate(object? value, int tabCount = 0)
    {
        var dict = (IDictionary)value!;

        if (dict.GetType().TryGetSubclassType(typeof(IDictionary<,>), out var type))
            return type.GenericTypeArguments[0] == typeof(string)
                ? GenerateRecord(dict, tabCount)
                : GenerateMap(dict, tabCount);

        return GenerateRecord(dict, tabCount);
    }

    private string GenerateMap(IDictionary dict, int tabCount)
    {
        var sb = new StringBuilder("new Map(");
        var keys = dict.Keys.Cast<object>().ToArray();

        if (dict.Count == 0)
        {
            sb.Append(')');
            return sb.ToString();
        }

        if (dict.Count == 1)
        {
            var key = keys[0];
            sb.Append(
                $"[ [{GenerateKey(key, tabCount)}, {TypeScriptValueGenerator.Generate(dict[key], tabCount)}] ])");
            return sb.ToString();
        }

        sb.Append('[');
        for (var i = 0; i < dict.Count; i++)
        {
            var key = keys[i];
            sb.Append('\t', tabCount + 1);
            sb.Append(
                $"[{GenerateKey(key, tabCount)}, {TypeScriptValueGenerator.Generate(dict[key], tabCount)}]");
            if (i != dict.Count - 1)
                sb.Append(", ");
        }

        sb.Append($"\n{new string('\t', tabCount)}])");
        return sb.ToString();
    }

    private string GenerateRecord(IDictionary dict, int tabCount)
    {
        var sb = new StringBuilder("{");
        var keys = dict.Keys.Cast<object>().ToArray();

        if (dict.Count == 0)
        {
            sb.Append('}');
            return sb.ToString();
        }

        if (dict.Count == 1)
        {
            var key = keys[0];
            sb.Append(
                $" {GenerateKey(key, tabCount + 1)}: {TypeScriptValueGenerator.Generate(dict[key], tabCount + 1)} ");
            sb.Append('}');
            return sb.ToString();
        }

        sb.Append('\n');
        for (var i = 0; i < dict.Count; i++)
        {
            var key = keys[i];
            sb.Append('\t', tabCount + 1);
            sb.Append(
                $"{GenerateKey(key, tabCount + 1)}: {TypeScriptValueGenerator.Generate(dict[key], tabCount + 1)}");
            if (i != dict.Count - 1)
                sb.Append(",\n");
        }

        sb.Append($"\n{new string('\t', tabCount)}}}");
        return sb.ToString();
    }

    private string GenerateKey(object key, int tabCount)
    {
        return key is string str
            ? TypeScriptValueGenerator.Generate(str.ToCamelCase(), tabCount)
            : TypeScriptValueGenerator.Generate(key, tabCount);
    }
}