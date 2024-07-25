using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.SearchHistories;

public class SearchHistoryRepositoryTest: SharedRepositoryTest<
    SearchHistory,
    SearchHistoryRepository
>
{
    protected override SearchHistoryRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new SearchHistoryRepository(hikkaDbContext);
    }

    protected override SearchHistory GetSample()
    {
        return GetSearchHistoryModels.GetSample();
    }

    protected override SearchHistory GetSampleForUpdate()
    {
        return GetSearchHistoryModels.GetSampleForUpdate();
    }
}