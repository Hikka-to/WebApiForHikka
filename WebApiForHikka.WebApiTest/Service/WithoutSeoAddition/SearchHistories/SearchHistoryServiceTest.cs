using WebApiForHikka.Application.WithoutSeoAddition.SearchHistories;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.SearchHistories;

public class SearchHistoryServiceTest : SharedServiceTest<
    SearchHistory,
    SearchHistoryService
>
{
    protected override SearchHistory GetSample()
    {
        return GetSearchHistoryModels.GetSample();
    }

    protected override SearchHistory GetSampleForUpdate()
    {
        return GetSearchHistoryModels.GetSampleForUpdate();
    }

    protected override SearchHistoryService GetService(HikkaDbContext hikkaDbContext)
    {
        var rep = new SearchHistoryRepository(hikkaDbContext);

        return new SearchHistoryService(rep);
    }
}