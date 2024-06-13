using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Status;
using WebApiForHikka.Dtos.Dto.Statuses;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class StatusControllerTest : CrudControllerBaseWithSeoAddition<
    StatusController,
    StatusService,
    Status,
    IStatusRepository,
    UpdateStatusDto,
    CreateStatusDto,
    GetStatusDto,
    ReturnPageDto<GetStatusDto>
    >
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices()
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new StatusRepository(dbContext);
        var userManager = GetUserManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new StatusService(formatRepository), new SeoAdditionService(seoAdditionRepository), userManager);
    }



    protected override StatusController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in StatusControllerTest");

        return new StatusController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            GetHttpContextAccessForAdminUser(allServicesInController.UserManager)
            );
    }


    protected override CreateStatusDto GetCreateDtoSample()
    {
        return new CreateStatusDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetStatusDto GetGetDtoSample()
    {
        return new GetStatusDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionGetDtoSample(),
            Id = new Guid(),
        };
    }

    protected override Status GetModelSample()
    {
        return new Status()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateStatusDto GetUpdateDtoSample()
    {
        return new UpdateStatusDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}