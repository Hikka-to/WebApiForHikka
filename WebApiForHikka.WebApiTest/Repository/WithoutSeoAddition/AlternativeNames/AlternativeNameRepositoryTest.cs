using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AlternativeNames;

public class AlternativeNameRepositoryTest : SharedRepositoryTest<AlternativeName, AlternativeNameRepository>
{
    protected override AlternativeNameRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new AlternativeNameRepository(hikkaDbContext);
    }

    protected override AlternativeName GetSample()
    {
        return GetAlternativeNameModels.GetSample();
    }

    protected override AlternativeName GetSampleForUpdate()
    {
        return GetAlternativeNameModels.GetSampleForUpdate();
    }
}