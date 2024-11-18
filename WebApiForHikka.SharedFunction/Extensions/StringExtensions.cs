using System.Text.RegularExpressions;

namespace WebApiForHikka.SharedFunction.Extensions;

public static partial class StringExtensions
{
    private static string[] GetSplittedByWords(string text)
    {
        if (string.IsNullOrEmpty(text))
            return [];

        var processed = text.Replace('_', ' ').Replace('-', ' ');

        processed = MyRegex().Replace(processed, "$1 $2");

        processed = MyRegex1().Replace(processed, "$1 $2");

        return processed.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word.ToLower())
            .ToArray();
    }

    public static string ToCamelCase(this string text)
    {
        var words = GetSplittedByWords(text);
        if (words.Length == 0)
            return string.Empty;

        return words[0].ToLower() +
               string.Concat(words.Skip(1).Select(word =>
                   char.ToUpper(word[0]) + word[1..]));
    }

    public static string ToKebabCase(this string text)
    {
        return string.Join("-", GetSplittedByWords(text));
    }

    public static string ToSnakeCase(this string text)
    {
        return string.Join("_", GetSplittedByWords(text));
    }

    public static string ToPascalCase(this string text)
    {
        return string.Concat(GetSplittedByWords(text)
            .Select(word => char.ToUpper(word[0]) + word[1..]));
    }

    public static string ToTitleCase(this string text)
    {
        return string.Join(" ", GetSplittedByWords(text)
            .Select(word => char.ToUpper(word[0]) + word[1..]));
    }

    [GeneratedRegex(@"([a-z0-9])([A-Z])")]
    private static partial Regex MyRegex();

    [GeneratedRegex(@"([A-Z])([A-Z][a-z])")]
    private static partial Regex MyRegex1();
}