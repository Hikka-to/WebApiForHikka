using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

// ReSharper disable RouteTemplates.RouteTokenNotResolved

namespace WebApiForHikka.WebApi.Shared.RelationController;

[Authorize(Policy = ControllerStringConstants.CanAccessOnlyAdmin)]
[Route("api/v1")]
public abstract class RelationCrudController<TModel, TFirstModel, TSecondModel, TRelationService>(
    TRelationService relationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : MyBaseController(mapper, httpContextAccessor), IRelationCrudController
    where TModel : RelationModel<TFirstModel, TSecondModel>
    where TFirstModel : class, IModel
    where TSecondModel : class, IModel
    where TRelationService : IRelationCrudService<TModel, TFirstModel, TSecondModel>
{
    [HttpPost("[firstModel]/{firstId:Guid}/[secondModel]/{secondId:Guid}")]
    public virtual async Task<IActionResult> Create([FromRoute] Guid firstId,
        [FromRoute] Guid secondId,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var id = await relationService.CreateAsync(
            CreateRelationModel(firstId, secondId),
            cancellationToken
        );

        return Ok(id);
    }

    [HttpDelete("[firstModel]/{firstId:Guid}/[secondModel]/{secondId:Guid}")]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid firstId, [FromRoute] Guid secondId,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateRelation
            {
                FirstId = firstId,
                SecondId = secondId
            });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        await relationService.DeleteAsync(firstId, secondId, cancellationToken);

        return NoContent();
    }

    protected virtual TModel CreateRelationModel(Guid firstId, Guid secondId)
    {
        var model = Activator.CreateInstance<TModel>();
        model.FirstId = firstId;
        model.SecondId = secondId;
        return model;
    }

    protected override ErrorEndPoint ValidateRequest(ThingsToValidateBase thingsToValidate)
    {
        ErrorEndPoint errorEndPoint = new();

        if (!ModelState.IsValid)
        {
            errorEndPoint.BadRequestObjectResult = BadRequest(GetAllErrorsDuringValidation());
            return errorEndPoint;
        }

        if (thingsToValidate is not ThingsToValidateRelation thingsToValidateRelation ||
            relationService.CheckIfModelsWithThisIdsExist(thingsToValidateRelation.FirstId,
                thingsToValidateRelation.SecondId)) return errorEndPoint;

        errorEndPoint.BadRequestObjectResult = new BadRequestObjectResult(
                $"One of these models or both with these ids don't exist firstId = {thingsToValidateRelation.FirstId}, secondId = {thingsToValidateRelation.SecondId}")
            { StatusCode = 404 };
        return errorEndPoint;
    }

    protected record ThingsToValidateRelation : ThingsToValidateBase
    {
        public required Guid FirstId { get; init; }
        public required Guid SecondId { get; init; }
    }
}