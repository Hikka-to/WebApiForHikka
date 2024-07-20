using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class RelatedRelationRepositoryTest : SharedRelationRepositoryTest<
    Related, Anime, AnimeGroup,
    RelatedRelationRepository
>
{
    protected override Related GetSample()
    {
        return GetRelatedModels.GetSample();
    }

    protected override Related GetSampleForUpdate()
    {
        return GetRelatedModels.GetSampleForUpdate();
    }

    protected override RelatedRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new RelatedRelationRepository(hikkaDbContext);
    }
}