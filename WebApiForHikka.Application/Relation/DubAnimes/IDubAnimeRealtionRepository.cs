using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;

namespace WebApiForHikka.Application.Relation.DubAnimes;

public interface IDubAnimeRealtionRepository : IRelationCrudRepository<DubAnime>;
