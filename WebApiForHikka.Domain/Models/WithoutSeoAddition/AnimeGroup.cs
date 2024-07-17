using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.AnimeGroups;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class AnimeGroup : Model
{
    [StringLength(AnimeGroupNumberConstants.NameLength)]
    public required string Name { get; set; }
}
