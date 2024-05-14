using FluentAssertions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.RestrictedRatings;

public class RestrictedRatingRepositoryTest : SharedRepositoryTestWithSeoAddition<
    RestrictedRating,
    RestrictedRatingRepository
    >
{
    protected override RestrictedRatingRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new RestrictedRatingRepository(hikkaDbContext);
    }

    protected override RestrictedRating GetSample()
    {
        return new RestrictedRating()
        {
            Name = "test",
            Hint = "test",
            Icon = "test",
            Value = 1,
            SeoAddition = GetSeoAdditionSample(),
        };
    }

    protected override RestrictedRating GetSampleForUpdate()
    {
        return new RestrictedRating()
        {
            Name = "test1",
            Hint = "test1",
            Icon = "test1",
            Value = 2,
            SeoAddition = GetSeoAdditionSampleUpdate(),
        };
    }

}
