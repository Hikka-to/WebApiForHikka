using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.SharedFunction.Extensions;
using WebApiForHikka.SharedFunction.Filtering;
using WebApiForHikka.WebApi.Controllers;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.SwaggerFilters;

public class ColumnSelectorOperationFilter(IServiceProvider services) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.TryGetMethodInfo(out var methodInfo) ||
            methodInfo.Name != "GetAll" ||
            methodInfo.DeclaringType == null ||
            (!methodInfo.DeclaringType.TryGetSubclassType(typeof(CrudController<,,,,>), out var crudController) &&
             methodInfo.DeclaringType != typeof(UserController))) return;

        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HikkaDbContext>();
        var modelType = crudController != null ? crudController.GetGenericArguments()[4] : typeof(User);
        var entityType = dbContext.Model.FindEntityType(modelType) ??
                         throw new InvalidOperationException($"Entity type for {modelType} not found.");

        operation.RequestBody.Description =
            $"**Available filter columns**: " +
            $"{string.Concat(FilterColumnSelector.GetColumns(entityType).Keys.Select(k => "\n- " + k))}\n\n" +
            $"**Available sort columns**: " +
            $"{string.Concat(SortColumnSelector.GetColumns(entityType).Keys.Select(k => "\n- " + k))}";
    }
}