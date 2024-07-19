using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.Similars;

public class SimilarRelationService(ISimilarRelationRepository relationRepository)
    : RelationCrudService<Similar, Anime, Anime, ISimilarRelationRepository>(relationRepository);