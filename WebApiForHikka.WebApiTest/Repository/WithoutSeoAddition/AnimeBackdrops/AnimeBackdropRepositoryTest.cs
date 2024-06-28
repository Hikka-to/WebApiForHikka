using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Repository.WithSeoAddition.Animes;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropRepositoryTest : SharedRepositoryTest<AnimeBackdrop, AnimeBackdropRepository>
{
    protected override AnimeBackdropRepository GetRepository(HikkaDbContext hikkaDbContext) => new(hikkaDbContext);

    protected override AnimeBackdrop GetSample() => new()
    {
        Anime = new AnimeRepositoryTest().Anime,
        Path = "Test",
        Width = 1,
        Height = 1,
        Colors = [1, 2, 3],
    };

    protected override AnimeBackdrop GetSampleForUpdate() => new()
    {
        Anime = new AnimeRepositoryTest().AnimeForUpdate,
        Path = "Test1",
        Width = 2,
        Height = 2,
        Colors = [4, 5, 6],
    };
}