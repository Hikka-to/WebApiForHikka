using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
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
    private readonly ITagService _crudService = crudService;

    [HttpPost("Create")]
    public override async Task<IActionResult> Create([FromBody] CreateTagDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = Mapper.Map<Tag>(dto);

        var seoAddition = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.CreateAsync(seoAddition, cancellationToken);

        model.SeoAddition = seoAddition;

        if (dto.ParentTagId != null)
            model.ParentTag = await CrudService.GetAsync((Guid)dto.ParentTagId, cancellationToken);

        var createdId = await CrudService.CreateAsync(model, cancellationToken);


        return Ok(new CreateResponseDto { Id = createdId });
    }


    [HttpPut("Update")]
    public override async Task<IActionResult> Put([FromBody] UpdateTagDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateWithSeoAddtionEndPoint(
            new ThingsToValidateWithSeoAdditionForUpdate
            {
                UpdateDto = dto,
                IdForSeoAddition = dto.SeoAddition.Id
            });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = Mapper.Map<Tag>(dto);
        var seoAdditionModel = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);

        model.SeoAddition =
            (await SeoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken))!;

        if (dto.ParentTagId != null)
            model.ParentTag = await CrudService.GetAsync((Guid)dto.ParentTagId, cancellationToken);

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }

    [HttpPost("GetAllGenres")]
    public async Task<IActionResult> GetAllGenres([FromBody] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var filterPagination = Mapper.Map<FilterPagination>(paginationDto);
        var paginatedGenres = await CrudService.GetAllAsync(filterPagination, cancellationToken);

        var models = Mapper.Map<List<GetTagDto>>(paginatedGenres.Models);
        return Ok(new ReturnPageDto<GetTagDto>
        {
            HowManyPages =
                (int)Math.Ceiling((double)paginatedGenres.Total / filterPagination.PageSize),
            Models = models,

                Total = paginatedGenres.Total,
        });
    }

    [HttpPost("GetAllTagsForCharacters")]
    public async Task<IActionResult> GetAllTagsForCharacters(
        [FromBody] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var filterPagination = Mapper.Map<FilterPagination>(paginationDto);

        var paginatedCharacterTags =
            await _crudService.GetAllTagForCharactersAsync(filterPagination, cancellationToken);

        var models = Mapper.Map<List<GetTagDto>>(paginatedCharacterTags.Models);
        return Ok(new ReturnPageDto<GetTagDto>
        {
            HowManyPages =
                (int)Math.Ceiling((double)paginatedCharacterTags.Total / filterPagination.PageSize),
            Models = models,
                Total = paginatedCharacterTags.Total,
        });
    }
}