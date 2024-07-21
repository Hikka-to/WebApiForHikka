using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Languages;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Languages;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class LanguageController(
    ILanguageService crudService,
    ISeoAdditionService seoAdditionService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetLanguageDto,
        UpdateLanguageDto,
        CreateLanguageDto,
        ILanguageService,
        Language
    >(crudService, seoAdditionService, mapper, httpContextAccessor);