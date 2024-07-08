using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.RestrictedRatings;

public class RestrictedRatingService(IRestrictedRatingRepository repository)
    : CrudService<RestrictedRating, IRestrictedRatingRepository>(repository),
        IRestrictedRatingService;