using WebApiForHikka.Application.WithSeoAddition.Kinds;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class KindRepository(HikkaDbContext dbContext) : CrudRepository<Kind>(dbContext), IKindRepository;