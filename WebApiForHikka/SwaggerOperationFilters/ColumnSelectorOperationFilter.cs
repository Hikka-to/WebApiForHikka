using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.SwaggerOperationFilters;

public class ColumnSelectorOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.TryGetMethodInfo(out var methodInfo) &&
            methodInfo.Name == "GetAll" &&
            methodInfo.DeclaringType != null &&
            methodInfo.DeclaringType.IsGenericType &&
            methodInfo.DeclaringType.GetInterface(typeof(ICrudController<,,>).Name) is { } crudController)
        {
            var sortColumnParameter = operation.Parameters.First(p => p.Name == nameof(FilterPaginationDto.Column));
            var stringConstants = crudController.GetGenericArguments().Last().GetFields(
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.Public)
                .Where(f => f.FieldType == typeof(string)).ToList();

            var idString = new OpenApiString("Id");
            sortColumnParameter.Schema.Enum =
            [
                ..stringConstants.Select(f => new OpenApiString(f.GetValue(null)!.ToString()))
            ];
            sortColumnParameter.Schema.Default = idString;
            sortColumnParameter.Schema.Type = "string";
        }
    }
}
