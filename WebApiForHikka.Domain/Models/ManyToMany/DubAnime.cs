using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.ManyToMany;

public class DubAnime : RelationModel
{
    public required Anime Anime { get; set; }
    public required Dub Dub { get; set; }
}
