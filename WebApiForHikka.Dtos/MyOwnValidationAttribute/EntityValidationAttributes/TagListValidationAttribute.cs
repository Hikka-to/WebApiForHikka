using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Controllers;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class TagListValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success!;

        List<Guid> listOfIds = value as List<Guid>;
        var service = validationContext.GetRequiredService<ITagService>();


        foreach (var item in listOfIds)
        {
            var tag = service.Get(item);

            if (tag == null) return new ValidationResult($"{item} tag with this id doesn't exist");

        }


        return ValidationResult.Success!;
    }
}
