using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiForHikka.Domain;
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
            (!methodInfo.DeclaringType.TryGetSubclassType(typeof(CrudController<,,,,>),
                 out var crudController) &&
             methodInfo.DeclaringType != typeof(UserController))) return;

        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HikkaDbContext>();
        var modelType = crudController != null
            ? crudController.GetGenericArguments()[4]
            : typeof(User);
        var entityType = dbContext.Model.FindEntityType(modelType) ??
                         throw new InvalidOperationException(
                             $"Entity type for {modelType} not found.");

        var filterColumns = FilterColumnSelector.GetColumns(entityType);
        var filterTypes = Enum.GetValues<FilterType>();
        var header = "\n| Column |" +
                     string.Concat(filterTypes.Select(t => $" {(int)t} |"));
        var separator = "\n| --- |" +
                        string.Concat(filterTypes.Select(_ => " :---: |"));
        var body = string.Concat(filterColumns.Select(c =>
        {
            var column = c.ToArray();
            var availableFilterTypes =
                FilterColumnSelector.GetFilterTypes(column.GetProperty().ClrType);
            return $"\n| {column.GetReadablePath()} |" +
                   string.Concat(filterTypes.Select(t =>
                       $" {(availableFilterTypes.Contains(t) ? "✅" : "❌")} |"));
        }));
        var table = header + separator + body;

        operation.RequestBody.Description =
            $"**Available filter types**: " +
            $"{string.Concat(Enum.GetValues<FilterType>().Select(t => $"\n- {t} ({(int)t})"))}\n\n" +
            $"**Available filter columns**: " +
            $"{table}\n\n" +
            $"**Available sort columns**: " +
            $"{string.Concat(SortColumnSelector.GetColumns(entityType).Select(p => "\n- " + p.GetReadablePath()))}";
    }
}