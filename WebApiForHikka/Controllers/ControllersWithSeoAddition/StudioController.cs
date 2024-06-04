using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Studios;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class StudioController (IStudioService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetStudioDto,
        UpdateStudioDto,
        CreateStudioDto,
        IStudioService,
        Studio
    >(crudService, seoAdditionService, mapper, httpContextAccessor);
