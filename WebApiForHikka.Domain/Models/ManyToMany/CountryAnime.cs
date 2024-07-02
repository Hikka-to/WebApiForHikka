using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.ManyToMany;

[PrimaryKey(nameof(AnimeId), nameof(CountryId))]
public class CountryAnime
{
    public required Guid AnimeId { get; set; }
    public required Anime Anime { get; set; }

    public required Guid CountryId { get; set; }
    public required Country Country { get; set; }
}
