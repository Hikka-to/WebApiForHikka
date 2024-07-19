using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Episodes;

public class EpisodeRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Episode,
    EpisodeRepository
>

{
    protected override EpisodeRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new EpisodeRepository(hikkaDbContext);
    }

    protected override Episode GetSample()
    {
        return GetEpisodeModels.GetSample();
    }

    protected override Episode GetSampleForUpdate()
    {
        return GetEpisodeModels.GetSampleForUpdate();
    }
}