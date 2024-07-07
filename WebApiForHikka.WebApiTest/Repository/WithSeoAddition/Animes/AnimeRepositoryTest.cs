using FluentAssertions;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Animes;

public class AnimeRepositoryTest : SharedRepositoryTestWithSeoAddition<Anime, AnimeRepository>
{
    public Anime Anime => GetSample();

    public Anime AnimeForUpdate => GetSampleForUpdate();

    protected override AnimeRepository GetRepository(HikkaDbContext hikkaDbContext) => new(hikkaDbContext);



    protected override Anime GetSample() => GetAnimeModels.GetSample();
    protected override Anime GetSampleForUpdate() => GetAnimeModels.GetSampleForUpdate();
}
