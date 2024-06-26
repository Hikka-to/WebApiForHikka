using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared.Attributes;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes
{
    public class RestrictedRatingValidationAttribute : EntityValidationAttribute<RestrictedRating, IRestrictedRatingService>;
}
