using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Languages;

public class LanguageRepositoryTest: SharedRepositoryTestWithSeoAddition<
    Language,
    LanguageRepository
>
{
    protected override LanguageRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new LanguageRepository(hikkaDbContext);
    }

    protected override Language GetSample()
    {
        return GetLanguageModels.GetSample();
    }

    protected override Language GetSampleForUpdate()
    {
        return GetLanguageModels.GetSampleForUpdate();
    }
}