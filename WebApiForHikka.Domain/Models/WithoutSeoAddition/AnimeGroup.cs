using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class AnimeGroup : Model
{
    [StringLength(AnimeGroupNumberConstants.NameLength)]
    public required string Name { get; set; }

    public virtual ICollection<Anime> RelatedAnimes { get; set; } = [];
    public virtual ICollection<Anime> SeasonAnimes { get; set; } = [];
}