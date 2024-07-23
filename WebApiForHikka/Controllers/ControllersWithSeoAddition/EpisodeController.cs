using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class EpisodeController(
    IEpisodeService crudService,
    ISeoAdditionService seoAdditionService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetEpisodeDto,
        UpdateEpisodeDto,
        CreateEpisodeDto,
        IEpisodeService,
        Episode
    >(crudService, seoAdditionService, mapper, httpContextAccessor);