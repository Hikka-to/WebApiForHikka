using WebApiForHikka.Application.WithSeoAddition.Languages;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class LanguageRepository(HikkaDbContext dbContext) : CrudRepository<Language>(dbContext), ILanguageRepository;