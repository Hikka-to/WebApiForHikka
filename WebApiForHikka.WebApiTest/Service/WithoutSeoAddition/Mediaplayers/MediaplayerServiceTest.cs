using WebApiForHikka.Application.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.Mediaplayers;

public class MediaplayerServiceTest : SharedServiceTest<Mediaplayer, MediaplayerService>
{
    protected override Mediaplayer GetSample()
    {
        return GetMediaplayerModels.GetSample();
    }

    protected override Mediaplayer GetSampleForUpdate()
    {
        return GetMediaplayerModels.GetSampleForUpdate();
    }

    protected override MediaplayerService GetService(HikkaDbContext hikkaDbContext)
    {
        return new MediaplayerService(new MediaplayerRepository(hikkaDbContext));
    }
}