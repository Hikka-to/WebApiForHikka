using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Dubs;

public class DubServiceTest : SharedServiceTestWithSeoAddition<Dub, DubService>
{
    protected override Dub GetSample()
    {
        return GetDubModels.GetSample();
    }

    protected override Dub GetSampleForUpdate()
    {
        return GetDubModels.GetSampleForUpdate();
    }

    protected override DubService GetService(HikkaDbContext hikkaDbContext)
    {
        return new DubService(new DubRepository(hikkaDbContext));
    }
}