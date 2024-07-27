using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class TagController(
    ITagService crudService,
    ISeoAdditionService seoAdditionService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<GetTagDto,
        UpdateTagDto,
        CreateTagDto,
        ITagService,
        Tag
    >(crudService, seoAdditionService, mapper, httpContextAccessor)
{
    [HttpPost("Create")]
    public override async Task<IActionResult> Create([FromBody] CreateTagDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = Mapper.Map<Tag>(dto);

        var seoAddition = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.CreateAsync(seoAddition, cancellationToken);

        model.SeoAddition = seoAddition;

        if (dto.ParentTagId != null)
            model.ParentTag = await CrudRelationService.GetAsync((Guid)dto.ParentTagId, cancellationToken);

        var createdId = await CrudRelationService.CreateAsync(model, cancellationToken);


        return Ok(new CreateResponseDto { Id = createdId });
    }


    [HttpPut("Update")]
    public override async Task<IActionResult> Put([FromBody] UpdateTagDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateWithSeoAddtionEndPoint(new ThingsToValidateWithSeoAdditionForUpdate
        {
            UpdateDto = dto,
            IdForSeoAddition = dto.SeoAddition.Id
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = Mapper.Map<Tag>(dto);
        var seoAdditionModel = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);

        model.SeoAddition = (await SeoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken))!;

        if (dto.ParentTagId != null)
            model.ParentTag = await CrudRelationService.GetAsync((Guid)dto.ParentTagId, cancellationToken);

        await CrudRelationService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}