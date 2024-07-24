using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;

[ExportTsInterface]
public class GetEpisodeDto : GetDtoWithSeoAddition
{
    public Guid AnimeId { get; set; }

    public required string Name { get; set; }

    public required int Duration { get; set; }

    public required DateTime AirDate { get; set; }

    public required bool IsFiller { get; set; } = false;

    public required DateTime UpdatedAt { get; set; }
    public required DateTime CreatedAt { get; set; }
}