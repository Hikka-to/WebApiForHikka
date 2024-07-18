using WebApiForHikka.Application.Relation.Relateds;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.Test.Shared.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.Relateds;

public class RelatedRelationServiceTest : SharedRelationServiceTest<Related, RelatedRelationService, Anime, AnimeGroup>
{
    protected override Related GetSample()
    {
        return GetRelatedModels.GetSample();
    }

    protected override Related GetSampleForUpdate()
    {
        return GetRelatedModels.GetSampleForUpdate();
    }

    protected override RelatedRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new RelatedRelationService(new RelatedRelationRepository(hikkaDbContext));
    }
}