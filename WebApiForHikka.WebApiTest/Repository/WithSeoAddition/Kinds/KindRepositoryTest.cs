using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Kinds;

public class KindRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Kind,
    KindRepository
    >
{
    protected override KindRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Kind GetSample() => new()
    {
        Hint = "test",
        Slug = "test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Kind GetSampleForUpdate() => new()
    {
        Hint = "test1",
        Slug = "test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
    };
}