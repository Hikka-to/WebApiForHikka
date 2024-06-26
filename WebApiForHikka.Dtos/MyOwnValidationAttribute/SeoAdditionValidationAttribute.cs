using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Application.SeoAdditions;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class SeoAdditionValidationAttribute : ValidationAttribute
{
    public SeoAdditionValidationAttribute() : base()
    {
        ErrorMessage = "SeoAddition with this id doesn't exist";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(ErrorMessage);

        Guid id = (Guid)value;
        var seoAdditionService = validationContext.GetRequiredService<ISeoAdditionService>();

        if (!seoAdditionService.CheckIfTheSeoAdditionExist(id))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success!;
    }

    public override string FormatErrorMessage(string name) =>
        ErrorMessage!;
}