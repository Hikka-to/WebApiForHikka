using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Countries;

public class CountryRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Country,
    CountryRepository
    >

{
    protected override CountryRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CountryRepository(hikkaDbContext);
    }

    protected override Country GetSample() => GetCountryModels.GetSample();
    protected override Country GetSampleForUpdate() => GetCountryModels.GetSampleForUpdate();
}
