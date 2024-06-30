using AutoMapper;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Constants.Models.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Periods;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class PeriodController
    (IPeriodService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetPeriodDto,
        UpdatePeriodDto,
        CreatePeriodDto,
        IPeriodService,
        Period,
        PeriodStringConstants
    >(crudService, seoAdditionService, mapper, httpContextAccessor);