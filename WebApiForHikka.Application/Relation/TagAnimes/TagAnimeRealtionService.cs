using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;

namespace WebApiForHikka.Application.Relation.TagAnimes;

public class TagAnimeRealtionService : RelationCrudService<TagAnime, ITagAnimeRelationRepository>, ITagAnimeRelationService
{
    public TagAnimeRealtionService(ITagAnimeRelationRepository repository) : base(repository)
    {
    }
}
