using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class AnimeGroupControllerTest : CrudControllerBaseTest<
    AnimeGroupController,
    AnimeGroupService,
    AnimeGroup,
    IAnimeGroupRepository,
    UpdateAnimeGroupDto,
    CreateAnimeGroupDto,
    GetAnimeGroupDto,
    ReturnPageDto<GetAnimeGroupDto>
>

{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new AnimeGroupRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new AnimeGroupService(repository), userManager, roleManager);
    }

    protected override ICollection<AnimeGroup> GetCollectionOfModels(int howMany)
    {
        ICollection<AnimeGroup> seoAdditions = new List<AnimeGroup>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<AnimeGroupController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new AnimeGroupController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateAnimeGroupDto GetCreateDtoSample()
    {
        return GetAnimeGroupModels.GetCreateDtoSample();
    }

    protected override GetAnimeGroupDto GetGetDtoSample()
    {
        return GetAnimeGroupModels.GetGetDtoSample();
    }

    protected override AnimeGroup GetModelSample()
    {
        return GetAnimeGroupModels.GetModelSample();
    }

    protected override UpdateAnimeGroupDto GetUpdateDtoSample()
    {
        return GetAnimeGroupModels.GetUpdateDtoSample();
    }
}