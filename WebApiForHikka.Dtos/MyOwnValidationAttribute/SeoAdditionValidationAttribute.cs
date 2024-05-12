using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WebApiForHikka.Application.SeoAdditions;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class SeoAdditionValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        Guid id = (Guid)value;
        ISeoAdditionService? seoAdditionService = (ISeoAdditionService)validationContext.GetService(typeof(ISeoAdditionService));

        if (seoAdditionService == null) {
            throw new Exception("SeoAddition service hasn't been registrated");
        }

        if (!seoAdditionService.CheckIfTheSeoAdditionExist(id)) 
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    public override string FormatErrorMessage(string name)
    {
        return String.Format(CultureInfo.CurrentCulture,
          "SeoAddition with this id doesn't exist", name);
    }
}
