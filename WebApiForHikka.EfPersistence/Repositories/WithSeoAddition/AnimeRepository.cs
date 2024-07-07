using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class AnimeRepository(HikkaDbContext dbContext) : CrudRepository<Anime>(dbContext), IAnimeRepository;