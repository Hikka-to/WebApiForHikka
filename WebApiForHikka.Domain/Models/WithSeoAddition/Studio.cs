using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Studios;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Studio : ModelWithSeoAddition
{
    [StringLength(StudioNumberConstants.NameLength)]
    public required string Name { get; set; }

    [StringLength(StudioNumberConstants.LogoLength)]
    public string? Logo { get; set; }
}