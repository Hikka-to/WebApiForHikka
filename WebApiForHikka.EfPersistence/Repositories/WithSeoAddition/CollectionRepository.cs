using WebApiForHikka.Application.WithSeoAddition.Collections;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class CollectionRepository(HikkaDbContext dbContext)
    : CrudRepository<Collection>(dbContext), ICollectionRepository;