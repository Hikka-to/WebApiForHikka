using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Characters;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Characters;

public class CharacterController(
    ICharacterService crudService,
    ISeoAdditionService seoAdditionService,
    ITagService tagService,
    IAnimeService animeService,
    IFileHelper fileHelper,
    ILinkFactory linkFactory,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : CrudControllerForModelWithSeoAddition<GetCharacterDto,
    UpdateCharacterDto,
    CreateCharacterDto,
    ICharacterService,
    Character
>(crudService, seoAdditionService, mapper, httpContextAccessor)
{
    [AllowAnonymous]
    [HttpGet("downloadImage/{imageName}")]
    public IActionResult GetImage([FromRoute] string imageName)
    {
        var file = fileHelper.GetFile(ControllerStringConstants.CharacterImagePath, imageName);

        return File(file, ControllerStringConstants.JsonImageReturnType, imageName);
    }

    [AllowAnonymous]
    public override async Task<IActionResult> GetAll(FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());

        CkeckIfColumnsAreInModel(paginationDto, errorEndPoint);
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = Mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection =
            await CrudService.GetAllAsync(filterPagination, cancellationToken);

        var models = Mapper.Map<List<GetCharacterDto>>(paginationCollection.Models);

        foreach (var item in models)
            item.ImageUrl =
                linkFactory.GetLinkForDownloadImage(Request, "downloadImage", "GetAll",
                    item.ImageUrl);


        return Ok(
            new ReturnPageDto<GetCharacterDto>
            {
                HowManyPages =
                    (int)Math.Ceiling(
                        (double)paginationCollection.Total / filterPagination.PageSize),
                Models = models,

                Total = paginationCollection.Total,
            }
        );
    }


    [HttpPost("Create")]
    public override async Task<IActionResult> Create([FromForm] CreateCharacterDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<Character>(dto);

        var seoAddition = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.CreateAsync(seoAddition, cancellationToken);

        model.SeoAddition = seoAddition;

        var path =
            fileHelper.UploadFileImage(dto.Image, ControllerStringConstants.CharacterImagePath);
        model.ImagePath = path;

        model.Tags = (await tagService.GetAllModelsByIdsAsync(dto.Tags, cancellationToken))!;

        model.Animes = (await animeService.GetAllModelsByIdsAsync(dto.Tags, cancellationToken))!;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    [HttpPut("Update")]
    public override async Task<IActionResult> Put([FromForm] UpdateCharacterDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateWithSeoAddtionEndPoint(
            new ThingsToValidateWithSeoAdditionForUpdate
            {
                UpdateDto = dto,
                IdForSeoAddition = dto.SeoAddition.Id
            });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<Character>(dto);

        var seoAdditionModel = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);

        model.SeoAddition =
            (await SeoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken))!;

        var character = await CrudService.GetAsync(dto.Id, cancellationToken);


        fileHelper.OverrideFileImage(dto.Image, character!.ImagePath);
        model.ImagePath = character.ImagePath;

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }


    [HttpGet("{id:Guid}")]
    public override async Task<IActionResult> Get([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<GetCharacterDto>(await CrudService.GetAsync(id, cancellationToken));

        if (model is null)
            return NotFound();


        model.ImageUrl =
            linkFactory.GetLinkForDownloadImage(Request, "downloadImage", "Get", model.ImageUrl);


        return Ok(model);
    }
}