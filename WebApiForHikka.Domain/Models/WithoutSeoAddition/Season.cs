using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.Season;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class Season : Model
{
    public required Anime Anime { get; set; }
    
    public required AnimeGroup AnimeGroup { get; set; }

    [StringLength(SeasonNumberConstants.NameLength)]
    public required string Name { get; set; }   
}
