using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class AnimeVideoController(IAnimeVideoService crudService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IAnimeVideoKindService animeVideoKindService)
    : CrudController<
        GetAnimeVideoDto,
        UpdateAnimeVideoDto,
        CreateAnimeVideoDto,
        IAnimeVideoService,
        AnimeVideo
    >(crudService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateAnimeVideoDto dto, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequest(new());
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        var model = _mapper.Map<AnimeVideo>(dto);

        model.AnimeVideoKind = (await animeVideoKindService.GetAsync(dto.AnimeVideoKindId, cancellationToken))!;

        var createdId = await _crudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto() { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateAnimeVideoDto dto, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequestForUpdateEndPoint(new()
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        var model = _mapper.Map<AnimeVideo>(dto);

        model.AnimeVideoKind = (await animeVideoKindService.GetAsync(dto.AnimeVideoKindId, cancellationToken))!;

        await _crudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}
