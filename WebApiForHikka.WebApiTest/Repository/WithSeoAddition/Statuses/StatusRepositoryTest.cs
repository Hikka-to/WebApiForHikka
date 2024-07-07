using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Statuses;

public class StatusRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Status,
    StatusRepository
>
{
    protected override Status GetSample()
    {
        return GetStatusModels.GetSample();
    }

    protected override Status GetSampleForUpdate()
    {
        return GetStatusModels.GetSampleForUpdate();
    }

    protected override StatusRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new StatusRepository(hikkaDbContext);
    }
}