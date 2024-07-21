using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.CollectionAnimes;

public class CollectionAnimeRelationService(ICollectionAnimeRelationRepository collectionAnimeRelationRepository)
    : RelationCrudService<CollectionAnime, Collection, Anime, ICollectionAnimeRelationRepository>(
        collectionAnimeRelationRepository), ICollectionAnimeRelationService;