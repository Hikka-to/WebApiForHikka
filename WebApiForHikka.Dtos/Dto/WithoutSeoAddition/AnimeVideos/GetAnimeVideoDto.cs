using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithoutSeoAddition/AnimeVideos")]
public class GetAnimeVideoDto : ModelDto
{
    public required Guid AnimeVideoKindId { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }

    public required string ImageUrl { get; set; }

    public required string EmbedUrl { get; set; }
}