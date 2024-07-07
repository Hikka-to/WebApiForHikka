using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeVideos;

public class AnimeVideoServiceTest : SharedServiceTest<AnimeVideo, AnimeVideoService>
{
    protected override AnimeVideo GetSample()
    {
        return GetAnimeVideoModels.GetSample();
    }

    protected override AnimeVideo GetSampleForUpdate()
    {
        return GetAnimeVideoModels.GetSampleForUpdate();
    }

    protected override AnimeVideoService GetService(HikkaDbContext hikkaDbContext)
    {
        return new AnimeVideoService(new AnimeVideoRepository(hikkaDbContext));
    }
}