using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Collections;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Collections;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class CollectionControllerTest : CrudControllerBaseWithSeoAddition<
    CollectionController,
    CollectionService,
    Collection,
    ICollectionRepository,
    UpdateCollectionDto,
    CreateCollectionDto,
    GetCollectionDto,
    ReturnPageDto<GetCollectionDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var collectionRepository = new CollectionRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new CollectionService(collectionRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override ICollection<Collection> GetCollectionOfModels(int howMany)
    {
        var seoAdditions = new List<Collection>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<CollectionController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in CollectionControllerTest");

        return new CollectionController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
        );
    }

    protected override Collection GetModelSample()
    {
        return GetCollectionModels.GetSample();
    }

    protected override CreateCollectionDto GetCreateDtoSample()
    {
        return GetCollectionModels.GetCreateDtoSample();
    }

    protected override GetCollectionDto GetGetDtoSample()
    {
        return GetCollectionModels.GetGetDtoSample();
    }

    protected override UpdateCollectionDto GetUpdateDtoSample()
    {
        return GetCollectionModels.GetUpdateDtoSample();
    }
}