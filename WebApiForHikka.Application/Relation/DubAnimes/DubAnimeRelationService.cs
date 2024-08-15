using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.DubAnimes;

public class DubAnimeRelationService : RelationCrudService<DubAnime, Dub, Anime, IDubAnimeRelationRepository>,
    IDubAnimeRelationService
{
    public DubAnimeRelationService(IDubAnimeRelationRepository repository) : base(repository)
    {
    }
}