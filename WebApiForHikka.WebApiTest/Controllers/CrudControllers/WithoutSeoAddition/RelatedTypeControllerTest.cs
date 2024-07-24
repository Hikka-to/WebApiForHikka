using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class RelatedTypeControllerTest : CrudControllerBaseTest<
    RelatedTypeController,
    RelatedTypeService,
    RelatedType,
    IRelatedTypeRepository,
    UpdateRelatedTypeDto,
    CreateRelatedTypeDto,
    GetRelatedTypeDto,
    ReturnPageDto<GetRelatedTypeDto>
>

{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new RelatedTypeRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new RelatedTypeService(repository), userManager, roleManager);
    }

    protected override ICollection<RelatedType> GetCollectionOfModels(int howMany)
    {
        ICollection<RelatedType> seoAdditions = new List<RelatedType>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<RelatedTypeController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new RelatedTypeController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateRelatedTypeDto GetCreateDtoSample()
    {
        return GetRelatedTypeModels.GetCreateDtoSample();
    }

    protected override GetRelatedTypeDto GetGetDtoSample()
    {
        return GetRelatedTypeModels.GetGetDtoSample();
    }

    protected override RelatedType GetModelSample()
    {
        return GetRelatedTypeModels.GetModelSample();
    }

    protected override UpdateRelatedTypeDto GetUpdateDtoSample()
    {
        return GetRelatedTypeModels.GetUpdateDtoSample();
    }
}