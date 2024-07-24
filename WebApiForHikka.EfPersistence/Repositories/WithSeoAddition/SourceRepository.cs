using WebApiForHikka.Application.WithSeoAddition.Sources;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class SourceRepository(HikkaDbContext dbContext) : CrudRepository<Source>(dbContext), ISourceRepository;