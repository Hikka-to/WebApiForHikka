using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class CollectionAnimeRelationRepositoryTest : SharedRelationRepositoryTest<
    CollectionAnime, Collection, Anime,
    CollectionAnimeRelationRepository
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

    protected override CollectionAnimeRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CollectionAnimeRelationRepository(hikkaDbContext);
    }
}