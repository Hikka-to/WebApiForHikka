using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Animes;

public class AnimeServiceTest : SharedServiceTestWithSeoAddition<Anime, AnimeService>
{
    public Anime Anime => GetSample();

    public Anime AnimeForUpdate => GetSampleForUpdate();

    protected override Anime GetSample()
    {
        return GetAnimeModels.GetSample();
    }

    protected override Anime GetSampleForUpdate()
    {
        return GetAnimeModels.GetSampleForUpdate();
    }

    protected override AnimeService GetService(HikkaDbContext hikkaDbContext)
    {
        AnimeRepository animeRepository = new(hikkaDbContext);

        return new AnimeService(animeRepository);
    }
}