using WebApiForHikka.Application.Relation.DubAnimes;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.Seasons;

public class SeasonRelationService : RelationCrudService<Season, Anime, AnimeGroup, ISeasonRelationRepository>,
    ISeasonRelationService
{
    public SeasonRelationService(ISeasonRelationRepository repository) : base(repository)
    {
    }
}