using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.Reviews;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Reviews;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class ReviewControllerTest : CrudControllerBaseTest<
    ReviewController,
    ReviewService,
    Review,
    IReviewRepository,
    UpdateReviewDto,
    CreateReviewDto,
    GetReviewDto,
    ReturnPageDto<GetReviewDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new ReviewRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IReviewRepository, ReviewRepository>();
        alternativeServices.AddSingleton<ReviewService, ReviewService>();

        return new AllServicesInController(new ReviewService(repository),
            userManager,
            roleManager
        );
    }

    protected override async Task<ReviewController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new ReviewController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override CreateReviewDto GetCreateDtoSample()
    {
        return GetReviewModels.GetCreateSampleDto();
    }

    protected override GetReviewDto GetGetDtoSample()
    {
        return GetReviewModels.GetGetDtoSample();
    }

    protected override UpdateReviewDto GetUpdateDtoSample()
    {
        return GetReviewModels.GetUpdateDtoSample();
    }

    protected override Review GetModelSample()
    {
        return GetReviewModels.GetSample();
    }
}