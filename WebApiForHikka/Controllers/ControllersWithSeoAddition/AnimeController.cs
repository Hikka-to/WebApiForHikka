using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.ResponseDto;
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
    IFileHelper fileHelper
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


        var path = fileHelper.UploadFile(dto.PosterImage, ["images", "animes", "posters"]);


        var model = _mapper.Map<Anime>(dto);

        var seoAddition = _mapper.Map<SeoAddition>(dto.SeoAddition);
        await _seoAdditionService.CreateAsync(seoAddition, cancellationToken);

        model.SeoAddition = seoAddition;

        model.Kind = (await kindService.GetAsync(dto.KindId, cancellationToken))!;
        model.Status = (await statusService.GetAsync(dto.StatusId, cancellationToken))!;
        model.Period = (await periodService.GetAsync(dto.PeriodId, cancellationToken))!;
        model.RestrictedRating = (await restrictedRatingService.GetAsync(dto.RestrictedRatingId, cancellationToken))!;
        model.Source = (await sourceService.GetAsync(dto.SourceId, cancellationToken))!;
        model.PosterPath = path;

        var createdId = await _crudService.CreateAsync(model, cancellationToken);


        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateAnimeDto dto, CancellationToken cancellationToken)
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

        await _crudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}