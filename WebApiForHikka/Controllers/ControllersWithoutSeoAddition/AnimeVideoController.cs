using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class AnimeVideoController(
    IAnimeVideoService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IAnimeVideoKindService animeVideoKindService)
    : CrudController<
        GetAnimeVideoDto,
        UpdateAnimeVideoDto,
        CreateAnimeVideoDto,
        IAnimeVideoService,
        AnimeVideo
    >(crudService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateAnimeVideoDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<AnimeVideo>(dto);

        model.AnimeVideoKind = (await animeVideoKindService.GetAsync(dto.AnimeVideoKindId, cancellationToken))!;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateAnimeVideoDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<AnimeVideo>(dto);

        model.AnimeVideoKind = (await animeVideoKindService.GetAsync(dto.AnimeVideoKindId, cancellationToken))!;

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}