using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.Relateds;

public class RelatedRelationService(IRelatedRelationRepository relationRepository) :
    RelationCrudService<Related, Anime, AnimeGroup, IRelatedRelationRepository>(relationRepository),
    IRelatedRelationService;