using FluentAssertions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Sources;

public class SourceServiceTest : SharedServiceTestWithSeoAddition<Source, SourceService>
{
    protected override Source GetSample()
    {
        return new Source()
        {
            Name = "test",
            SeoAddition = GetSeoAdditionSample(),
        };

    }

    protected override Source GetSampleForUpdate()
    {
        return new Source()
        {
            Name = "test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        };
    }

    protected override SourceService GetService(HikkaDbContext hikkaDbContext)
    {
        SourceRepository sourceRepository = new SourceRepository(hikkaDbContext);

        return new SourceService(sourceRepository);
    }
}
