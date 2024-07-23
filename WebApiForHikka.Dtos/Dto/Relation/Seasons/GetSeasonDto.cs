using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.Seasons;

[ExportTsInterface]
public class GetSeasonDto : ModelDto
{
    public required GetAnimeDto Anime { get; set; }
    public required GetAnimeGroupDto AnimeGroup { get; set; }

    public required string Name { get; set; }
}