using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.HistoryQueries;

public interface ISearchHistoryRepository : ICrudRepository<SearchHistory>;
