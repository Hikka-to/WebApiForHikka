using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.Seasons;

public class SeasonSeviceTest : SharedServiceTest<Season, SeasonService>
{
    protected override Season GetSample()
    {
        return GetSeasonModels.GetSample();
    }

    protected override Season GetSampleForUpdate()
    {
        return GetSeasonModels.GetSampleForUpdate();
    }

    protected override SeasonService GetService(HikkaDbContext hikkaDbContext)
    {
        var repostiory = new SeasonRepository(hikkaDbContext);
        return new SeasonService(repostiory);
    }
}
