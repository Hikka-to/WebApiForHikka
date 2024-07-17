using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class RelatedTypeController(
    IRelatedTypeService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetRelatedTypeDto,
        UpdateRelatedTypeDto,
        CreateRelatedTypeDto,
        IRelatedTypeService,
        RelatedType
    >(crudService, mapper, httpContextAccessor);