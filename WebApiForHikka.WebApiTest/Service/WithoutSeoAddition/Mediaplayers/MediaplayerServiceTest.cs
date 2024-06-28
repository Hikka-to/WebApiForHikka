using WebApiForHikka.Application.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.Mediaplayers;

public class MediaplayerServiceTest : SharedServiceTest<Mediaplayer, MediaplayerService>
{
    protected override Mediaplayer GetSample()
    {
        return new Mediaplayer()
        {
            Icon = "Icon",
            Name = "Name",
        };
    }

    protected override Mediaplayer GetSampleForUpdate()
    {
        return new Mediaplayer()
        {
            Icon = "Icon1",
            Name = "Name1",
        };
    }

    protected override MediaplayerService GetService(HikkaDbContext hikkaDbContext)
    {
        return new MediaplayerService(new MediaplayerRepository(hikkaDbContext));
    }
}
