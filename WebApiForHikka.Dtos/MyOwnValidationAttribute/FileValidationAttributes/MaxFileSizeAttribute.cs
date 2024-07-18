using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MaxFileSizeAttribute : ValidationAttribute
{
    public MaxFileSizeAttribute(long size)
    {
        Size = size;
        ErrorMessage = "The file size must be less than {0} bytes";
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public long Size { get; }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        if (value is not IFormFile file)
            throw new ArgumentException("The property must be an IFormFile", nameof(value));

        return file.Length <= Size;
    }

    public override string FormatErrorMessage(string name)
    {
        return ErrorMessage != null ? string.Format(ErrorMessage, Size) : string.Empty;
    }
}