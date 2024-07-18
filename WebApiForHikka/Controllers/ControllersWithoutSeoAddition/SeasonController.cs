using AutoMapper;
using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.Relation.Seasons;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class SeasonController(
    ISeasonRelationService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetSeasonDto,
        UpdateSeasonDto,
        CreateSeasonDto,
        ISeasonRelationService,
        Season
    >(crudService, mapper, httpContextAccessor);