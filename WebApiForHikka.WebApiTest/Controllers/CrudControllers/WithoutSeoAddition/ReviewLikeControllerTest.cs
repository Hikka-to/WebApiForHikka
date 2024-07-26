using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.ReviewLikes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ReviewLikes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class ReviewLikeControllerTest : CrudControllerBaseTest<
    ReviewLikeController,
    ReviewLikeService,
    ReviewLike,
    IReviewLikeRepository,
    UpdateReviewLikeDto,
    CreateReviewLikeDto,
    GetReviewLikeDto,
    ReturnPageDto<GetReviewLikeDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();


        var repository = new ReviewLikeRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IReviewLikeRepository, ReviewLikeRepository>();
        alternativeServices.AddSingleton<ReviewLikeService, ReviewLikeService>();


        return new AllServicesInController(new ReviewLikeService(repository),
            userManager,
            roleManager
        );
    }

    protected override async Task<ReviewLikeController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;


        return new ReviewLikeController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateReviewLikeDto GetCreateDtoSample()
    {
        return GetReviewLikeModels.GetCreateSampleDto();
    }

    protected override GetReviewLikeDto GetGetDtoSample()
    {
        return GetReviewLikeModels.GetGetDtoSample();
    }

    protected override UpdateReviewLikeDto GetUpdateDtoSample()
    {
        return GetReviewLikeModels.GetUpdateDtoSample();
    }

    protected override ReviewLike GetModelSample()
    {
        return GetReviewLikeModels.GetSample();
    }
}