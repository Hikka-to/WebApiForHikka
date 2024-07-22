using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.WithSeoAddition.Languages;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Languages;

public class LanguageServiceTest: SharedServiceTestWithSeoAddition<Language, LanguageService>
{
    protected override Language GetSample()
    {
        return GetLanguageModels.GetSample();
    }

    protected override Language GetSampleForUpdate()
    {
        return GetLanguageModels.GetSampleForUpdate();
    }

    protected override LanguageService GetService(HikkaDbContext hikkaDbContext)
    {
        LanguageRepository repository = new(hikkaDbContext);

        return new LanguageService(repository);
    }
}