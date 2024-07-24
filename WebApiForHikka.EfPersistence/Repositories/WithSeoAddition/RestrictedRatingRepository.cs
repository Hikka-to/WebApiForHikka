using WebApiForHikka.Application.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class RestrictedRatingRepository(HikkaDbContext dbContext)
    : CrudRepository<RestrictedRating>(dbContext), IRestrictedRatingRepository;