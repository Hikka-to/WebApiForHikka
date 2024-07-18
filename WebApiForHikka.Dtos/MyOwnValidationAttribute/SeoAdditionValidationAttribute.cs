using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class SeoAdditionValidationAttribute : ValidationAttribute
{
    public SeoAdditionValidationAttribute()
    {
        ErrorMessage = "Seo addition with this id doesn't exist";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(ErrorMessage);

        var id = (Guid)value;
        var seoAdditionService = validationContext.GetRequiredService<ISeoAdditionService>();

        if (!seoAdditionService.CheckIfTheSeoAdditionExist(id)) return new ValidationResult(ErrorMessage);

        return ValidationResult.Success!;
    }
}