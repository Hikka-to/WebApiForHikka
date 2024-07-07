using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Statuses;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Statuses;

public class StatusServiceTest : SharedServiceTestWithSeoAddition<
    Status,
    StatusService
    >
{
    protected override Status GetSample() => GetStatusModels.GetSample();
    protected override Status GetSampleForUpdate() => GetStatusModels.GetSampleForUpdate();
    
    
    protected override StatusService GetService(HikkaDbContext hikkaDbContext)
    {
        StatusRepository statusRepository = new(hikkaDbContext);

        return new StatusService(statusRepository);
    }
}