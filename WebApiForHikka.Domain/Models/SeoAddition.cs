using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.SeoAdditions;
using WebApiForHikka.Domain.Enums;

namespace WebApiForHikka.Domain.Models;

public class SeoAddition : Model
{
    [StringLength(SeoAdditionNumberConstants.SlugLength)]
    public required string Slug { get; set; }

    [StringLength(SeoAdditionNumberConstants.TitleLength)]
    public required string Title { get; set; }

    [StringLength(SeoAdditionNumberConstants.DescriptionLength)]
    public required string Description { get; set; }

    [StringLength(SeoAdditionNumberConstants.ImageLength)]
    public string? Image { get; set; }

    [StringLength(SeoAdditionNumberConstants.ImageAltLength)]
    public string? ImageAlt { get; set; }

    [StringLength(SeoAdditionNumberConstants.SocialTitleLength)]
    public string? SocialTitle { get; set; }

    public SocialType? SocialType { get; set; } // Consider using an enum type here if you have specific values

    [StringLength(SeoAdditionNumberConstants.SocialImageLength)]
    public string? SocialImage { get; set; }

    [StringLength(SeoAdditionNumberConstants.SocialImageAltLength)]
    public string? SocialImageAlt { get; set; }
}