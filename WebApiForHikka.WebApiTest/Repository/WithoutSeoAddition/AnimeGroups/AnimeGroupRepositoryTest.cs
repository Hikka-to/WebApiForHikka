
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeGroups;

public class AnimeGroupRepositoryTest : SharedRepositoryTest<
    AnimeGroup,
    AnimeGroupRepository
>
{
    protected override AnimeGroupRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new AnimeGroupRepository(hikkaDbContext);
    }

    protected override AnimeGroup GetSample()
    {
        return GetAnimeGroupModels.GetSample();
    }

    protected override AnimeGroup GetSampleForUpdate()
    {
        return GetAnimeGroupModels.GetSampleForUpdate();
    }
}
