using WebApiForHikka.Application.WithSeoAddition.Statuses;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Statuses;

public class StatusServiceTest : SharedServiceTestWithSeoAddition<
    Status,
    StatusService
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


    protected override StatusService GetService(HikkaDbContext hikkaDbContext)
    {
        StatusRepository statusRepository = new(hikkaDbContext);

        return new StatusService(statusRepository);
    }
}