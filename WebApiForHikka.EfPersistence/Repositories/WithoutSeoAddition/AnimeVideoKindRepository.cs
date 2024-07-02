using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Constants.Models.AnimeVideoKinds;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class AnimeVideoKindRepository(HikkaDbContext dbContext) : CrudRepository<AnimeVideoKind>(dbContext), IAnimeVideoKindRepository;
