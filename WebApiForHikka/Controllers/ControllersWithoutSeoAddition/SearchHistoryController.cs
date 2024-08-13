using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.SearchHistories;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.SearchHistories;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class SearchHistoryController(
    ISearchHistoryService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetSearchHistoryDto,
        UpdateSearchHistoryDto,
        CreateSearchHistoryDto,
        ISearchHistoryService,
        SearchHistory
    >(crudService, mapper, httpContextAccessor);