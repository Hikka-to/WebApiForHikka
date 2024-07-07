using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.RestrictedRatings;

namespace WebApiForHikka.Domain.Models;

public class RestrictedRating : ModelWithSeoAddition
{
    [StringLength(RestrictedRatingNumberConstants.NameLength)]
    public required string Name { get; set; }

    public required int Value { get; set; }

    [StringLength(RestrictedRatingNumberConstants.HintLength)]
    public required string Hint { get; set; }

    [StringLength(RestrictedRatingNumberConstants.IconLength)]
    public string? Icon { get; set; }
}