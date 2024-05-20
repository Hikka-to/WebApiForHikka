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
    protected override FormatRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Format GetSample() => new()
    {
        Name = "test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Format GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
    };
}