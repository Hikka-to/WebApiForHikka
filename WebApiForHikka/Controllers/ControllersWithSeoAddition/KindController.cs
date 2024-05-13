using AutoMapper;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Kinds;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class KindController : CrudControllerForModelWithSeoAddition<
    GetKindDto,
    UpdateKindDto,
    CreateKindDto,
    IKindService,
    Kind
    >
{
    public KindController(IKindService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, seoAdditionService, mapper, httpContextAccessor)
    {
    }
}
