using FluentAssertions;
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
    protected override KindRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new KindRepository(hikkaDbContext);
    }

    protected override Kind GetSample()
    {
        return new Kind()
        {
            Hint = "test",
            Slug = "test",
            SeoAddition = GetSeoAdditionSample(),
        };
    }

    protected override Kind GetSampleForUpdate()
    {
        return new Kind()
        {
            Hint = "test1",
            Slug = "test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        };
    }

}
