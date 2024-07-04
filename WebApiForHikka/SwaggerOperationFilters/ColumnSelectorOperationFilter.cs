using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.SharedFunction.Extensions;
using WebApiForHikka.WebApi.Controllers;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.SwaggerOperationFilters;

public class ColumnSelectorOperationFilter(IServiceProvider services) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.TryGetMethodInfo(out var methodInfo) &&
            methodInfo.Name == "GetAll" &&
            methodInfo.DeclaringType != null &&
            (methodInfo.DeclaringType.TryGetSubclassType(typeof(CrudController<,,,,>), out var crudController) ||
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
                ..entityType.GetProperties()
                    .Where(p => (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.IsPubliclyReadable() ?? false))
                    .Select(p => new OpenApiString(p.Name)),
                ..entityType.GetNavigations()
                    .Where(n => (n.FieldInfo?.IsPublic ?? false) || (n.PropertyInfo?.IsPubliclyReadable() ?? false))
                    .Select(n => new OpenApiString(n.Name)),
                ..entityType.GetSkipNavigations()
                    .Where(n => (n.FieldInfo?.IsPublic ?? false) || (n.PropertyInfo?.IsPubliclyReadable() ?? false))
                    .Select(n => new OpenApiString(n.Name))
            ];
            sortColumnParameter.Schema.Default = idString;
            sortColumnParameter.Schema.Type = "string";
        }
    }
}
