using AutoMapper;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAdditions;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers;

public class SeoAdditionController(
    ISeoAdditionService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetSeoAdditionDto,
        UpdateSeoAdditionDto,
        CreateSeoAdditionDto,
        ISeoAdditionService,
        SeoAddition
    >(crudService, mapper, httpContextAccessor);