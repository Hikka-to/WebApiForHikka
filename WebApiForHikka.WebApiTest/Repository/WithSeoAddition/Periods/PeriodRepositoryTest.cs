using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Periods;

public class PeriodRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Period,
    PeriodRepository
>
{
    protected override PeriodRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new PeriodRepository(hikkaDbContext);
    }

    protected override Period GetSample()
    {
        return GetPeriodModels.GetSample();
    }

    protected override Period GetSampleForUpdate()
    {
        return GetPeriodModels.GetSampleForUpdate();
    }
}