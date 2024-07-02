using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.ManyToMany;

[PrimaryKey(nameof(AnimeId), nameof(TagId))]
public class TagAnime
{
    public required Guid AnimeId { get; set; }

    public required Anime Anime { get; set; }


    public required Guid TagId { get; set; }
    public required Tag Tag { get; set; }
}
