using WebApiForHikka.Application.WithSeoAddition.Studios;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Studios;

public class StudioServiceTest : SharedServiceTestWithSeoAddition<
    Studio,
    StudioService
    >
{
    protected override Studio GetSample() => new()
    {
        Name = "test",
        Logo = "test",
        SeoAddition = GetSeoAdditionSample()
    };


    protected override Studio GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionSampleUpdate()
    };
    protected override StudioService GetService(HikkaDbContext hikkaDbContext)
    {
        StudioRepository statusRepository = new(hikkaDbContext);

        return new StudioService(statusRepository);
    }
}