using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Seasons;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class SeasonControllerTest : CrudControllerBaseTest<
    SeasonController,
    SeasonService,
    Season,
    ISeasonRepository,
    UpdateSeasonDto,
    CreateSeasonDto,
    GetSeasonDto,
    ReturnPageDto<GetSeasonDto>
>

{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new SeasonRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new SeasonService(repository), userManager, roleManager);
    }

    protected override ICollection<Season> GetCollectionOfModels(int howMany)
    {
        ICollection<Season> seoAdditions = new List<Season>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<SeasonController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new SeasonController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateSeasonDto GetCreateDtoSample()
    {
        return GetSeasonModels.GetCreateDtoSample();
    }

    protected override GetSeasonDto GetGetDtoSample()
    {
        return GetSeasonModels.GetGetDtoSample();
    }

    protected override Season GetModelSample()
    {
        return GetSeasonModels.GetSample();
    }

    protected override UpdateSeasonDto GetUpdateDtoSample()
    {
        return GetSeasonModels.GetUpdateDtoSample();
    }
}