using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ExternalLinks;

[ExportTsInterface]
public class GetExternalLinkDto : ModelDto
{
    public required Guid AnimeId { get; set; }
    public required string Url { get; set; }
}