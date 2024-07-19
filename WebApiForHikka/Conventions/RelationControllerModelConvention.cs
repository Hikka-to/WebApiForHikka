using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApiForHikka.SharedFunction.Extensions;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.Conventions;

public class RelationControllerModelConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (!controller.ControllerType.IsClass ||
            controller.ControllerType.IsGenericType ||
            controller.ControllerType.IsAbstract ||
            !controller.ControllerType.TryGetSubclassType(
                typeof(RelationCrudController<,,,>), out var relationController))
            return;

        var firstModel = relationController.GetGenericArguments()[1];
        var secondModel = relationController.GetGenericArguments()[2];

        var firstModelName = firstModel.Name;
        var secondModelName = firstModelName == secondModel.Name ? "Second" + secondModel.Name : secondModel.Name;

        var firstIdName = $"{ToLowerFirstChar(firstModelName)}Id";
        var secondIdName = $"{ToLowerFirstChar(secondModelName)}Id";

        controller.RouteValues["firstModel"] = firstModelName;
        controller.RouteValues["secondModel"] = secondModelName;

        foreach (var action in controller.Actions)
        {
            if (action.Selectors[0].AttributeRouteModel is { Template: not null } route)
                route.Template = route.Template.Replace("firstId", firstIdName).Replace("secondId", secondIdName);

            foreach (var parameter in action.Parameters)
                parameter.BindingInfo = parameter.Name switch
                {
                    "firstId" => new BindingInfo { BinderModelName = firstIdName },
                    "secondId" => new BindingInfo { BinderModelName = secondIdName },
                    _ => parameter.BindingInfo
                };
        }
    }

    private static string ToLowerFirstChar(string str)
    {
        return char.ToLower(str[0]) + str[1..];
    }
}