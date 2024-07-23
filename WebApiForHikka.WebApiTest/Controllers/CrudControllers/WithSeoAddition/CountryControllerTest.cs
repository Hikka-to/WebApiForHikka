using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Countries;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class CountryControllerTest : CrudControllerBaseWithSeoAddition<
    CountryController,
    CountryService,
    Country,
    ICountryRepository,
    UpdateCountryDto,
    CreateCountryDto,
    GetCountryDto,
    ReturnPageDto<GetCountryDto>
>

{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var countryRepository = new CountryRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new CountryService(countryRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override ICollection<Country> GetCollectionOfModels(int howMany)
    {
        ICollection<Country> seoAdditions = new List<Country>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<CountryController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in CountryControllerTest");

        return new CountryController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
        );
    }


    protected override CreateCountryDto GetCreateDtoSample()
    {
        return GetCountryModels.GetCreateDtoSample();
    }

    protected override GetCountryDto GetGetDtoSample()
    {
        return GetCountryModels.GetGetDtoSample();
    }

    protected override Country GetModelSample()
    {
        return GetCountryModels.GetSample();
    }

    protected override UpdateCountryDto GetUpdateDtoSample()
    {
        return GetCountryModels.GetUpdateDtoSample();
    }
}