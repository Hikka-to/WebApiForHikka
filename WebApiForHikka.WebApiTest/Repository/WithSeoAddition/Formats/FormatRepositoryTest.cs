using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Formats;

public class FormatRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Format,
    FormatRepository
    >
{
    protected override FormatRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new FormatRepository(hikkaDbContext);
    }

    protected override Format GetSample()
    {
        return new Format()
        {
            Name = "test",
            SeoAddition = GetSeoAdditionSample(),
        };
    }

    protected override Format GetSampleForUpdate()
    {
        return new Format()
        {
            Name = "test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        };
    }

}
