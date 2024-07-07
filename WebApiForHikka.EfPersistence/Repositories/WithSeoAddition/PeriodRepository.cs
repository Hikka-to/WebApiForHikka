using WebApiForHikka.Application.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class PeriodRepository(HikkaDbContext dbContext) : CrudRepository<Period>(dbContext), IPeriodRepository;