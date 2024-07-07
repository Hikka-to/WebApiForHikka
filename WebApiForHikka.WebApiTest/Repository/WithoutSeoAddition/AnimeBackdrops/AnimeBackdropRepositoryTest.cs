using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropRepositoryTest : SharedRepositoryTest<AnimeBackdrop, AnimeBackdropRepository>
{
    protected override AnimeBackdropRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new AnimeBackdropRepository(hikkaDbContext);
    }

    protected override AnimeBackdrop GetSample()
    {
        return GetAnimeBackdropModels.GetSample();
    }

    protected override AnimeBackdrop GetSampleForUpdate()
    {
        return GetAnimeBackdropModels.GetSample();
    }
}