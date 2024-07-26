using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Kinds;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Kinds;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class KindControllerTest : CrudControllerBaseWithSeoAddition<
    KindController,
    KindService,
    Kind,
    IKindRepository,
    UpdateKindDto,
    CreateKindDto,
    GetKindDto,
    ReturnPageDto<GetKindDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new KindRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new KindService(formatRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }


    protected override async Task<KindController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in KindControllerTest");

        return new KindController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateKindDto GetCreateDtoSample()
    {
        return GetKindModels.GetCreateDtoSample();
    }

    protected override GetKindDto GetGetDtoSample()
    {
        return GetKindModels.GetGetDtoSample();
    }

    protected override Kind GetModelSample()
    {
        return GetKindModels.GetSample();
    }

    protected override UpdateKindDto GetUpdateDtoSample()
    {
        return GetKindModels.GetUpdateDtoSample();
    }
}