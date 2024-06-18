using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.RestrictedRatings;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.RestrictedRatings;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/RestrictedRatings")]
public class CreateRestrictedRatingDto : CreateDtoWithSeoAddition
{
    [StringLength(RestrictedRatingNumberConstants.NameLenght)]
    public required string Name { get; set; }
    public required int Value { get; set; }

    [StringLength(RestrictedRatingNumberConstants.HintLenght)]
    public required string Hint { get; set; }

    [StringLength(RestrictedRatingNumberConstants.IconLenght)]
    public string? Icon { get; set; }
}