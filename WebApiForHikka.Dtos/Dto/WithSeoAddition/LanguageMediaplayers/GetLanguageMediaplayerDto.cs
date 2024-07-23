using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Formats;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Languages;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.LanguageMediaplayers;

[ExportTsInterface]
public class GetLanguageMediaplayerDto : GetDtoWithSeoAddition
{
    public virtual required GetMediaplayerDto Mediaplayer { get; set; }

    public virtual required GetLanguageDto Language { get; set; }

    public virtual required GetEpisodeDto Episode { get; set; }

    public virtual required GetFormatDto Format { get; set; }

    public required string Url { get; set; }

    public string? FileId { get; set; }

    public uint? StartIntro { get; set; }

    public uint? EndIntro { get; set; } = null;

    public uint? StartEnding { get; set; } = null;

    public uint? EndEnding { get; set; } = null;
}