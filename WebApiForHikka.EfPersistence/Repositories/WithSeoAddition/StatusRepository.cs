using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;
public class StatusRepository(HikkaDbContext dbContext) : CrudRepository<Status>(dbContext), IStatusRepository;
