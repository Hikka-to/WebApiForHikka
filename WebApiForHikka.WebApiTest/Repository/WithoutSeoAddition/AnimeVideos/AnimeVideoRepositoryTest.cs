using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeVideos;

public class AnimeVideoRepositoryTest : SharedRepositoryTest<AnimeVideo, AnimeVideoRepository>
{
    protected override AnimeVideo GetSample()
    {
        return GetAnimeVideoModels.GetSample();
    }

    protected override AnimeVideo GetSampleForUpdate()
    {
        return GetAnimeVideoModels.GetSampleForUpdate();
    }

    protected override AnimeVideoRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new AnimeVideoRepository(hikkaDbContext);
    }
}