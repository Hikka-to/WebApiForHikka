using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class SimilarRelationRepositoryTest : SharedRelationRepositoryTest<
    Similar,
    Anime,
    Anime,
    SimilarRelationRepository
>
{
    protected override Similar GetSample()
    {
        return GetSimilarModels.GetSample();
    }

    protected override Similar GetSampleForUpdate()
    {
        return GetSimilarModels.GetSampleForUpdate();
    }

    protected override SimilarRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new SimilarRelationRepository(hikkaDbContext);
    }
}