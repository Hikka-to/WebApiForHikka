using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.Seasons;

public class SeasonRepositoryTest : SharedRepositoryTest<
    Season,
    SeasonRepository
>
{
    protected override SeasonRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new SeasonRepository(hikkaDbContext);
    }

    protected override Season GetSample()
    {
        return GetSeasonModels.GetSample();
    }

    protected override Season GetSampleForUpdate()
    {
        return GetSeasonModels.GetSampleForUpdate();
    }
}
