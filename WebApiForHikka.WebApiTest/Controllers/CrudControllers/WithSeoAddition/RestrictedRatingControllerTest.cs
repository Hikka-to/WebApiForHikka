using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Constants.Models.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.RestrictedRatings;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
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
    ReturnPageDto<GetRestrictedRatingDto>,
    RestrictedRatingStringConstants
    >
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new RestrictedRatingRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new RestrictedRatingService(formatRepository), new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }



    protected override async Task<RestrictedRatingController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in RestrictedRatingControllerTest");

        return new RestrictedRatingController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
            );
    }


    protected override CreateRestrictedRatingDto GetCreateDtoSample()
    {
        return new CreateRestrictedRatingDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            Value = 2,
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetRestrictedRatingDto GetGetDtoSample()
    {
        return new GetRestrictedRatingDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            Value = 1,
            SeoAddition = GetSeoAdditionGetDtoSample(),
            Id = new Guid(),
        };
    }

    protected override RestrictedRating GetModelSample()
    {
        return new RestrictedRating()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Value = 1,
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateRestrictedRatingDto GetUpdateDtoSample()
    {
        return new UpdateRestrictedRatingDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            Value = 1,
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}