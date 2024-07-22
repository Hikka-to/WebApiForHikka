using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.LanguageMediaplayers;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class LanguageMediaplayer : ModelWithSeoAddition
{
    public virtual required Mediaplayer Mediaplayer { get; set; }

    public virtual required Language Language { get; set; }

    public virtual required Episode Episode { get; set; }

    public virtual required Format Format { get; set; }

    [StringLength(SharedNumberConstatnts.UrlLength)]
    public required string Url { get; set; }

    [StringLength(LanguageMediaplayerNumberConstants.FileIdLength)]
    public string? FileId { get; set; }

    public uint? StartIntro { get; set; }

    public uint? EndIntro { get; set; } = null;

    public uint? StartEnding { get; set; } = null;

    public uint? EndEnding { get; set; } = null;
}