using WebApiForHikka.Application.Relation.Similars;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class SimilarRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<Similar, Anime, Anime>(dbContext), ISimilarRelationRepository;