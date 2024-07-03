using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.WebApi.Controllers;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.SwaggerOperationFilters;

public class ColumnSelectorOperationFilter(IServiceProvider services) : IOperationFilter
{
    private bool TryGetCrudController(Type? type, [NotNullWhen(true)] out Type? crudController)
    {
        crudController = null;

        while (type != null)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(CrudController<,,,,>))
            {
                crudController = type;
                return true;
            }

            type = type.BaseType;
        }

        return false;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.TryGetMethodInfo(out var methodInfo) &&
            methodInfo.Name == "GetAll" &&
            methodInfo.DeclaringType != null &&
            (TryGetCrudController(methodInfo.DeclaringType, out var crudController) ||
             methodInfo.DeclaringType == typeof(UserController)))
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HikkaDbContext>();
            var modelType = crudController != null ? crudController.GetGenericArguments()[4] : typeof(User);
            var sortColumnParameter = operation.Parameters.First(p => p.Name == nameof(FilterPaginationDto.Column));
            var entityType = dbContext.Model.FindEntityType(modelType) ??
                throw new InvalidOperationException($"Entity type for {modelType} not found.");

            var idString = new OpenApiString("Id");
            sortColumnParameter.Schema.Enum =
            [
                ..entityType.GetProperties().Select(p => new OpenApiString(p.Name)),
                ..entityType.GetNavigations().Select(n => new OpenApiString(n.Name)),
                ..entityType.GetSkipNavigations().Select(n => new OpenApiString(n.Name))
            ];
            sortColumnParameter.Schema.Default = idString;
            sortColumnParameter.Schema.Type = "string";
        }
    }
}
