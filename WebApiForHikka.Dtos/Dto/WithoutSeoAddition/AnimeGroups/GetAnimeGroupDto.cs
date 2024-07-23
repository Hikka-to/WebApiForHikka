using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeGroups;

[ExportTsInterface]
public class GetAnimeGroupDto : ModelDto
{
    public required string Name { get; set; }
}