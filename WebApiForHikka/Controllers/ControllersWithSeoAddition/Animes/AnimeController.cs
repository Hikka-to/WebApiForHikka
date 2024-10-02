using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Characters;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Application.WithSeoAddition.Kinds;
using WebApiForHikka.Application.WithSeoAddition.Periods;
using WebApiForHikka.Application.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.Application.WithSeoAddition.Sources;
using WebApiForHikka.Application.WithSeoAddition.Statuses;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;

public class AnimeController(
    IAnimeService crudService,
    ISeoAdditionService seoAdditionService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IKindService kindService,
    IStatusService statusService,
    IPeriodService periodService,
    IRestrictedRatingService restrictedRatingService,
    ISourceService sourceService,
    ITagService tagService,
    ICountryService countryService,
    ICharacterService characterService,
    IDubService dubService,
    IAnimeBackdropService animeBackdropService,
    IFileHelper fileHelper,
    IColorHelper colorHelper,
    ILinkFactory linkFactory
)
    : CrudControllerForModelWithSeoAddition<
        GetAnimeDto,
        UpdateAnimeDto,
        CreateAnimeDto,
        IAnimeService,
        Anime
    >(crudService, seoAdditionService, mapper, httpContextAccessor)
{
    private readonly IAnimeService _crudService = crudService;

    public override async Task<IActionResult> Create([FromForm] CreateAnimeDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<Anime>(dto);

        var seoAddition = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.CreateAsync(seoAddition, cancellationToken);

        model.SeoAddition = seoAddition;

        model.Kind = (await kindService.GetAsync(dto.KindId, cancellationToken))!;
        model.Status = (await statusService.GetAsync(dto.StatusId, cancellationToken))!;
        model.Period = (await periodService.GetAsync(dto.PeriodId, cancellationToken))!;
        model.RestrictedRating =
            (await restrictedRatingService.GetAsync(dto.RestrictedRatingId, cancellationToken))!;
        model.Source = (await sourceService.GetAsync(dto.SourceId, cancellationToken))!;

        model.Tags = (await tagService.GetAllModelsByIdsAsync(dto.Tags, cancellationToken))!;
        model.Countries =
            (await countryService.GetAllModelsByIdsAsync(dto.Countries, cancellationToken))!;
        model.Dubs = (await dubService.GetAllModelsByIdsAsync(dto.Dubs, cancellationToken))!;
        model.Characters =
            (await characterService.GetAllModelsByIdsAsync(dto.Characters, cancellationToken))!;
        model.SimilarChildAnimes = (await CrudService.GetAllModelsByIdsAsync(dto.SimilarAnimes ??
            [], cancellationToken))!;

        var path =
            fileHelper.UploadFileImage(dto.PosterImage, ControllerStringConstants.AnimePosterPath);

        model.PosterPath = path;

        model.PosterColors = colorHelper.GetListOfColorsFromImage(dto.PosterImage);

        model.CreatedAt = DateTime.UtcNow;

        model.UpdatedAt = DateTime.UtcNow;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);


        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromForm] UpdateAnimeDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateWithSeoAddtionEndPoint(
            new ThingsToValidateWithSeoAdditionForUpdate
            {
                UpdateDto = dto,
                IdForSeoAddition = dto.SeoAddition.Id
            });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<Anime>(dto);


        var seoAdditionModel = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);


        model.SeoAddition =
            (await SeoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken))!;
        model.Kind = (await kindService.GetAsync(dto.KindId, cancellationToken))!;
        model.Status = (await statusService.GetAsync(dto.StatusId, cancellationToken))!;
        model.Period = (await periodService.GetAsync(dto.PeriodId, cancellationToken))!;
        model.RestrictedRating =
            (await restrictedRatingService.GetAsync(dto.RestrictedRatingId, cancellationToken))!;
        model.Source = (await sourceService.GetAsync(dto.SourceId, cancellationToken))!;

        var path = CrudService.GetPosterPath(model.Id);

        if (path == null)
        {
            model.PosterPath = fileHelper.UploadFileImage(dto.PosterImage,
                ControllerStringConstants.AnimePosterPath);
        }
        else
        {
            fileHelper.OverrideFileImage(dto.PosterImage, path);
            model.PosterPath = path;
        }

        model.PosterColors = colorHelper.GetListOfColorsFromImage(dto.PosterImage);


        model.Tags = (await tagService.GetAllModelsByIdsAsync(dto.Tags, cancellationToken))!;
        model.Characters =
            (await characterService.GetAllModelsByIdsAsync(dto.Charaters, cancellationToken))!;
        model.Countries =
            (await countryService.GetAllModelsByIdsAsync(dto.Countries, cancellationToken))!;
        model.Dubs = (await dubService.GetAllModelsByIdsAsync(dto.Dubs, cancellationToken))!;
        model.SimilarChildAnimes = (await _crudService.GetAllModelsByIdsAsync(dto.SimilarAnimes ??
            [], cancellationToken))!;

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }


    [AllowAnonymous]
    [HttpGet("downloadImage/{imageName}")]
    public IActionResult GetImage([FromRoute] string imageName)
    {
        var file = fileHelper.GetFile(ControllerStringConstants.AnimePosterPath, imageName);

        return File(file, ControllerStringConstants.JsonImageReturnType, imageName);
    }

    [AllowAnonymous]
    [HttpGet("downloadBackdrop/{backdropName}")]
    public IActionResult GetBackdrop([FromRoute] string backdropName)
    {
        var file = fileHelper.GetFile(ControllerStringConstants.AnimeBackdropPath, backdropName);

        return File(file, ControllerStringConstants.JsonImageReturnType, backdropName);
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

        var models = Mapper.Map<List<GetLightAnimeDto>>(paginationCollection.Models);

        foreach (var item in models)
            item.PosterPathUrl =
                linkFactory.GetLinkForDownloadImage(Request, "downloadImage", "GetAll",
                    item.PosterPathUrl);


        return Ok(
            new ReturnPageDto<GetLightAnimeDto>
            {
                HowManyPages =
                    (int)Math.Ceiling(
                        (double)paginationCollection.Total / filterPagination.PageSize),
                Models = models
            }
        );
    }


    [HttpDelete("{id:Guid}")]
    public override async Task<IActionResult> Delete([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = await CrudService.GetAsync(id, cancellationToken);


        await CrudService.DeleteAsync(model!.Id, cancellationToken);
        await SeoAdditionService.DeleteAsync(model.SeoAddition.Id, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:Guid}")]
    public override async Task<IActionResult> Get([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<GetAnimeDto>(await CrudService.GetAsync(id, cancellationToken));

        if (model is null)
            return NotFound();

        var backdrops = animeBackdropService.GetAllBackdropForAnime(model.Id).ToList();

        if (backdrops.Count >= 1)
            model.BackdropPathUrl = linkFactory.GetLinkForDownloadImage(Request, "downloadBackdrop",
                "Get", backdrops.First().Path);


        model.PosterPathUrl =
            linkFactory.GetLinkForDownloadImage(Request, "downloadImage", "Get",
                model.PosterPathUrl);


        return Ok(model);
    }
}