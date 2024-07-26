using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class RestrictedRatingControllerTest : CrudControllerBaseWithSeoAddition<
    RestrictedRatingController,
    RestrictedRatingService,
    RestrictedRating,
    IRestrictedRatingRepository,
    UpdateRestrictedRatingDto,
    CreateRestrictedRatingDto,
    GetRestrictedRatingDto,
    ReturnPageDto<GetRestrictedRatingDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new RestrictedRatingRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new RestrictedRatingService(formatRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }


    protected override async Task<RestrictedRatingController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in RestrictedRatingControllerTest");

        return new RestrictedRatingController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
        );
    }


    protected override CreateRestrictedRatingDto GetCreateDtoSample()
    {
        return GetRestrictedRatingModels.GetCreateDtoSample();
    }

    protected override GetRestrictedRatingDto GetGetDtoSample()
    {
        return GetRestrictedRatingModels.GetGetDtoSample();
    }

    protected override RestrictedRating GetModelSample()
    {
        return GetRestrictedRatingModels.GetSample();
    }

    protected override UpdateRestrictedRatingDto GetUpdateDtoSample()
    {
        return GetRestrictedRatingModels.GetUpdateDtoSample();
    }
}