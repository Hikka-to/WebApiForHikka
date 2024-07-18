using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.Seasons;

public interface ISeasonRelationService : IRelationCrudRepository<Season, Anime, AnimeGroup>;
