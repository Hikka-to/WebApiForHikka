using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.RelatedTypes;

public class RelatedTypeRepositoryTest : SharedRepositoryTest<
    RelatedType,
    RelatedTypeRepository
>
{
    protected override RelatedTypeRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new RelatedTypeRepository(hikkaDbContext);
    }

    protected override RelatedType GetSample()
    {
        return GetRelatedTypeModels.GetSample();
    }

    protected override RelatedType GetSampleForUpdate()
    {
        return GetRelatedTypeModels.GetSampleForUpdate();
    }
}