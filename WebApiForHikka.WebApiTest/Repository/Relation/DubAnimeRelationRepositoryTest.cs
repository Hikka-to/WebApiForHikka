using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.Test.Shared.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class DubAnimeRelationRepositoryTest : SharedRelationRepositoryTest<
    DubAnime, Dub, Anime,
    DubAnimeRelationRepository
>
{
    protected override DubAnime GetSample()
    {
        return GetDubAnimeModels.GetSample();
    }

    protected override DubAnime GetSampleForUpdate()
    {
        return GetDubAnimeModels.GetSampleForUpdate();
    }

    protected override DubAnimeRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new DubAnimeRelationRepository(hikkaDbContext);
    }
}