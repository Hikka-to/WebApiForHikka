using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Relation.Relateds;
using WebApiForHikka.Application.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.Relateds;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.Relation;

public class RelatedController(
    IRelatedRelationService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IRelatedTypeService relatedTypeService)
    : CrudController<
        GetRelatedDto,
        UpdateRelatedDto,
        CreateRelatedDto,
        IRelatedRelationService,
        Related
    >(crudRelationService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateRelatedDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<Related>(dto);

        model.RelatedType = (await relatedTypeService.GetAsync(dto.RelatedTypeId, cancellationToken))!;

        var createdId = await CrudRelationService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateRelatedDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<Related>(dto);

        model.RelatedType = (await relatedTypeService.GetAsync(dto.RelatedTypeId, cancellationToken))!;

        await CrudRelationService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}