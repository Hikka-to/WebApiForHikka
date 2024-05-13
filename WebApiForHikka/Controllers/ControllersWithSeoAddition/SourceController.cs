using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Sources;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class SourceController : CrudControllerForModelWithSeoAddition<
    GetSourceDto,
    UpdateSourceDto,
    CreateSourceDto,
    ISourceService,
    Source
    >
{
    public SourceController(ISourceService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, seoAdditionService, mapper, httpContextAccessor)
    {
    }
}
