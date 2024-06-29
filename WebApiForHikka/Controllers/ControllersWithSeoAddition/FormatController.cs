using AutoMapper;
using WebApiForHikka.Application.Formats;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Constants.Models.Formats;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Formats;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class FormatController
    (IFormatService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetFormatDto,
        UpdateFormatDto,
        CreateFormatDto,
        IFormatService,
        Format,
        FormatStringConstants
    >(crudService, seoAdditionService, mapper, httpContextAccessor);