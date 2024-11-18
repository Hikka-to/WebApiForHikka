using System.Text;

namespace WebApiForHikka.Dtos.ZodGeneration.ValueGeneration.Utils;

public static class StringEscaper
{
    public static string Escape(string str)
    {
        var sb = new StringBuilder();
        foreach (var c in str)
            sb.Append(c switch
            {
                '\'' => @"\'",
                '\\' => @"\\",
                '\n' => @"\n",
                '\r' => @"\r",
                '\t' => @"\t",
                _ => c
            });
        return sb.ToString();
    }
}