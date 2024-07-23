using WebApiForHikka.Application.WithSeoAddition.LanguageMediaplayers;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class LanguageMediaplayerRepository(HikkaDbContext dbContext)
    : CrudRepository<LanguageMediaplayer>(dbContext), ILanguageMediaplayerRepository;