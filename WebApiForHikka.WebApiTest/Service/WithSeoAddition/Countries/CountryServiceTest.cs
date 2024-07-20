using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Countries;

public class CountryServiceTest : SharedServiceTestWithSeoAddition<Country, CountryService>
{
    protected override Country GetSample()
    {
        return GetCountryModels.GetSample();
    }


    protected override Country GetSampleForUpdate()
    {
        return GetCountryModels.GetSampleForUpdate();
    }

    protected override CountryService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CountryService(new CountryRepository(hikkaDbContext));
    }
}