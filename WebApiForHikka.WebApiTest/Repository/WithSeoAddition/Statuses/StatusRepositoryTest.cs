using FluentAssertions;
using WebApiForHikka.Application.Statuses;
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
    protected override Status GetSample()
    {
        return new Status()
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionSample(),
        };
    }

    protected override Status GetSampleForUpdate()
    {
        return new Status
        {
            Name = "test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        };

    }

    protected override StatusRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new StatusRepository(hikkaDbContext);
    }
}
