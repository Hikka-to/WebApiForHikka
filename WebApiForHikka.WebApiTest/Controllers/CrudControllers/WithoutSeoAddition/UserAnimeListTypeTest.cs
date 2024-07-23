using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.UserAnimeListTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserAnimeListTypes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class UserAnimeListTypeTest : CrudControllerBaseTest<
    UserAnimeListTypeController,
    UserAnimeListTypeService,
    UserAnimeListType,
    IUserAnimeListTypeRepository,
    UpdateUserAnimeListTypeDto,
    CreateUserAnimeListTypeDto,
    GetUserAnimeListTypeDto,
    ReturnPageDto<GetUserAnimeListTypeDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();


        var repository = new UserAnimeListTypeRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IUserAnimeListTypeRepository, UserAnimeListTypeRepository>();
        alternativeServices.AddSingleton<UserAnimeListTypeService, UserAnimeListTypeService>();


        return new AllServicesInController(new UserAnimeListTypeService(repository),
            userManager,
            roleManager
        );
    }

    protected override async Task<UserAnimeListTypeController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        AllServicesInController allServices = allServicesInController;

        


        return new UserAnimeListTypeController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }
    

    protected override CreateUserAnimeListTypeDto GetCreateDtoSample() => GetUserAnimeListTypeModels.GetCreateSampleDto();
    protected override GetUserAnimeListTypeDto GetGetDtoSample() => GetUserAnimeListTypeModels.GetGetDtoSample();
    protected override UpdateUserAnimeListTypeDto GetUpdateDtoSample() => GetUserAnimeListTypeModels.GetUpdateDtoSample();
    protected override UserAnimeListType GetModelSample() => GetUserAnimeListTypeModels.GetSample();
}