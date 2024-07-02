using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.Constants.Models.Periods;

namespace WebApiForHikka.EfPersistence.Repositories;
public class PeriodRepository(HikkaDbContext dbContext) : CrudRepository<Period>(dbContext), IPeriodRepository;
