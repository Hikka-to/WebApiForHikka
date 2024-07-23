using WebApiForHikka.Application.WithSeoAddition.Periods;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Periods;

public class PeriodServiceTest : SharedServiceTestWithSeoAddition<Period, PeriodService>
{
    protected override Period GetSample()
    {
        return GetPeriodModels.GetSample();
    }

    protected override Period GetSampleForUpdate()
    {
        return GetPeriodModels.GetSampleForUpdate();
    }

    protected override PeriodService GetService(HikkaDbContext hikkaDbContext)
    {
        PeriodRepository repository = new(hikkaDbContext);

        return new PeriodService(repository);
    }
}