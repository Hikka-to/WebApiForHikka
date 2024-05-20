using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.RestrictedRatings;

public interface IRestrictedRatingRepository : ICrudRepository<RestrictedRating>;