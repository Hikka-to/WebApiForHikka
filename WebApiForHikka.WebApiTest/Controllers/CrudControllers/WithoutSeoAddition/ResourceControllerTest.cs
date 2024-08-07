using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.Resources;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Resources;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class ResourceControllerTest : CrudControllerBaseTest<
    ResourceController,
    ResourceService,
    Resource,
    IResourceRepository,
    UpdateResourceDto,
    CreateResourceDto,
    GetResourceDto,
    ReturnPageDto<GetResourceDto>
>

{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new ResourceRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new ResourceService(repository), userManager, roleManager);
    }

    protected override ICollection<Resource> GetCollectionOfModels(int howMany)
    {
        ICollection<Resource> seoAdditions = new List<Resource>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<ResourceController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new ResourceController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateResourceDto GetCreateDtoSample()
    {
        return GetResourceModels.GetCreateDtoSample();
    }

    protected override GetResourceDto GetGetDtoSample()
    {
        return GetResourceModels.GetGetDtoSample();
    }

    protected override Resource GetModelSample()
    {
        return GetResourceModels.GetSample();
    }

    protected override UpdateResourceDto GetUpdateDtoSample()
    {
        return GetResourceModels.GetUpdateDtoSample();
    }
}