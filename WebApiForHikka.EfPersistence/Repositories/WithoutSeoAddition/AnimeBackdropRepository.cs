using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Constants.Models.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class AnimeBackdropRepository(HikkaDbContext dbContext) : CrudRepository<AnimeBackdrop>(dbContext), IAnimeBackdropRepository;
