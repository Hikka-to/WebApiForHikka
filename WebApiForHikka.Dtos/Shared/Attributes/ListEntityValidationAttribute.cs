using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Dtos.Shared.Attributes;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class ListEntityValidationAttribute<IService, TModel> : ValidationAttribute
    where IService : ICrudService<TModel>
    where TModel : class, IModel
{

    protected static readonly string _modelName = typeof(TModel).Name;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success!;

        List<Guid> listOfIds = value as List<Guid>;
        var service = validationContext.GetRequiredService<IService>();

        foreach (var item in listOfIds)
        {
            var tag = service.Get(item);

            if (tag == null) return new ValidationResult($"{item} {_modelName} with this id doesn't exist");

        }

        return ValidationResult.Success!;
    }
}
