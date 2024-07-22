using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.LanguageMediaplayers;

[ExportTsInterface]
public class GetLanguageMediaplayerDto : GetDtoWithSeoAddition
{
    public virtual required Mediaplayer Mediaplayer { get; set; }

    public virtual required Language Language { get; set; }

    public virtual required Episode Episode { get; set; }

    public virtual required Format Format { get; set; }

    public required string Url { get; set; }

    public string? FileId { get; set; }

    public uint? StartIntro { get; set; }

    public uint? EndIntro { get; set; } = null;

    public uint? StartEnding { get; set; } = null;

    public uint? EndEnding { get; set; } = null;
}