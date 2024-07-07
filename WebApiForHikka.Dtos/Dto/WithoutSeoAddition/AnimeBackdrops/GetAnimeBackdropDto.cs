using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ExportTsClass(OutputDir = "./TS/Dto/WithoutSeoAddition/AnimeBackdrops")]
public class GetAnimeBackdropDto : ModelDto
{
    public required Guid AnimeId { get; set; }

    public required string Path { get; set; }

    public required int Width { get; set; }

    public required int Height { get; set; }

    public required List<int> Colors { get; set; }
}