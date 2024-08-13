using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.Resources;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Resources;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class ResourceController(
    ResourceService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetResourceDto,
        UpdateResourceDto,
        CreateResourceDto,
        IResourceService,
        Resource
    >(crudService, mapper, httpContextAccessor);