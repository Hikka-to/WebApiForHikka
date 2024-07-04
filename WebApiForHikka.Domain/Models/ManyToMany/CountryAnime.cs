using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.ManyToMany;

public class CountryAnime : RelationModel
{
    public required Anime Anime { get; set; }

    public required Country Country { get; set; }
}
