using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Dubs;

public class DubServiceTest : SharedServiceTestWithSeoAddition<Dub, DubService>
{
    protected override Dub GetSample()
    {
        return new Dub()
        {
            Icon = "Icon",
            Name = "Name",
            SeoAddition = GetSeoAdditionSample()
        };
    }

    protected override Dub GetSampleForUpdate()
    {
        return new Dub()
        {
            Icon = "Icon1",
            Name = "Name1",
            SeoAddition = GetSeoAdditionSampleUpdate()
        };
    }

    protected override DubService GetService(HikkaDbContext hikkaDbContext)
    {
        return new DubService(new DubRepository(hikkaDbContext));
    }
}