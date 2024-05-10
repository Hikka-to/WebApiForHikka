using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.RestrictedRatings;

public class RestrictedRatingService : CrudService<RestrictedRating, IRestrictedRatingRepository>, IRestrictedRatingService
{
    public RestrictedRatingService(IRestrictedRatingRepository repository) : base(repository)
    {
    }
}
