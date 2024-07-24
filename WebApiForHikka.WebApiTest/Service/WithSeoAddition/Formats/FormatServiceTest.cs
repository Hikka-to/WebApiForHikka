using WebApiForHikka.Application.WithSeoAddition.Formats;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Formats;

public class FormatServiceTest : SharedServiceTestWithSeoAddition<Format, FormatService>
{
    protected override Format GetSample()
    {
        return GetFormatModels.GetSample();
    }

    protected override Format GetSampleForUpdate()
    {
        return GetFormatModels.GetSampleForUpdate();
    }

    protected override FormatService GetService(HikkaDbContext hikkaDbContext)
    {
        FormatRepository repository = new(hikkaDbContext);

        return new FormatService(repository);
    }
}