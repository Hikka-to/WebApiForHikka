using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.WithoutSeoAddition.SearchHistories;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.HistoryQueries;

public class SearchHistoryService(ISearchHistoryRepository repository)
    : CrudService<SearchHistory, ISearchHistoryRepository>(repository), ISearchHistoryService;
