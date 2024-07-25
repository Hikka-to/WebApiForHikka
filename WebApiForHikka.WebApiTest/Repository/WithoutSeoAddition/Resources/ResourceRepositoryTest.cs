using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.Resources;

public class ResourceRepositoryTest : SharedRepositoryTest<
    Resource,
    ResourceRepository
>
{
    protected override ResourceRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new ResourceRepository(hikkaDbContext);
    }

    protected override Resource GetSample()
    {
        return GetResourceModels.GetSample();
    }

    protected override Resource GetSampleForUpdate()
    {
        return GetResourceModels.GetSampleForUpdate();
    }
}