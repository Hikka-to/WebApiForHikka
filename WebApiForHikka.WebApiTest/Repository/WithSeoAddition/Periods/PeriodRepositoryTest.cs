using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Periods;


public class PeriodRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Period,
    PeriodRepository
    >
{
    protected override PeriodRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Period GetSample() => new()
    {
        Name = "test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Period GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
    };
}