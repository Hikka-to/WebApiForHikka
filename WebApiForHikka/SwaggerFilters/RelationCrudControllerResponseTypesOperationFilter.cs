using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiForHikka.SharedFunction.Extensions;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.SwaggerFilters;

public class RelationCrudControllerResponseTypesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.TryGetMethodInfo(out var methodInfo) ||
            methodInfo.DeclaringType == null ||
            !methodInfo.DeclaringType.GenericIsSubclassOf(typeof(RelationCrudController<,,,>)))
            return;

        var responseAttributes = methodInfo.GetCustomAttributes<SwaggerResponseAttribute>().ToArray();

        foreach (var (statusCode, _) in operation.Responses.ToDictionary())
        {
            if (responseAttributes.Any(a => a.StatusCode == int.Parse(statusCode))) continue;
            operation.Responses.Remove(statusCode);
        }

        switch (methodInfo.Name)
        {
            case "Create":
                Response("200", "Created", typeof(int));
                Response("401", "Unauthorized");
                Response("422", "Model Validation Error", typeof(IDictionary<string, IEnumerable<string>>));
                break;
            case "Delete":
                Response("204", "Deleted");
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