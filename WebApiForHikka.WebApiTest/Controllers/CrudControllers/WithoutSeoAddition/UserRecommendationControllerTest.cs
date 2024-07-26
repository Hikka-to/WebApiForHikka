using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.UserRecommendations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserRecommendations;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class UserRecommendationControllerTest : CrudControllerBaseTest<
    UserRecommendationController,
    UserRecommendationService,
    UserRecommendation,
    IUserRecommendationRepository,
    UpdateUserRecommendationDto,
    CreateUserRecommendationDto,
    GetUserRecommendationDto,
    ReturnPageDto<GetUserRecommendationDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new UserRecommendationRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IUserRecommendationRepository, UserRecommendationRepository>();
        alternativeServices.AddSingleton<UserRecommendationService, UserRecommendationService>();

        return new AllServicesInController(new UserRecommendationService(repository),
            userManager,
            roleManager
        );
    }

    protected override async Task<UserRecommendationController> GetController(
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new UserRecommendationController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override CreateUserRecommendationDto GetCreateDtoSample()
    {
        return GetUserRecommendationModels.GetCreateSampleDto();
    }

    protected override GetUserRecommendationDto GetGetDtoSample()
    {
        return GetUserRecommendationModels.GetGetDtoSample();
    }

    protected override UpdateUserRecommendationDto GetUpdateDtoSample()
    {
        return GetUserRecommendationModels.GetUpdateDtoSample();
    }

    protected override UserRecommendation GetModelSample()
    {
        return GetUserRecommendationModels.GetSample();
    }
}