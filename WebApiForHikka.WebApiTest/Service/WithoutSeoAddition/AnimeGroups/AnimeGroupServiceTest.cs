using WebApiForHikka.Application.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeGroups;

public class AnimeGroupServiceTest : SharedServiceTest<AnimeGroup, AnimeGroupService>
{
    protected override AnimeGroup GetSample()
    {
        return GetAnimeGroupModels.GetSample();
    }

    protected override AnimeGroup GetSampleForUpdate()
    {
        return GetAnimeGroupModels.GetSampleForUpdate();
    }

    protected override AnimeGroupService GetService(HikkaDbContext hikkaDbContext)
    {
        return new AnimeGroupService(new AnimeGroupRepository(hikkaDbContext));
    }
}