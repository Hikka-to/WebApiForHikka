using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.SeoAdditions;

public class SeoAdditionServiceTest : SharedServiceTest<SeoAddition, SeoAdditionService>
{
    protected override SeoAddition GetSample()
    {
        return GetSeoAdditionModels.GetSample();
    }

    protected override SeoAddition GetSampleForUpdate()
    {
        return GetSeoAdditionModels.GetSampleForUpdate();
    }

    protected override SeoAdditionService GetService(HikkaDbContext hikkaDbContext)
    {
        SeoAdditionRepository repository = new(hikkaDbContext);

        return new SeoAdditionService(repository);
    }
}