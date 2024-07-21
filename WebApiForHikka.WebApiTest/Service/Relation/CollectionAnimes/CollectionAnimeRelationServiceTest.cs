using WebApiForHikka.Application.Relation.CollectionAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.CollectionAnimes;

public class CollectionAnimeRelationServiceTest : SharedRelationServiceTest<
    CollectionAnime,
    CollectionAnimeRelationService,
    Collection,
    Anime
>
{
    protected override CollectionAnime GetSample()
    {
        return GetCollectionAnimeModels.GetSample();
    }

    protected override CollectionAnime GetSampleForUpdate()
    {
        return GetCollectionAnimeModels.GetSampleForUpdate();
    }

    protected override CollectionAnimeRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CollectionAnimeRelationService(new CollectionAnimeRelationRepository(hikkaDbContext));
    }
}