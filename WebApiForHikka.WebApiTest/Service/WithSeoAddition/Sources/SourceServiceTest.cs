using WebApiForHikka.Application.Sources;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Sources;

public class SourceServiceTest : SharedServiceTestWithSeoAddition<Source, SourceService>
{
    protected override Source GetSample() => new()
    {
        Name = "test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Source GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
    };

    protected override SourceService GetService(HikkaDbContext hikkaDbContext)
    {
        SourceRepository sourceRepository = new(hikkaDbContext);

        return new SourceService(sourceRepository);
    }
}