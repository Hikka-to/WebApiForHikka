using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Studios;

public class StudioRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Studio,
    StudioRepository
>
{
    protected override StudioRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new StudioRepository(hikkaDbContext);
    }

    protected override Studio GetSample()
    {
        return GetStudioModels.GetSample();
    }

    protected override Studio GetSampleForUpdate()
    {
        return GetStudioModels.GetSampleForUpdate();
    }
}