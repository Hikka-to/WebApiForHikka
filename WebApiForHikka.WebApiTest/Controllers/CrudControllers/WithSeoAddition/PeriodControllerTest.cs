using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Periods;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Periods;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class PeriodControllerTest : CrudControllerBaseWithSeoAddition<
    PeriodController,
    PeriodService,
    Period,
    IPeriodRepository,
    UpdatePeriodDto,
    CreatePeriodDto,
    GetPeriodDto,
    ReturnPageDto<GetPeriodDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new PeriodRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new PeriodService(formatRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }


    protected override async Task<PeriodController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in PeriodControllerTest");

        return new PeriodController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
        );
    }


    protected override CreatePeriodDto GetCreateDtoSample()
    {
        return GetPeriodModels.GetCreateDtoSample();
    }

    protected override GetPeriodDto GetGetDtoSample()
    {
        return GetPeriodModels.GetGetDtoSample();
    }

    protected override Period GetModelSample()
    {
        return GetPeriodModels.GetSample();
    }

    protected override UpdatePeriodDto GetUpdateDtoSample()
    {
        return GetPeriodModels.GetUpdateDtoSample();
    }
}