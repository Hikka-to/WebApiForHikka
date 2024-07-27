using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.UserAnimeLists;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.UserAnimeLists;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.Relation;

namespace WebApiForHikka.Test.Controllers.CrudControllers.Relation;

public class UserAnimeListControllerTest : CrudControllerBaseTest<
    UserAnimeListController,
    UserAnimeListRelationService,
    UserAnimeList,
    IUserAnimeListRelationRepository,
    UpdateUserAnimeListDto,
    CreateUserAnimeListDto,
    GetUserAnimeListDto,
    ReturnPageDto<GetUserAnimeListDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new UserAnimeListRelationRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new UserAnimeListRelationService(repository), userManager, roleManager);
    }

    protected override ICollection<UserAnimeList> GetCollectionOfModels(int howMany)
    {
        ICollection<UserAnimeList> userAnimeLists = new List<UserAnimeList>();
        for (var i = 0; i < howMany; ++i) userAnimeLists.Add(GetModelSample());
        return userAnimeLists;
    }

    protected override async Task<UserAnimeListController> GetController(
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new UserAnimeListController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateUserAnimeListDto GetCreateDtoSample()
    {
        return GetUserAnimeListModels.GetCreateDtoSample();
    }

    protected override GetUserAnimeListDto GetGetDtoSample()
    {
        return GetUserAnimeListModels.GetGetDtoSample();
    }

    protected override UserAnimeList GetModelSample()
    {
        return GetUserAnimeListModels.GetSample();
    }

    protected override UpdateUserAnimeListDto GetUpdateDtoSample()
    {
        return GetUserAnimeListModels.GetUpdateDtoSample();
    }
}