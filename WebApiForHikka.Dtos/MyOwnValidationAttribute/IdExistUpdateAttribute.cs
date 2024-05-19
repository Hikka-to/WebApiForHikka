using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class IdExistUpdateAttribute<TIService> : ValidationAttribute where TIService : ICrudService<Model>
{
    public IdExistUpdateAttribute() : base()
    {
        ErrorMessage = "Model with this id doesn't exist:";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(ErrorMessage + value);

        Guid id = (Guid)value;
        TIService? service = (TIService)validationContext.GetService(typeof(TIService));

        if (service == null) 
        {
            throw new ArgumentNullException(nameof(service) + " this service hasn't been registated");
        }


        if (service.Get(id) == null) 
        {
            return new ValidationResult(ErrorMessage + value);
        }

        return ValidationResult.Success;
    }
}
