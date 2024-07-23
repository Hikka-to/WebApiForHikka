using WebApiForHikka.Application.WithSeoAddition.Statuses;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class StatusRepository(HikkaDbContext dbContext) : CrudRepository<Status>(dbContext), IStatusRepository;