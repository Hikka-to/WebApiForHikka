using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Collections;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Collections;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class CollectionController(
    ICollectionService crudService,
    ISeoAdditionService seoAdditionService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : CrudControllerForModelWithSeoAddition<
    GetCollectionDto,
    UpdateCollectionDto,
    CreateCollectionDto,
    ICollectionService,
    Collection
>(crudService, seoAdditionService, mapper, httpContextAccessor);