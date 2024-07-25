using WebApiForHikka.Application.WithoutSeoAddition.Resources;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class ResourceRepository(HikkaDbContext dbContext)
    : CrudRepository<Resource>(dbContext), IResourceRepository;
