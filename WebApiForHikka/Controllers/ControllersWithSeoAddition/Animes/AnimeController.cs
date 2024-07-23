﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Application.WithSeoAddition.Dubs;
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
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.WebApi.Helper.FileHelper;
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
    IDubService dubService,
    IFileHelper _fileHelper,
    IColorHelper _colorHelper,
    ILinkFactory _linkfactory
)
    : CrudControllerForModelWithSeoAddition<
        GetAnimeDto,
        UpdateAnimeDto,
        CreateAnimeDto,
        IAnimeService,
        Anime
    >(crudService, seoAdditionService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromForm] CreateAnimeDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<Anime>(dto);

        var seoAddition = _mapper.Map<SeoAddition>(dto.SeoAddition);
        await _seoAdditionService.CreateAsync(seoAddition, cancellationToken);

        model.SeoAddition = seoAddition;

        model.Kind = (await kindService.GetAsync(dto.KindId, cancellationToken))!;
        model.Status = (await statusService.GetAsync(dto.StatusId, cancellationToken))!;
        model.Period = (await periodService.GetAsync(dto.PeriodId, cancellationToken))!;
        model.RestrictedRating = (await restrictedRatingService.GetAsync(dto.RestrictedRatingId, cancellationToken))!;
        model.Source = (await sourceService.GetAsync(dto.SourceId, cancellationToken))!;

        var tags = new List<Tag>();

        foreach (var item in dto.Tags) tags.Add((await tagService.GetAsync(item, cancellationToken))!);

        var countries = new List<Country>();
        foreach (var item in dto.Countries) countries.Add((await countryService.GetAsync(item, cancellationToken))!);

        var dubs = new List<Dub>();
        foreach (var item in dto.Dubs) dubs.Add((await dubService.GetAsync(item, cancellationToken))!);

        var similarAnimes = new List<Anime>();
        foreach (var item in dto.SimilarAnimes)
            similarAnimes.Add((await crudService.GetAsync(item, cancellationToken))!);


        model.Tags = tags;
        model.Countries = countries;
        model.Dubs = dubs;
        model.SimilarChildAnimes = similarAnimes;

        var path = _fileHelper.UploadFileImage(dto.PosterImage, ControllerStringConstants.AnimePosterPath);

        model.PosterPath = path;

        model.PosterColors = _colorHelper.GetListOfColorsFromImage(dto.PosterImage);

        model.CreatedAt = DateTime.UtcNow;

        model.UpdatedAt = DateTime.UtcNow;

        var createdId = await CrudRelationService.CreateAsync(model, cancellationToken);


        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromForm] UpdateAnimeDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateWithSeoAddtionEndPoint(new ThingsToValidateWithSeoAdditionForUpdate
        {
            UpdateDto = dto,
            IdForSeoAddition = dto.SeoAddition.Id
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<Anime>(dto);


        var seoAdditionModel = _mapper.Map<SeoAddition>(dto.SeoAddition);
        await _seoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);


        model.SeoAddition = (await _seoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken))!;
        model.Kind = (await kindService.GetAsync(dto.KindId, cancellationToken))!;
        model.Status = (await statusService.GetAsync(dto.StatusId, cancellationToken))!;
        model.Period = (await periodService.GetAsync(dto.PeriodId, cancellationToken))!;
        model.RestrictedRating = (await restrictedRatingService.GetAsync(dto.RestrictedRatingId, cancellationToken))!;
        model.Source = (await sourceService.GetAsync(dto.SourceId, cancellationToken))!;

        var path = CrudRelationService.GetPosterPath(model.Id);

        if (path == null)
        {
            model.PosterPath = _fileHelper.UploadFileImage(dto.PosterImage, ControllerStringConstants.AnimePosterPath);
        }
        else
        {
            _fileHelper.OverrideFileImage(dto.PosterImage, path!);
            model.PosterPath = path;
        }

        model.PosterColors = _colorHelper.GetListOfColorsFromImage(dto.PosterImage);

        var tags = new List<Tag>();
        foreach (var item in dto.Tags) tags.Add((await tagService.GetAsync(item, cancellationToken))!);

        var countries = new List<Country>();
        foreach (var item in dto.Countries) countries.Add((await countryService.GetAsync(item, cancellationToken))!);

        var dubs = new List<Dub>();
        foreach (var item in dto.Dubs) dubs.Add((await dubService.GetAsync(item, cancellationToken))!);

        var similarAnimes = new List<Anime>();
        foreach (var item in dto.SimilarAnimes)
            similarAnimes.Add((await crudService.GetAsync(item, cancellationToken))!);

        model.Tags = tags;
        model.Countries = countries;
        model.Dubs = dubs;
        model.SimilarChildAnimes = similarAnimes;

        await CrudRelationService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }


    [AllowAnonymous] 
    [HttpGet("dowloadFile/{imageName}")]
    public IActionResult GetImage([FromRoute] string imageName)
    {
        var file = _fileHelper.GetFile(ControllerStringConstants.AnimePosterPath, imageName);

        return File(file, ControllerStringConstants.JsonImageReturnType, imageName);
    }


    [AllowAnonymous]
    public override async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = _mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection = await CrudRelationService.GetAllAsync(filterPagination, cancellationToken);

        var models = _mapper.Map<List<GetAnimeDto>>(paginationCollection.Models);

        foreach (var item in models)
            item.PosterPathUrl = _linkfactory.GetLinkForDowloadImage(Request, "dowloadImage", "GetAll", item.PosterPathUrl);


        return Ok(
            new ReturnPageDto<GetAnimeDto>
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / filterPagination.PageSize),
                Models = models
            }
        );
    }


    [HttpDelete("{id:Guid}")]
    public override async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = await CrudRelationService.GetAsync(id, cancellationToken);


        await CrudRelationService.DeleteAsync(model!.Id, cancellationToken);
        await _seoAdditionService.DeleteAsync(model.SeoAddition.Id, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:Guid}")]
    public override async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<GetAnimeDto>(await CrudRelationService.GetAsync(id, cancellationToken));

        if (model is null)
            return NotFound();



        model.PosterPathUrl = _linkfactory.GetLinkForDowloadImage(Request, "dowloadImage", "Get", model.PosterPathUrl );


        return Ok(model);
    }
}