using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Status;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class StatusController : CrudControllerForModelWithSeoAddition<
    GetStatusDto,
    UpdateStatusDto,
    CreateStatusDto,
    IStatusService,
    Status
    >
{
    public StatusController(IStatusService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, seoAdditionService, mapper, httpContextAccessor)
    {
    }
}
