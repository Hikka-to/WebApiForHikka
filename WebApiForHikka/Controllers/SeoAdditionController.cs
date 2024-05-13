using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers;

public class SeoAdditionController : CrudController<GetSeoAdditionDto, UpdateSeoAdditionDto, CreateSeoAdditionDto, ISeoAdditionService, SeoAddition>
{
    public SeoAdditionController(ISeoAdditionService crudService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, mapper, httpContextAccessor)
    {
    }
}
