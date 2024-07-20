using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
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