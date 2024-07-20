using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAdditions;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers;

namespace WebApiForHikka.Test.Controllers.CrudControllers;

public class SeoAdditionControllerTest : CrudControllerBaseTest<
    SeoAdditionController,
    SeoAdditionService,
    SeoAddition,
    ISeoAdditionRepository,
    UpdateSeoAdditionDto,
    CreateSeoAdditionDto,
    GetSeoAdditionDto,
    ReturnPageDto<GetSeoAdditionDto>
>
{
    protected override async Task<SeoAdditionController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        return new SeoAdditionController(
            allServicesInController.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override CreateSeoAdditionDto GetCreateDtoSample()
    {
        return GetSeoAdditionModels.GetCreateDtoSample();
    }

    protected override SeoAddition GetModelSample()
    {
        return GetSeoAdditionModels.GetSample();
    }

    protected override UpdateSeoAdditionDto GetUpdateDtoSample()
    {
        return GetSeoAdditionModels.GetUpdateDtoSample();
    }

    protected override GetSeoAdditionDto GetGetDtoSample()
    {
        return GetSeoAdditionModels.GetGetDtoSample();
    }

    protected override ICollection<SeoAddition> GetCollectionOfModels(int howMany)
    {
        ICollection<SeoAddition> seoAdditions = new List<SeoAddition>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }


    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var db = GetDatabaseContext();
        var res = new SeoAdditionRepository(db);
        var userManager = GetUserManager(db);
        var roleManager = GetRoleManager(db);

        return new AllServicesInController(new SeoAdditionService(res), userManager, roleManager);
    }
}