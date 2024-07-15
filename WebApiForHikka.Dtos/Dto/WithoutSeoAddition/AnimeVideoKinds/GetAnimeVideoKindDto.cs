using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;

[ExportTsInterface]
public class GetAnimeVideoKindDto : ModelDto
{
    public required string Name { get; set; }
}