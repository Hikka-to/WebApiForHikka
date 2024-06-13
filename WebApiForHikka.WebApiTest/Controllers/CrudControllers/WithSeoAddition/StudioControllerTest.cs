using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Studios;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class StudioControllerTest : CrudControllerBaseWithSeoAddition<
    StudioController,
    StudioService,
    Studio,
    IStudioRepository,
    UpdateStudioDto,
    CreateStudioDto,
    GetStudioDto,
    ReturnPageDto<GetStudioDto>
    >

{
    protected override AllServicesInControllerWithSeoAddition GetAllServices()
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var countryRepository = new StudioRepository(dbContext);
        var userManager = GetUserManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new StudioService(countryRepository), new SeoAdditionService(seoAdditionRepository), userManager);
    }

    protected override ICollection<Studio> GetCollectionOfModels(int howMany)
    {
        ICollection<Studio> seoAdditions = new List<Studio>();
        for (int i = 0; i < howMany; ++i)
        {
            seoAdditions.Add(GetModelSample());
        }
        return seoAdditions;

    }

    protected override StudioController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in StudioControllerTest");

        return new StudioController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            GetHttpContextAccessForAdminUser(allServicesInController.UserManager)
            );
    }


    protected override CreateStudioDto GetCreateDtoSample()
    {
        return new CreateStudioDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Logo = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetStudioDto GetGetDtoSample()
    {
        return new GetStudioDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Logo = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionGetDtoSample(),
            Id = new Guid(),
        };
    }

    protected override Studio GetModelSample()
    {
        return new Studio()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Logo = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateStudioDto GetUpdateDtoSample()
    {
        return new UpdateStudioDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Logo = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
