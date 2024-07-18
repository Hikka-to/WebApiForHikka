using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.Seasons;

public class SeasonRelationService(ISeasonRelationRepository repository)
    : RelationCrudService<Season, Anime, AnimeGroup, ISeasonRelationRepository>(repository),
        ISeasonRelationService;