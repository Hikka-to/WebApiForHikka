using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class StringZodBuilder : TypeBaseZodBuilder<string, StringZodBuilder>
{
    internal StringZodBuilder() : base("z.string()")
    {
    }

    public StringZodBuilder Min(int length)
    {
        return AppendChain($"min({length})");
    }

    public StringZodBuilder Max(int length)
    {
        return AppendChain($"max({length})");
    }

    public StringZodBuilder Email()
    {
        return AppendChain("email()");
    }

    public StringZodBuilder Url()
    {
        return AppendChain("url()");
    }

    public StringZodBuilder Uuid()
    {
        return AppendChain("uuid()");
    }

    public StringZodBuilder Regex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern,
        RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(pattern))
            throw new ArgumentException("Pattern cannot be null or empty", nameof(pattern));

        var escapedPattern = EscapeRegexPattern(pattern);
        var flags = GetTypeScriptFlags(options);

        return AppendChain($"regex(/{escapedPattern}/{flags})");
    }

    private static string EscapeRegexPattern(string pattern)
    {
        var sb = new StringBuilder(pattern.Length);
        foreach (var c in pattern)
            sb.Append(c switch
            {
                '/' => @"\/",
                _ => c
            });
        return sb.ToString();
    }

    private static string GetTypeScriptFlags(RegexOptions options)
    {
        var flags = new StringBuilder();

        if ((options & RegexOptions.IgnoreCase) != 0) flags.Append('i');
        if ((options & RegexOptions.Multiline) != 0) flags.Append('m');
        if ((options & RegexOptions.Singleline) != 0) flags.Append('s');
        if ((options & RegexOptions.Global) != 0) flags.Append('g');

        return flags.ToString();
    }
}