using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Sources;

public class SourceRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Source,
    SourceRepository
    >
{
    protected override SourceRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new SourceRepository(hikkaDbContext);
    }

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

}

