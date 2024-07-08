using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.TagAnimes;

public class TagAnimeRealtionService : RelationCrudService<TagAnime, Tag, Anime, ITagAnimeRelationRepository>,
    ITagAnimeRelationService
{
    public TagAnimeRealtionService(ITagAnimeRelationRepository repository) : base(repository)
    {
    }
}