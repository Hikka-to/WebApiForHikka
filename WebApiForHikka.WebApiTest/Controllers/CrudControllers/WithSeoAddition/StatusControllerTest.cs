using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Statuses;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Statuses;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
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
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new StatusRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new StatusService(formatRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }


    protected override async Task<StatusController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in StatusControllerTest");

        return new StatusController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
        );
    }


    protected override CreateStatusDto GetCreateDtoSample()
    {
        return GetStatusModels.GetCreateDtoSample();
    }

    protected override GetStatusDto GetGetDtoSample()
    {
        return GetStatusModels.GetGetDtoSample();
    }

    protected override Status GetModelSample()
    {
        return GetStatusModels.GetSample();
    }

    protected override UpdateStatusDto GetUpdateDtoSample()
    {
        return GetStatusModels.GetUpdateDtoSample();
    }
}