using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.UserWatchHistories;
using WebApiForHikka.Dtos.Dto.Relation.WatchUserHistories;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.Relation;

namespace WebApiForHikka.Test.Controllers.CrudControllers.Relation;

public class UserWatchHistoryControllerTest : CrudControllerBaseTest<
    UserWatchHistoryController,
    UserWatchHistoryService,
    UserWatchHistory,
    IUserWatchHistoryRepository,
    UpdateUserWatchHistoryDto,
    CreateUserWatchHistoryDto,
    GetUserWatchHistoryDto,
    ReturnPageDto<GetUserWatchHistoryDto>
>

{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new UserWatchHistoryRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new UserWatchHistoryService(repository), userManager, roleManager);
    }

    protected override ICollection<UserWatchHistory> GetCollectionOfModels(int howMany)
    {
        ICollection<UserWatchHistory> seoAdditions = new List<UserWatchHistory>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<UserWatchHistoryController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new UserWatchHistoryController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateUserWatchHistoryDto GetCreateDtoSample()
    {
        return GetUserWatchHistoryModels.GetCreateDtoSample();
    }

    protected override GetUserWatchHistoryDto GetGetDtoSample()
    {
        return GetUserWatchHistoryModels.GetGetDtoSample();
    }

    protected override UserWatchHistory GetModelSample()
    {
        return GetUserWatchHistoryModels.GetSample();
    }

    protected override UpdateUserWatchHistoryDto GetUpdateDtoSample()
    {
        return GetUserWatchHistoryModels.GetUpdateDtoSample();
    }
}