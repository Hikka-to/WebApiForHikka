using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.Relation;

public class Related : RelationModel<Anime, AnimeGroup>
{
    public virtual required RelatedType RelatedType { get; set; }
}