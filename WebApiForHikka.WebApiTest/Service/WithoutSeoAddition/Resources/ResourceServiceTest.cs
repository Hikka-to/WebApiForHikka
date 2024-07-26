using WebApiForHikka.Application.WithoutSeoAddition.Resources;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.Resources;

public class ResourceServiceTest : SharedServiceTest<
    Resource,
    ResourceService
>
{
    protected override Resource GetSample()
    {
        return GetResourceModels.GetSample();
    }

    protected override Resource GetSampleForUpdate()
    {
        return GetResourceModels.GetSampleForUpdate();
    }

    protected override ResourceService GetService(HikkaDbContext hikkaDbContext)
    {
        var rep = new ResourceRepository(hikkaDbContext);

        return new ResourceService(rep);
    }
}