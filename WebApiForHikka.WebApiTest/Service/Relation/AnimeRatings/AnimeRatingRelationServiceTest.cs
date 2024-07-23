using WebApiForHikka.Application.Relation.AnimeRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.AnimeRatings;

public class AnimeRatingRelationServiceTest: SharedRelationServiceTest<
    AnimeRating,
    AnimeRatingRelationService,
    User,
    Anime
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

    protected override AnimeRatingRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new AnimeRatingRelationService(new AnimeRatingRelationRepository(hikkaDbContext));
    }
}