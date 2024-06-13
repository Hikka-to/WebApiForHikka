using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.Countries;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
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
    protected override AllServicesInControllerWithSeoAddition GetAllServices()
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var countryRepository = new CountryRepository(dbContext);
        var userManager = GetUserManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new CountryService(countryRepository), new SeoAdditionService(seoAdditionRepository), userManager);
    }

    protected override ICollection<Country> GetCollectionOfModels(int howMany)
    {
        ICollection<Country> seoAdditions = new List<Country>();
        for (int i = 0; i < howMany; ++i)
        {
            seoAdditions.Add(GetModelSample());
        }
        return seoAdditions;

    }

    protected override CountryController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in CountryControllerTest");

        return new CountryController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            GetHttpContextAccessForAdminUser(allServicesInController.UserManager)
            );
    }


    protected override CreateCountryDto GetCreateDtoSample()
    {
        return new CreateCountryDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetCountryDto GetGetDtoSample()
    {
        return new GetCountryDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionGetDtoSample(),
            Id = new Guid(),
        };
    }

    protected override Country GetModelSample()
    {
        return new Country()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateCountryDto GetUpdateDtoSample()
    {
        return new UpdateCountryDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
