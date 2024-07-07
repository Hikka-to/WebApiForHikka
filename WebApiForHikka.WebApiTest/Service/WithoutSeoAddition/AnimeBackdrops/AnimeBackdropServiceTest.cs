using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Service.WithSeoAddition.Animes;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropServiceTest : SharedServiceTest<AnimeBackdrop, AnimeBackdropService>
{
    protected override AnimeBackdrop GetSample() => GetAnimeBackdropModels.GetSample();
    protected override AnimeBackdrop GetSampleForUpdate() => GetAnimeBackdropModels.GetSampleForUpdate();

    protected override AnimeBackdropService GetService(HikkaDbContext hikkaDbContext) => new(new AnimeBackdropRepository(hikkaDbContext));
}
