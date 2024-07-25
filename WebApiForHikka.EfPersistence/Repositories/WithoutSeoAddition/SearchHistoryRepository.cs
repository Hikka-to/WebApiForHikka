using WebApiForHikka.Application.WithoutSeoAddition.HistoryQueries;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class SearchHistoryRepository(HikkaDbContext dbContext)
    : CrudRepository<SearchHistory>(dbContext), ISearchHistoryRepository;
