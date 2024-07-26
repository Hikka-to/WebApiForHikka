using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.SearchHistories;

public class SearchHistoryService(ISearchHistoryRepository repository)
    : CrudService<SearchHistory, ISearchHistoryRepository>(repository), ISearchHistoryService;