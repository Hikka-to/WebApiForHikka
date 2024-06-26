using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Service.WithSeoAddition.Animes;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropServiceTest : SharedServiceTest<AnimeBackdrop, AnimeBackdropService>
{
    protected override AnimeBackdrop GetSample() => new()
    {
        Anime = new AnimeServiceTest().Anime,
        Path = "Test",
        Width = 1,
        Height = 1,
        Colors = [1, 2, 3],
    };

    protected override AnimeBackdrop GetSampleForUpdate() => new()
    {
        Anime = new AnimeServiceTest().AnimeForUpdate,
        Path = "Test1",
        Width = 2,
        Height = 2,
        Colors = [4, 5, 6],
    };

    protected override AnimeBackdropService GetService(HikkaDbContext hikkaDbContext) => new(new AnimeBackdropRepository(hikkaDbContext));
}
