using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.RestrictRatings;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.RestrictedRatings;
public class CreateRestrictedRatingDto : CreateDtoWithSeoAddition
{
    [StringLength(RestrictedRatingNumberConstants.NameLenght)]
    public required string Name { get; set; }
    public required int Value { get; set; }

    [StringLength(RestrictedRatingNumberConstants.HintLenght)]
    public required string Hint { get; set; }

    [StringLength(RestrictedRatingNumberConstants.IconLenght)]
    public required string Icon { get; set; }
}
