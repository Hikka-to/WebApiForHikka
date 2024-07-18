using AutoMapper;
using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Seasons;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class SeasonController(
    ISeasonService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetSeasonDto,
        UpdateSeasonDto,
        CreateSeasonDto,
        ISeasonService,
        Season
    >(crudService, mapper, httpContextAccessor);