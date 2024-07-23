using WebApiForHikka.Application.WithoutSeoAddition.Providers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class ProviderRepository(HikkaDbContext dbContext)
    : CrudRepository<Provider>(dbContext), IProviderRepository
{
}