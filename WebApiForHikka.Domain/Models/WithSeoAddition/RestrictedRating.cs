using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.RestrictedRatings;

namespace WebApiForHikka.Domain.Models;
public class RestrictedRating : ModelWithSeoAddition
{
    [StringLength(RestrictedRatingNumberConstants.NameLenght)]
    public required string Name { get; set; }
    public required int Value { get; set; }

    [StringLength(RestrictedRatingNumberConstants.HintLenght)]
    public required string Hint { get; set; }

    [StringLength(RestrictedRatingNumberConstants.IconLenght)]
    public string? Icon { get; set; }

}
