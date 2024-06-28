using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Dtos.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class EntityValidationAttribute<TModel, TCrudService> : ValidationAttribute
    where TModel : class, IModel
    where TCrudService : ICrudService<TModel>
{
    protected static readonly string _modelName = typeof(TModel).Name;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success!;

        Guid id = (Guid)value;
        var service = validationContext.GetRequiredService<TCrudService>();
        TModel? tag = service.Get(id);

        if (tag == null)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success!;
    }

    public override string FormatErrorMessage(string name) =>
        $"{_modelName} with this id doesn't exist";
}
