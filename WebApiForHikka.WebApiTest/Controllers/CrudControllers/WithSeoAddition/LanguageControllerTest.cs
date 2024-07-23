using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Languages;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Languages;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class LanguageControllerTest : CrudControllerBaseWithSeoAddition<
    LanguageController,
    LanguageService,
    Language,
    ILanguageRepository,
    UpdateLanguageDto,
    CreateLanguageDto,
    GetLanguageDto,
    ReturnPageDto<GetLanguageDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new LanguageRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new LanguageService(formatRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override async Task<LanguageController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in LanguageControllerTest");

        return new LanguageController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override CreateLanguageDto GetCreateDtoSample()
    {
        return GetLanguageModels.GetCreateDtoSample();
    }

    protected override GetLanguageDto GetGetDtoSample()
    {
        return GetLanguageModels.GetGetDtoSample();
    }

    protected override Language GetModelSample()
    {
        return GetLanguageModels.GetSample();
    }

    protected override UpdateLanguageDto GetUpdateDtoSample()
    {
        return GetLanguageModels.GetUpdateDtoSample();
    }
}