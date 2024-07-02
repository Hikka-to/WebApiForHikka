using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Constants.Models.Animes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class AnimeRepository(HikkaDbContext dbContext) : CrudRepository<Anime>(dbContext), IAnimeRepository;
