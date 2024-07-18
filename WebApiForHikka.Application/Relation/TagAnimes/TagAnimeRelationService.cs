using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.TagAnimes;

public class TagAnimeRelationService : RelationCrudService<TagAnime, Tag, Anime, ITagAnimeRelationRepository>,
    ITagAnimeRelationService
{
    public TagAnimeRelationService(ITagAnimeRelationRepository repository) : base(repository)
    {
    }
}