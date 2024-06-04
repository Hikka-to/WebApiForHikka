using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Studios;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Studio : ModelWithSeoAddition
{
    [StringLength(StudioNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(StudioNumberConstants.LogoLenght)]
    public string? Logo {  get; set; }
}
