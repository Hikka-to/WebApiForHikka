using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.DubAnimes;

public interface IDubAnimeRealtionRepository : IRelationCrudRepository<DubAnime, Dub, Anime>;