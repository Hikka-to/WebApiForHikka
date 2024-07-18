using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class EntityValidationAttribute<TModel> : ValidationAttribute
    where TModel : class, IModel
{
    private string ModelName { get; } = typeof(TModel).Name;

    private Type ServiceType { get; } = GetServiceType();

    private static Type GetServiceType()
    {
        var crudServiceType = typeof(ICrudService<>);
        var assembly = typeof(ICrudService<>).Assembly;
        foreach (var type in assembly.GetTypes())
            if (type is { IsInterface: true, IsGenericType: false } &&
                type.TryGetSubclassType(crudServiceType, out var serviceType) &&
                serviceType.GetGenericArguments()[0] == typeof(TModel))
                return type;
        throw new Exception($"Service for {typeof(TModel).Name} not found");
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success!;
        var service = (validationContext.GetRequiredService(ServiceType) as ICrudService<TModel>)!;

        switch (value)
        {
            case Guid idValue:
                return service.Get(idValue) == null
                    ? new ValidationResult($"{ModelName} with id {idValue} doesn't exist")
                    : ValidationResult.Success!;
            case IEnumerable<Guid> idValues:
            {
                foreach (var id in idValues)
                    if (service.Get(id) == null)
                        return new ValidationResult($"{ModelName} with id {id} doesn't exist");
                return ValidationResult.Success!;
            }
            default:
                throw new Exception("Entity validation value must be Guid or IEnumerable<Guid>");
        }
    }
}