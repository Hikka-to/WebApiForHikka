using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.Providers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Providers;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class ProviderControllerTest : CrudControllerBaseTest<
    ProviderController,
    ProviderService,
    Provider,
    IProviderRepository,
    UpdateProviderDto,
    CreateProviderDto,
    GetProviderDto,
    ReturnPageDto<GetProviderDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();


        var repository = new ProviderRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IProviderRepository, ProviderRepository>();
        alternativeServices.AddSingleton<ProviderService, ProviderService>();


        return new AllServicesInController(new ProviderService(repository),
            userManager,
            roleManager
        );
    }

    protected override async Task<ProviderController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;


        return new ProviderController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateProviderDto GetCreateDtoSample()
    {
        return GetProviderModels.GetCreateSampleDto();
    }

    protected override GetProviderDto GetGetDtoSample()
    {
        return GetProviderModels.GetGetDtoSample();
    }

    protected override UpdateProviderDto GetUpdateDtoSample()
    {
        return GetProviderModels.GetUpdateDtoSample();
    }

    protected override Provider GetModelSample()
    {
        return GetProviderModels.GetSample();
    }
}