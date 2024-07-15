using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class AnimeVideoKindControllerTest : CrudControllerBaseTest<
    AnimeVideoKindController,
    AnimeVideoKindService,
    AnimeVideoKind,
    IAnimeVideoKindRepository,
    UpdateAnimeVideoKindDto,
    CreateAnimeVideoKindDto,
    GetAnimeVideoKindDto,
    ReturnPageDto<GetAnimeVideoKindDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new AnimeVideoKindRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new AnimeVideoKindService(repository), userManager, roleManager);
    }

    protected override async Task<AnimeVideoKindController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new AnimeVideoKindController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override CreateAnimeVideoKindDto GetCreateDtoSample()
    {
        return GetAnimeVideoKindModels.GetCreateDtoSample();
    }

    protected override GetAnimeVideoKindDto GetGetDtoSample()
    {
        return GetAnimeVideoKindModels.GetGetDtoSample();
    }

    protected override UpdateAnimeVideoKindDto GetUpdateDtoSample()
    {
        return GetAnimeVideoKindModels.GetUpdateDtoSample();
    }

    protected override AnimeVideoKind GetModelSample()
    {
        return GetAnimeVideoKindModels.GetSample();
    }
}