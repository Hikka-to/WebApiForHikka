using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.CollectionAnimes;

public interface ICollectionAnimeRelationRepository : IRelationCrudRepository<CollectionAnime, Collection, Anime>;