using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.Resources;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class Resource : Model
{
    [StringLength(ResourceNumberConstants.SlugLength)]
    public required string Slug { get; set; }
}
