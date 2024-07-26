using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Sources;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Sources;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class SourceControllerTest : CrudControllerBaseWithSeoAddition<
    SourceController,
    SourceService,
    Source,
    ISourceRepository,
    UpdateSourceDto,
    CreateSourceDto,
    GetSourceDto,
    ReturnPageDto<GetSourceDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new SourceRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new SourceService(formatRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }


    protected override async Task<SourceController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in SourceControllerTest");

        return new SourceController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateSourceDto GetCreateDtoSample()
    {
        return GetSourceModels.GetCreateDtoSample();
    }

    protected override GetSourceDto GetGetDtoSample()
    {
        return GetSourceModels.GetGetDtoSample();
    }

    protected override Source GetModelSample()
    {
        return GetSourceModels.GetSample();
    }

    protected override UpdateSourceDto GetUpdateDtoSample()
    {
        return GetSourceModels.GetUpdateDtoSample();
    }
}