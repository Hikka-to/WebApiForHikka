using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;

namespace WebApiForHikka.Application.Relation.DubAnimes;

public class DubAnimeRelationService : RelationCrudService<DubAnime, IDubAnimeRealtionRepository>, IDubAnimeRelationService
{
    public DubAnimeRelationService(IDubAnimeRealtionRepository repository) : base(repository)
    {
    }
}
