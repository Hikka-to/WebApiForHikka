using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class ExternalLink : Model
{
    public virtual required Anime Anime { get; set; }

    [StringLength(ExternalLinkNumberConstants.UrlLength)]
    public required string Url { get; set; }
}