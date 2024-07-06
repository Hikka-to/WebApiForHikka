using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropRepositoryTest : SharedRepositoryTest<AnimeBackdrop, AnimeBackdropRepository>
{
    protected override AnimeBackdropRepository GetRepository(HikkaDbContext hikkaDbContext) => new(hikkaDbContext);

    protected override AnimeBackdrop GetSample() => GetAnimeBackdropModels.GetSample();
    protected override AnimeBackdrop GetSampleForUpdate() => GetAnimeBackdropModels.GetSample();
}