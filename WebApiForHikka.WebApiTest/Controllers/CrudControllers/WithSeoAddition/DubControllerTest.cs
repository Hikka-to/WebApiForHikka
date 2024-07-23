using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

internal class DubControllerTest : CrudControllerBaseWithSeoAddition<
    DubController,
    DubService,
    Dub,
    IDubRepository,
    UpdateDubDto,
    CreateDubDto,
    GetDubDto,
    ReturnPageDto<GetDubDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var countryRepository = new DubRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new DubService(countryRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override ICollection<Dub> GetCollectionOfModels(int howMany)
    {
        ICollection<Dub> seoAdditions = new List<Dub>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<DubController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in DubControllerTest");

        return new DubController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
        );
    }


    protected override CreateDubDto GetCreateDtoSample()
    {
        return GetDubModels.GetCreateDtoSample();
    }

    protected override GetDubDto GetGetDtoSample()
    {
        return GetDubModels.GetGetDtoSample();
    }

    protected override Dub GetModelSample()
    {
        return GetDubModels.GetSample();
    }

    protected override UpdateDubDto GetUpdateDtoSample()
    {
        return GetDubModels.GetUpdateDtoSample();
    }
}