using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class SeasonRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<Season, Anime, AnimeGroup>(dbContext), ISeasonRelationRepository;