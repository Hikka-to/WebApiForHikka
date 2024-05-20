using WebApiForHikka.Application.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Periods;

public class PeriodServiceTest : SharedServiceTestWithSeoAddition<Period, PeriodService>
{
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

    protected override PeriodService GetService(HikkaDbContext hikkaDbContext)
    {
        PeriodRepository repository = new(hikkaDbContext);

        return new PeriodService(repository);
    }
}