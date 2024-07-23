using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.LanguageMediaplayers;

public class LanguageMediaplayerRepositoryTest : SharedRepositoryTestWithSeoAddition<
    LanguageMediaplayer,
    LanguageMediaplayerRepository
>
{
    protected override LanguageMediaplayerRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new LanguageMediaplayerRepository(hikkaDbContext);
    }

    protected override LanguageMediaplayer GetSample()
    {
        return GetLanguageMediaplayerModels.GetSample();
    }

    protected override LanguageMediaplayer GetSampleForUpdate()
    {
        return GetLanguageMediaplayerModels.GetSampleForUpdate();
    }
}