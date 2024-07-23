using WebApiForHikka.Application.WithSeoAddition.Periods;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class PeriodRepository(HikkaDbContext dbContext) : CrudRepository<Period>(dbContext), IPeriodRepository;