using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.SeoAdditions;

namespace WebApiForHikka.Domain.Models;
public class SeoAddition : Model
{
    [StringLength(SeoAdditionNumberConstants.SlugLength)]
    public string Slug { get; set; }

    [StringLength(SeoAdditionNumberConstants.TitleLength)]
    public string Title { get; set; }

    [StringLength(SeoAdditionNumberConstants.DescriptionLength)]
    public string Description { get; set; }

    [StringLength(SeoAdditionNumberConstants.ImageLength)]
    public string? Image { get; set; }

    [StringLength(SeoAdditionNumberConstants.ImageAltLength)]
    public string? ImageAlt { get; set; }

    [StringLength(SeoAdditionNumberConstants.SocialTitleLength)]
    public string? SocialTitle { get; set; }

    [StringLength(SeoAdditionNumberConstants.SocialTypeLength)]
    public string? SocialType { get; set; } // Consider using an enum type here if you have specific values

    [StringLength(SeoAdditionNumberConstants.SocialImageLength)]
    public string? SocialImage { get; set; }

    [StringLength(SeoAdditionNumberConstants.SocialImageAltLength)]
    public string? SocialImageAlt { get; set; }
}
