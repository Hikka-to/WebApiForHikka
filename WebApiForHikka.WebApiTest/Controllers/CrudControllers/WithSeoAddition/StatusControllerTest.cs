using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Statuses;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
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

        return new AllServicesInControllerWithSeoAddition(new StatusService(formatRepository), new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }



    protected override async Task<StatusController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in StatusControllerTest");

        return new StatusController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
            );
    }


    protected override CreateStatusDto GetCreateDtoSample() => GetStatusModels.GetCreateDtoSample();
    protected override GetStatusDto GetGetDtoSample() => GetStatusModels.GetGetDtoSample();
    protected override Status GetModelSample()=> GetStatusModels.GetSample();
    protected override UpdateStatusDto GetUpdateDtoSample() => GetStatusModels.GetUpdateDtoSample();
}