using WebApiForHikka.Application.WithSeoAddition.Collections;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Collections;

public class CollectionServiceTest : SharedServiceTestWithSeoAddition<Collection, CollectionService>
{
    protected override Collection GetSample()
    {
        return GetCollectionModels.GetSample();
    }

    protected override Collection GetSampleForUpdate()
    {
        return GetCollectionModels.GetSampleForUpdate();
    }

    protected override CollectionService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CollectionService(new CollectionRepository(hikkaDbContext));
    }
}