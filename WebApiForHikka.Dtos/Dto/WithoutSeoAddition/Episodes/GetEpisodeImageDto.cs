using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Episodes;

[ExportTsInterface]
public class GetEpisodeImageDto : ModelDto
{
    public required Guid EpisodeId { get; set; }

    public required string ImageUrl { get; set; }

    public required int Width { get; set; }

    public required int Height { get; set; }

    public required List<int> Colors { get; set; }
}