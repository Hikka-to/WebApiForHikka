using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.ManyToMany;

[PrimaryKey(nameof(AnimeId), nameof(DubId))]
public class DubAnime
{
    public required Guid AnimeId { get; set; }
    public required Anime Anime { get; set; }

    public required Guid DubId { get; set; }
    public required Dub Dub { get; set; }
}
