using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Constants.Models.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class AnimeBackdropController(
    IAnimeBackdropService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IAnimeService animeService)
    : CrudController<
        GetAnimeBackdropDto,
        UpdateAnimeBackdropDto,
        CreateAnimeBackdropDto,
        IAnimeBackdropService,
        AnimeBackdrop
   >(crudService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateAnimeBackdropDto dto, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequest(new());
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        var model = _mapper.Map<AnimeBackdrop>(dto);

        model.Anime = (await animeService.GetAsync(dto.AnimeId, cancellationToken))!;

        var createdId = await _crudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto() { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateAnimeBackdropDto dto, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequestForUpdateEndPoint(new()
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        var model = _mapper.Map<AnimeBackdrop>(dto);

        model.Anime = (await animeService.GetAsync(dto.AnimeId, cancellationToken))!;

        await _crudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}
