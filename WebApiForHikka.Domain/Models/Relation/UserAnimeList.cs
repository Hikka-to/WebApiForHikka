using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.Relation;

public class UserAnimeList : RelationModel<User, Anime>
{
    public required virtual UserAnimeListType UserAnimeListType { get; set; }
    public required bool IsFavorite { get; set; }
    
}