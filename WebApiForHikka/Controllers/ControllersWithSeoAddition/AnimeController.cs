using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.VisualBasic;
using System.Drawing;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.WebApi.Helper.FileHelper;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

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
    IFileHelper _fileHelper,
    IColorHelper _colorHelper
)
    : CrudControllerForModelWithSeoAddition<
        GetAnimeDto,
        UpdateAnimeDto,
        CreateAnimeDto,
        IAnimeService,
        Anime
    >(crudService, seoAdditionService, mapper, httpContextAccessor)
{

    protected string[] PathOfImage = ["images", "animes", "posters"];

    protected const string MimeType = "image/webp";

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

        List<Tag> tags = new List<Tag>();

        foreach (var item in dto.Tags)
        {
            tags.Add((await tagService.GetAsync(item, cancellationToken))!);
        }

        model.Tags = tags;

        var path = _fileHelper.UploadFileImage(dto.PosterImage, PathOfImage);

        model.PosterPath = path;

        model.PosterColors = _colorHelper.GetListOfColorsFromImage(dto.PosterImage);


        var createdId = await _crudService.CreateAsync(model, cancellationToken);



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

        var posterPath = await _crudService.GetPosterPathAsync(model.Id);

        var seoAdditionModel = _mapper.Map<SeoAddition>(dto.SeoAddition);
        await _seoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);


        model.SeoAddition = (await _seoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken))!;
        model.Kind = (await kindService.GetAsync(dto.KindId, cancellationToken))!;
        model.Status = (await statusService.GetAsync(dto.StatusId, cancellationToken))!;
        model.Period = (await periodService.GetAsync(dto.PeriodId, cancellationToken))!;
        model.RestrictedRating = (await restrictedRatingService.GetAsync(dto.RestrictedRatingId, cancellationToken))!;
        model.Source = (await sourceService.GetAsync(dto.SourceId, cancellationToken))!;

        string? path = _crudService.GetPosterPath(model.Id);

        if (path != null)
        {
            _fileHelper.DeleteFile(path);
        }

        _fileHelper.OverrideFileImage(dto.PosterImage, path!);

        model.PosterPath = path!;

        model.PosterColors = _colorHelper.GetListOfColorsFromImage(dto.PosterImage);

        List<Tag> tags = new List<Tag>();

        foreach (var item in dto.Tags)
        {
            tags.Add((await tagService.GetAsync(item, cancellationToken))!);
        }

        model.Tags = tags;

        await _crudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }


    [AllowAnonymous]
    [HttpGet("dowloadFile/{imageName}")]
    public IActionResult GetImage([FromRoute] string imageName)
    {

        byte[] file = _fileHelper.GetFile(PathOfImage, imageName);

        return File(file, MimeType, imageName);


    }



    [AllowAnonymous]
    public override async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
           new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = _mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection = await _crudService.GetAllAsync(filterPagination, cancellationToken);

        var models = _mapper.Map<List<GetAnimeDto>>(paginationCollection.Models);

        foreach (var item in models)
        {
            item.PosterPathUrl = $"{Request.Scheme}://{Request.Host.Value}" + Request.Path.Value.Substring(0, Request.Path.Value.IndexOf("GetAll")) + "dowloadFile/" + item.PosterPathUrl.Split('\\').Last();
        }



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

        var model = await _crudService.GetAsync(id, cancellationToken);


        await _crudService.DeleteAsync(model!.Id, cancellationToken);
        await _seoAdditionService.DeleteAsync(model.SeoAddition.Id, cancellationToken);
        _fileHelper.DeleteFile(model.PosterPath);
        return NoContent();
    }

    [HttpGet("{id:Guid}")]
    public override async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<GetAnimeDto>(await _crudService.GetAsync(id, cancellationToken));

        model.PosterPathUrl = $"{Request.Scheme}://{Request.Host.Value}" + Request.Path.Value.Substring(0, Request.Path.Value.IndexOf("Get")) + "dowloadFile/" + model.PosterPathUrl.Split('\\').Last();

        if (model is null)
            return NotFound();

        return Ok(model);
    }



}