using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.SeoAdditions;

public class SeoAdditionRepositoryTest : SharedRepositoryTest<SeoAddition, SeoAdditionRepository>
{
    protected override SeoAdditionRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new SeoAdditionRepository(hikkaDbContext);
    }

    protected override SeoAddition GetSample()
    {
        return GetSeoAdditionModels.GetSample();
    }

    protected override SeoAddition GetSampleForUpdate()
    {
        return GetSeoAdditionModels.GetSampleForUpdate();
    }
}