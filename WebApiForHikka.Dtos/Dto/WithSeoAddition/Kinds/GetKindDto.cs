using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Kinds;

[ExportTsInterface]
public class GetKindDto : GetDtoWithSeoAddition
{
    public required string Slug { get; set; }

    public required string Hint { get; set; }
}