using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.AnimeAnimeGroups;

public class RelatedRepository:
    RelationCrudService<Related, Anime, AnimeGroup, IRelatedRepository>, IRelatedService
{
    public RelatedRepository(IRelatedRepository repository) : base(repository)
    {
    }
}