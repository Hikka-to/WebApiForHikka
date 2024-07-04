using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.ManyToMany;

public class TagAnime : RelationModel
{

    public required Anime Anime { get; set; }

    public required Tag Tag { get; set; }
}
