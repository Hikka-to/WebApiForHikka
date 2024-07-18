using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.AnimeVideos;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class AnimeVideo : Model
{
    public required AnimeVideoKind AnimeVideoKind { get; set; }

    [StringLength(AnimeVideoNumberConstants.NameLength)]
    public required string Name { get; set; }

    [StringLength(AnimeVideoNumberConstants.UrlLength)]
    public required string Url { get; set; }

    [StringLength(AnimeVideoNumberConstants.ImageUrlLength)]
    public required string ImageUrl { get; set; }

    [StringLength(AnimeVideoNumberConstants.EmbedUrlLength)]
    public required string EmbedUrl { get; set; }
}