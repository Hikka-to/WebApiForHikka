using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Collections;

public class CollectionRepositoryTest : SharedRepositoryTestWithSeoAddition<Collection, CollectionRepository>
{
    protected override CollectionRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CollectionRepository(hikkaDbContext);
    }

    protected override Collection GetSample()
    {
        return GetCollectionModels.GetSample();
    }

    protected override Collection GetSampleForUpdate()
    {
        return GetCollectionModels.GetSampleForUpdate();
    }
}