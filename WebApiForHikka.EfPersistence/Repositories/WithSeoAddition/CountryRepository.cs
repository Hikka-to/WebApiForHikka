using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class CountryRepository(HikkaDbContext dbContext) : CrudRepository<Country>(dbContext), ICountryRepository;