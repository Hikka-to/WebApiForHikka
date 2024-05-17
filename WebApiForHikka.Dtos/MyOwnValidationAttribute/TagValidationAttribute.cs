using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class TagValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;

        Guid id = (Guid)value;
        ITagService? tagService = (ITagService)validationContext.GetService(typeof(ITagService));

        if (tagService == null) {
            throw new Exception("Tag service hasn't been registrated");
        }

        Tag? tag = tagService.Get(id);

        if (tag == null) 
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    public override string FormatErrorMessage(string name)
    {
        return String.Format(CultureInfo.CurrentCulture,
          "Tag with this id doesn't exist", name);
    }

}
