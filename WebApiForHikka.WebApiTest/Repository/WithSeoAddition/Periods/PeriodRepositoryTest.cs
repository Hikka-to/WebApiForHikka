using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Periods;


public class PeriodRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Period,
    PeriodRepository
    >
{
    protected override PeriodRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Period GetSample() => GetPeriodModels.GetSample();
    protected override Period GetSampleForUpdate() => GetPeriodModels.GetSampleForUpdate();

}