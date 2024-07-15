using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Countries;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class CountryController(
    ICountryService crudService,
    ISeoAdditionService seoAdditionService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetCountryDto,
        UpdateCountryDto,
        CreateCountryDto,
        ICountryService,
        Country
    >(crudService, seoAdditionService, mapper, httpContextAccessor);