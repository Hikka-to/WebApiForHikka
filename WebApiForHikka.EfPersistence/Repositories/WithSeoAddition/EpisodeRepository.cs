using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class EpisodeRepository(HikkaDbContext dbContext) : CrudRepository<Episode>(dbContext), IEpisodeRepository;
