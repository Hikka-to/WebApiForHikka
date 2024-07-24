using WebApiForHikka.Application.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class AnimeGroupRepository(HikkaDbContext dbContext)
    : CrudRepository<AnimeGroup>(dbContext), IAnimeGroupRepository;