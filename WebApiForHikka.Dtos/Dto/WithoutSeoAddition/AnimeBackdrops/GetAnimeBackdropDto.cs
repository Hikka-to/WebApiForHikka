using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ExportTsInterface]
public class GetAnimeBackdropDto : ModelDto
{
    public required Guid AnimeId { get; set; }

    public required string ImageUrl { get; set; }

    public required int Width { get; set; }

    public required int Height { get; set; }

    public required List<int> Colors { get; set; }
}