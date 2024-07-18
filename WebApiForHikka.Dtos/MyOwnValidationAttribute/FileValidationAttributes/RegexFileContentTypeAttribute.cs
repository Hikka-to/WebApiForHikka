using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

public class RegexFileContentTypeAttribute : ValidationAttribute
{
    public RegexFileContentTypeAttribute([StringSyntax("Regex")] string regex)
    {
        Regex = regex;
        ErrorMessage = "File type must be {0}";
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public string Regex { get; }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        if (value is not IFormFile file)
            throw new ArgumentException("The property must be an IFormFile", nameof(value));

        var regex = new Regex(Regex);
        return regex.IsMatch(file.ContentType);
    }

    public override string FormatErrorMessage(string name)
    {
        return ErrorMessage != null ? string.Format(ErrorMessage, Regex) : string.Empty;
    }
}