using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Countries;

public class CountryService(ICountryRepository repository)
    : CrudService<Country, ICountryRepository>(repository), ICountryService;