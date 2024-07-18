using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.ManyToMany;

public class Related : RelationModel<Anime, AnimeGroup>
{

    public required RelatedType RelatedType { get; set; }
}
