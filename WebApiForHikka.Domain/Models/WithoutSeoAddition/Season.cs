using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.Season;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class Season : RelationModel<Anime, AnimeGroup>
{
  
    [StringLength(SeasonNumberConstants.NameLength)]
    public required string Name { get; set; }   
}
