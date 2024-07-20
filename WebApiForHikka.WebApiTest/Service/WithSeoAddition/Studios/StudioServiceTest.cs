using WebApiForHikka.Application.WithSeoAddition.Studios;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Studios;

public class StudioServiceTest : SharedServiceTestWithSeoAddition<
    Studio,
    StudioService
>
{
    protected override Studio GetSample()
    {
        return GetStudioModels.GetSample();
    }

    protected override Studio GetSampleForUpdate()
    {
        return GetStudioModels.GetSampleForUpdate();
    }

    protected override StudioService GetService(HikkaDbContext hikkaDbContext)
    {
        StudioRepository statusRepository = new(hikkaDbContext);

        return new StudioService(statusRepository);
    }
}