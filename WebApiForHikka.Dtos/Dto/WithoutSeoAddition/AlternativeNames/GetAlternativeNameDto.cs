using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AlternativeNames;

[ExportTsInterface]
public class GetAlternativeNameDto : ModelDto
{
    public required Guid AnimeId { get; set; }

    public required string Name { get; set; }
}