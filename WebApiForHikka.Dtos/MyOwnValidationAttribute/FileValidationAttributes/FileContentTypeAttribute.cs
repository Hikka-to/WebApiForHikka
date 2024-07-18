using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FileContentTypeAttribute : ValidationAttribute
{
    public FileContentTypeAttribute(string contentType)
    {
        ErrorMessage = "File type must be {0}";

        ContentType = contentType;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public string ContentType { get; }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        if (value is not IFormFile file)
            throw new ArgumentException("The property must be an IFormFile", nameof(value));

        return ContentType.Split(',', StringSplitOptions.TrimEntries).Any(type =>
        {
            var fileParts = type.Split('*');
            var fileContentType = file.ContentType;
            if (!fileContentType.StartsWith(fileParts.First()) || !fileContentType.EndsWith(fileParts.Last()))
                return false;

            var index = 0;
            foreach (var part in fileParts)
            {
                index = fileContentType.IndexOf(part, index, StringComparison.Ordinal) + part.Length;
                if (index == -1)
                    return false;
            }

            return true;
        });
    }

    public override string FormatErrorMessage(string name)
    {
        return ErrorMessage != null ? string.Format(ErrorMessage, ContentType) : string.Empty;
    }
}