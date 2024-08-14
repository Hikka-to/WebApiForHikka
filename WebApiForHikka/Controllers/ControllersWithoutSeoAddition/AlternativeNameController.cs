using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class AlternativeNameController(
    IAlternativeNameService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IAnimeService animeService)
    : CrudController<
        GetAlternativeNameDto,
        UpdateAlternativeNameDto,
        CreateAlternativeNameDto,
        IAlternativeNameService,
        AlternativeName
    >(crudService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateAlternativeNameDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<AlternativeName>(dto);

        model.Anime = (await animeService.GetAsync(dto.AnimeId, cancellationToken))!;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateAlternativeNameDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<AlternativeName>(dto);

        model.Anime = (await animeService.GetAsync(dto.AnimeId, cancellationToken))!;

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}