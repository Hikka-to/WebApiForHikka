using WebApiForHikka.Application.Formats;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Formats;

public class FormatServiceTest : SharedServiceTestWithSeoAddition<Format, FormatService>
{
    protected override Format GetSample() => GetFormatModels.GetSample();
    protected override Format GetSampleForUpdate() => GetFormatModels.GetSampleForUpdate();

    protected override FormatService GetService(HikkaDbContext hikkaDbContext)
    {
        FormatRepository repository = new(hikkaDbContext);

        return new FormatService(repository);
    }
}