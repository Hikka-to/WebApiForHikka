using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.Similars;

public interface ISimilarRelationRepository : IRelationCrudRepository<Similar, Anime, Anime>;