using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.Seasons;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.Relation;

namespace WebApiForHikka.Test.Controllers.CrudControllers.Relation;

public class SeasonControllerTest : CrudControllerBaseTest<
    SeasonController,
    SeasonRelationService,
    Season,
    ISeasonRelationRepository,
    UpdateSeasonDto,
    CreateSeasonDto,
    GetSeasonDto,
    ReturnPageDto<GetSeasonDto>
>

{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new SeasonRelationRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new SeasonRelationService(repository), userManager, roleManager);
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
            Mapper,
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