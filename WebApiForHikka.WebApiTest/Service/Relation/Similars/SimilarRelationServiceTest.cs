using WebApiForHikka.Application.Relation.Similars;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.Similars;

public class SimilarRelationServiceTest : SharedRelationServiceTest<
    Similar,
    SimilarRelationService,
    Anime,
    Anime
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

    protected override SimilarRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new SimilarRelationService(new SimilarRelationRepository(hikkaDbContext));
    }
}