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
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
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


        var model = _mapper.Map<Tag>(dto);

        var seoAddition = _mapper.Map<SeoAddition>(dto.SeoAddition);
        await _seoAdditionService.CreateAsync(seoAddition, cancellationToken);

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


        var model = _mapper.Map<Tag>(dto);
        var seoAdditionModel = _mapper.Map<SeoAddition>(dto.SeoAddition);
        await _seoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);

        model.SeoAddition = (await _seoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken))!;

        if (dto.ParentTagId != null)
            model.ParentTag = await CrudRelationService.GetAsync((Guid)dto.ParentTagId, cancellationToken);

        await CrudRelationService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
    
    [HttpPost("GetAllGenres")]
    public async Task<IActionResult> GetAllGenres([FromBody] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var filterPagination = _mapper.Map<FilterPagination>(paginationDto);
        var paginatedGenres = await CrudRelationService.GetAllAsync(filterPagination, cancellationToken);

        var models = _mapper.Map<List<GetTagDto>>(paginatedGenres.Models);
        return Ok(new ReturnPageDto<GetTagDto>
        {
            HowManyPages = (int)Math.Ceiling((double)paginatedGenres.Total / filterPagination.PageSize),
            Models = models
        });
    }
    
    [HttpPost("GetAllTagsForCharacters")]
    public async Task<IActionResult> GetAllTagsForCharacters([FromBody] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var filterPagination = _mapper.Map<FilterPagination>(paginationDto);
        
        var paginatedCharacterTags = await crudService.GetAllTagForCharactersAsync(filterPagination, cancellationToken);

        var models = _mapper.Map<List<GetTagDto>>(paginatedCharacterTags.Models);
        return Ok(new ReturnPageDto<GetTagDto>
        {
            HowManyPages = (int)Math.Ceiling((double)paginatedCharacterTags.Total / filterPagination.PageSize),
            Models = models
        });
    }
    
}