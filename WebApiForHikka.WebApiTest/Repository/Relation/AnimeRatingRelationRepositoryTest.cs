using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class AnimeRatingRelationRepositoryTest : SharedRelationRepositoryTest<
    AnimeRating, User, Anime,
    AnimeRatingRelationRepository
>
{
    protected override AnimeRating GetSample()
    {
        return GetAnimeRatingModels.GetSample();
    }

    protected override AnimeRating GetSampleForUpdate()
    {
        return GetAnimeRatingModels.GetSampleForUpdate();
    }

    protected override AnimeRatingRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new AnimeRatingRelationRepository(hikkaDbContext);
    }
}