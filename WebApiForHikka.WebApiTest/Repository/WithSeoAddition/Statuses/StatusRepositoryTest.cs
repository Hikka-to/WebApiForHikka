using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Statuses;

public class StatusRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Status,
    StatusRepository
    >
{
    protected override Status GetSample() => new()
    {
        Name = "Test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Status GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
    };

    protected override StatusRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);
}