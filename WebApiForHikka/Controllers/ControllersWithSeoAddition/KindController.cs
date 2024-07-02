using AutoMapper;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Constants.Models.Kinds;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Kinds;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class KindController
    (IKindService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetKindDto,
        UpdateKindDto,
        CreateKindDto,
        IKindService,
        Kind
    >(crudService, seoAdditionService, mapper, httpContextAccessor);