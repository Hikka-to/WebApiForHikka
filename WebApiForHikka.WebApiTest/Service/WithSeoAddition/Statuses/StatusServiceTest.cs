using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Statuses;

public class StatusServiceTest : SharedServiceTestWithSeoAddition<
    Status,
    StatusService
    >
{
    protected override Status GetSample() => new()
    {
        Name = "test",
        SeoAddition = GetSeoAdditionSample()
    };

    protected override Status GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionSampleUpdate()
    };
    protected override StatusService GetService(HikkaDbContext hikkaDbContext)
    {
        StatusRepository statusRepository = new(hikkaDbContext);

        return new StatusService(statusRepository);
    }
}