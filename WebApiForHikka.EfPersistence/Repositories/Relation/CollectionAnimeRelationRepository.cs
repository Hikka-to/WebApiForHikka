using WebApiForHikka.Application.Relation.CollectionAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class CollectionAnimeRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<CollectionAnime, Collection, Anime>(dbContext), ICollectionAnimeRelationRepository;