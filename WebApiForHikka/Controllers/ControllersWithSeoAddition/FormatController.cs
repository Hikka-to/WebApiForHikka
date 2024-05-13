using AutoMapper;
using WebApiForHikka.Application.Formats;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Formats;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class FormatController : CrudControllerForModelWithSeoAddition<
    GetFormatDto,
    UpdateFormatDto,
    CreateFormatDto,
    IFormatService,
    Format
    >
{
    public FormatController(IFormatService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, seoAdditionService, mapper, httpContextAccessor)
    {
    }
}
