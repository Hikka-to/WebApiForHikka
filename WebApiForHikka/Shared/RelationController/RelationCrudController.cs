using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Shared.RelationController;


[Authorize(Policy = ControllerStringConstants.CanAccessOnlyAdmin)]
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

    protected abstract TModel CreateRelationModel(Guid firstId, Guid secondId);



    [HttpPost("{firstId:Guid}/secondModel/{secodnId:Guid}/Create")]
    public virtual async Task<IActionResult> Create([FromRoute] Guid firstId, [FromRoute] Guid secodId, CancellationToken cancellationToken)
    {
        
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var id = await relationService.CreateAsync(
            CreateRelationModel(firstId, secodId),
            cancellationToken
            );

        return Ok(id);
    }

    [HttpDelete("{firstId:Guid}/secondModel/{secodnId:Guid}/Delete")]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid firstId, [FromRoute] Guid secodId, CancellationToken cancellationToken)
    {

        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        await relationService.DeleteAsync(firstId, secodId, cancellationToken);

        return NoContent();
    }

    protected override ErrorEndPoint ValidateRequest(ThingsToValidateBase thingsToValidate)
    {
        ErrorEndPoint errorEndPoint = new();

        if (!ModelState.IsValid)
        {
            errorEndPoint.BadRequestObjectResult = BadRequest(GetAllErrorsDuringValidation());
            return errorEndPoint;
        }

        ThingsToValidateRelation thingsToValidateRelation = thingsToValidate as ThingsToValidateRelation;

        if (relationService.CheckIfModelsWithThisIdsExist(thingsToValidateRelation.FirstId, thingsToValidateRelation.SecondId))
        {
            errorEndPoint.BadRequestObjectResult = new BadRequestObjectResult($"One of these models or both with these ids don't exist firstId =  {thingsToValidateRelation.FirstId}, secondId = {thingsToValidateRelation.SecondId}  ");
            return errorEndPoint;
        }

        return errorEndPoint;
    }
    protected record ThingsToValidateRelation : ThingsToValidateBase
    {
        public required Guid FirstId { get; init; }
        public required Guid SecondId { get; init; }
    }
}
