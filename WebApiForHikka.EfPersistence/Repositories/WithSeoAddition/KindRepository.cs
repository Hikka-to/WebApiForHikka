using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class KindRepository(HikkaDbContext dbContext) : CrudRepository<Kind>(dbContext), IKindRepository;