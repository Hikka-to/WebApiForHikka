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
    protected override SourceRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

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
}