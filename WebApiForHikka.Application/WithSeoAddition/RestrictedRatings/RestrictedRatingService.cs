using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.RestrictedRatings;

public class RestrictedRatingService(IRestrictedRatingRepository repository)
    : CrudService<RestrictedRating, IRestrictedRatingRepository>(repository),
        IRestrictedRatingService;