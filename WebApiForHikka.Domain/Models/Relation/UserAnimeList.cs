using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.Relation;

public class UserAnimeList : RelationModel<User, Anime>
{
    public virtual required UserAnimeListType UserAnimeListType { get; set; }
    public required bool IsFavorite { get; set; }
}