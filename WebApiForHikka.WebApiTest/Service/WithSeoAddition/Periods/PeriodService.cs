using WebApiForHikka.Application.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Periods;

public class PeriodServiceTest : SharedServiceTestWithSeoAddition<Period, PeriodService>
{
    protected override Period GetSample() => GetPeriodModels.GetSample();
    protected override Period GetSampleForUpdate() => GetPeriodModels.GetSampleForUpdate();

    protected override PeriodService GetService(HikkaDbContext hikkaDbContext)
    {
        PeriodRepository repository = new(hikkaDbContext);

        return new PeriodService(repository);
    }
}