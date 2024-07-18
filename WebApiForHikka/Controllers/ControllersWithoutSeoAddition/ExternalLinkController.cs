using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class ExternalLinkController(
    IExternalLinkService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IAnimeService animeService)
    : CrudController<
        GetExternalLinkDto,
        UpdateExternalLinkDto,
        CreateExternalLinkDto,
        IExternalLinkService,
        ExternalLink
    >(crudService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateExternalLinkDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<ExternalLink>(dto);

        model.Anime = (await animeService.GetAsync(dto.AnimeId, cancellationToken))!;

        var createdId = await _crudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateExternalLinkDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<ExternalLink>(dto);

        model.Anime = (await animeService.GetAsync(dto.AnimeId, cancellationToken))!;

        await _crudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}