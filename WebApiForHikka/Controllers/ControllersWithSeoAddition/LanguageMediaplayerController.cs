using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Application.WithSeoAddition.Formats;
using WebApiForHikka.Application.WithSeoAddition.LanguageMediaplayers;
using WebApiForHikka.Application.WithSeoAddition.Languages;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.LanguageMediaplayers;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class LanguageMediaplayerController(
    ILanguageMediaplayerService crudService,
    ISeoAdditionService seoAdditionService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IMediaplayerService mediaplayerService,
    ILanguageService languageService,
    IEpisodeService episodeService,
    IFormatService formatService)
    : CrudControllerForModelWithSeoAddition<
        GetLanguageMediaplayerDto,
        UpdateLanguageMediaplayerDto,
        CreateLanguageMediaplayerDto,
        ILanguageMediaplayerService,
        LanguageMediaplayer
    >(crudService, seoAdditionService, mapper, httpContextAccessor)

{
    [HttpPost("Create")]
    public override async Task<IActionResult> Create([FromBody] CreateLanguageMediaplayerDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = Mapper.Map<LanguageMediaplayer>(dto);

        var seoAddition = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.CreateAsync(seoAddition, cancellationToken);

        model.SeoAddition = seoAddition;
        model.Episode = (await episodeService.GetAsync(dto.EpisodeId, cancellationToken))!;
        model.Mediaplayer = (await mediaplayerService.GetAsync(dto.MediaplayerId, cancellationToken))!;
        model.Format = (await formatService.GetAsync(dto.FormatId, cancellationToken))!;
        model.Language = (await languageService.GetAsync(dto.LanguageId, cancellationToken))!;

        var createdId = await CrudRelationService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }


    [HttpPut("Update")]
    public override async Task<IActionResult> Put([FromBody] UpdateLanguageMediaplayerDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateWithSeoAddtionEndPoint(new ThingsToValidateWithSeoAdditionForUpdate
        {
            UpdateDto = dto,
            IdForSeoAddition = dto.SeoAddition.Id
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = Mapper.Map<LanguageMediaplayer>(dto);

        var seoAddition = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.UpdateAsync(seoAddition, cancellationToken);

        model.SeoAddition = (await SeoAdditionService.GetAsync(seoAddition.Id, cancellationToken))!;

        model.Episode = (await episodeService.GetAsync(dto.EpisodeId, cancellationToken))!;
        model.Mediaplayer = (await mediaplayerService.GetAsync(dto.MediaplayerId, cancellationToken))!;
        model.Format = (await formatService.GetAsync(dto.FormatId, cancellationToken))!;
        model.Language = (await languageService.GetAsync(dto.LanguageId, cancellationToken))!;

        await CrudRelationService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}