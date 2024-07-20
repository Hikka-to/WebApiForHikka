using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.RestrictedRatings;

public class RestrictedRatingServiceTest : SharedServiceTestWithSeoAddition<RestrictedRating, RestrictedRatingService>
{
    protected override RestrictedRating GetSample()
    {
        return GetRestrictedRatingModels.GetSample();
    }

    protected override RestrictedRating GetSampleForUpdate()
    {
        return GetRestrictedRatingModels.GetSampleForUpdate();
    }

    protected override RestrictedRatingService GetService(HikkaDbContext hikkaDbContext)
    {
        RestrictedRatingRepository repository = new(hikkaDbContext);

        return new RestrictedRatingService(repository);
    }
}