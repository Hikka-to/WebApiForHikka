using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
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
        return GetRestrictedRatingModels.GetSample();
    }

    protected override RestrictedRating GetSampleForUpdate()
    {
        return GetRestrictedRatingModels.GetSampleForUpdate();
    }
}