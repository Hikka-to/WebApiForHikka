using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.SharedFunction.Extensions;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.SwaggerFilters;

public class CrudControllerResponseTypesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.TryGetMethodInfo(out var methodInfo) ||
            methodInfo.DeclaringType == null ||
            !methodInfo.DeclaringType.TryGetSubclassType(typeof(CrudController<,,,,>), out var crudController)) return;

        var getDtoType = crudController.GetGenericArguments()[0];

        var responseAttributes = methodInfo.GetCustomAttributes<SwaggerResponseAttribute>().ToArray();

        foreach (var (statusCode, _) in operation.Responses.ToDictionary())
        {
            if (responseAttributes.Any(a => a.StatusCode == int.Parse(statusCode))) continue;
            operation.Responses.Remove(statusCode);
        }

        switch (methodInfo.Name)
        {
            case "Create":
                Response("200", "Created", typeof(CreateResponseDto));
                Response("400", "Bad Request", typeof(string));
                Response("401", "Unauthorized");
                Response("422", "Model Validation Error", typeof(IDictionary<string, IEnumerable<string>>));
                break;
            case "Delete":
                Response("201", "Deleted");
                Response("401", "Unauthorized");
                Response("422", "Model Validation Error", typeof(IDictionary<string, IEnumerable<string>>));
                break;
            case "Get":
                Response("200", "Ok", getDtoType);
                Response("401", "Unauthorized");
                Response("404", "Not Found");
                Response("422", "Model Validation Error", typeof(IDictionary<string, IEnumerable<string>>));
                break;
            case "GetAll":
                Response("200", "Ok", typeof(ReturnPageDto<>).MakeGenericType(getDtoType));
                Response("401", "Unauthorized");
                Response("422", "Model Validation Error", typeof(IDictionary<string, IEnumerable<string>>));
                break;
            case "Put":
                Response("201", "Updated");
                Response("401", "Unauthorized");
                Response("404", "Not Found", typeof(string));
                Response("422", "Model Validation Error", typeof(IDictionary<string, IEnumerable<string>>));
                break;
        }

        return;

        void Response(string responseCode, string description, Type? responseType = null)
        {
            if (responseAttributes.Any(a => a.StatusCode == int.Parse(responseCode))) return;

            if (responseType == null)
                operation.Responses[responseCode] = new OpenApiResponse
                {
                    Description = description
                };
            else
                operation.Responses[responseCode] = new OpenApiResponse
                {
                    Description = description,
                    Content =
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = context.SchemaGenerator.GenerateSchema(responseType, context.SchemaRepository)
                        }
                    }
                };
        }
    }
}