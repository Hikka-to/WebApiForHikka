using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Countries;

public class CountryServiceTest : SharedServiceTestWithSeoAddition<Country, CountryService>
{
    protected override Country GetSample()
    {
        return new Country()
        {
            Icon = "Icon",
            Name = "Name",
            SeoAddition = GetSeoAdditionSample()
        };
    }

    protected override Country GetSampleForUpdate()
    {
        return new Country()
        {
            Icon = "Icon1",
            Name = "Name1",
            SeoAddition = GetSeoAdditionSampleUpdate()
        };
    }

    protected override CountryService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CountryService(new CountryRepository(hikkaDbContext));
    }
}
