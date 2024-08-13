using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.UserSettings;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserSettings;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class GetUserSettingControllerTest : CrudControllerBaseTest<
    UserSettingController,
    UserSettingService,
    UserSetting,
    IUserSettingRepository,
    UpdateUserSettingDto,
    CreateUserSettingDto,
    GetUserSettingDto,
    ReturnPageDto<GetUserSettingDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();


        var repository = new UserSettingRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IUserSettingRepository, UserSettingRepository>();
        alternativeServices.AddSingleton<UserSettingService, UserSettingService>();


        return new AllServicesInController(new UserSettingService(repository),
            userManager,
            roleManager
        );
    }

    protected override async Task<UserSettingController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;


        return new UserSettingController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateUserSettingDto GetCreateDtoSample()
    {
        return GetUserSettingModels.GetCreateSampleDto();
    }

    protected override GetUserSettingDto GetGetDtoSample()
    {
        return GetUserSettingModels.GetGetDtoSample();
    }

    protected override UpdateUserSettingDto GetUpdateDtoSample()
    {
        return GetUserSettingModels.GetUpdateDtoSample();
    }

    protected override UserSetting GetModelSample()
    {
        return GetUserSettingModels.GetSample();
    }
}