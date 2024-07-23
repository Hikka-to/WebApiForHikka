using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.Providers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Providers;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class ProviderController(
    ProviderService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
)
    : CrudController
        <GetProviderDto, UpdateProviderDto, CreateProviderDto, ProviderService, Provider>(crudService, mapper,
            httpContextAccessor);