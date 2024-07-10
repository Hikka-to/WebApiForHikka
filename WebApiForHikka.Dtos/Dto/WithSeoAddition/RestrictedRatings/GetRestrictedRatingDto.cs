using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.RestrictedRatings;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/RestrictedRatings")]
public class GetRestrictedRatingDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
    public required int Value { get; set; }

    public required string Hint { get; set; }

    public string? Icon { get; set; }
}