using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class SeasonRepository(HikkaDbContext dbContext)
    : CrudRepository<Season>(dbContext), ISeasonRepository;