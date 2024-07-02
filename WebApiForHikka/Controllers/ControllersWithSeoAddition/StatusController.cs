using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Statuses;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class StatusController
    (IStatusService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<
        GetStatusDto,
        UpdateStatusDto,
        CreateStatusDto,
        IStatusService,
        Status
    >(crudService, seoAdditionService, mapper, httpContextAccessor);