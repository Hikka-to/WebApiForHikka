using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.RestrictedRatings;

public class RestrictedRatingServiceTest : SharedServiceTestWithSeoAddition<RestrictedRating, RestrictedRatingService>
{
    protected override RestrictedRating GetSample()
    {
        return new RestrictedRating()
        {
            Hint = "test",
            Icon = "test",
            Name = "test",
            Value = 1,
            SeoAddition = GetSeoAdditionSample(),
        };
    }

    protected override RestrictedRating GetSampleForUpdate()
    {
        return new RestrictedRating()
        {
            Hint = "test1",
            Icon = "test1",
            Name = "test1",
            Value = 2,
            SeoAddition = GetSeoAdditionSampleUpdate(),
        };
    }

    protected override RestrictedRatingService GetService(HikkaDbContext hikkaDbContext)
    {
        RestrictedRatingRepository repository = new RestrictedRatingRepository(hikkaDbContext);

        return new RestrictedRatingService(repository);
    }
}