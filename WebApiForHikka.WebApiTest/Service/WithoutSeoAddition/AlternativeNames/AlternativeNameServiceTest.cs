using WebApiForHikka.Application.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AlternativeNames;

public class AlternativeNameServiceTest : SharedServiceTest<AlternativeName, AlternativeNameService>
{
    protected override AlternativeName GetSample()
    {
        return GetAlternativeNameModels.GetSample();
    }

    protected override AlternativeName GetSampleForUpdate()
    {
        return GetAlternativeNameModels.GetSampleForUpdate();
    }

    protected override AlternativeNameService GetService(HikkaDbContext hikkaDbContext)
    {
        return new AlternativeNameService(new AlternativeNameRepository(hikkaDbContext));
    }
}