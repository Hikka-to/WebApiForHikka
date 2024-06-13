using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

class DubControllerTest : CrudControllerBaseWithSeoAddition<
    DubController,
    DubService,
    Dub,
    IDubRepository,
    UpdateDubDto,
    CreateDubDto,
    GetDubDto,
    ReturnPageDto<GetDubDto>
    >

{
    protected override AllServicesInControllerWithSeoAddition GetAllServices()
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var countryRepository = new DubRepository(dbContext);
        var userManager = GetUserManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new DubService(countryRepository), new SeoAdditionService(seoAdditionRepository), userManager);
    }

    protected override ICollection<Dub> GetCollectionOfModels(int howMany)
    {
        ICollection<Dub> seoAdditions = new List<Dub>();
        for (int i = 0; i < howMany; ++i)
        {
            seoAdditions.Add(GetModelSample());
        }
        return seoAdditions;

    }

    protected override DubController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in DubControllerTest");

        return new DubController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            GetHttpContextAccessForAdminUser(allServicesInController.UserManager)
            );
    }


    protected override CreateDubDto GetCreateDtoSample()
    {
        return new CreateDubDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetDubDto GetGetDtoSample()
    {
        return new GetDubDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionGetDtoSample(),
            Id = new Guid(),
        };
    }

    protected override Dub GetModelSample()
    {
        return new Dub()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateDubDto GetUpdateDtoSample()
    {
        return new UpdateDubDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}