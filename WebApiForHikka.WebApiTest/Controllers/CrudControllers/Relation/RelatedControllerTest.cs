using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.Relateds;
using WebApiForHikka.Application.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.Relateds;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.Relation;

namespace WebApiForHikka.Test.Controllers.CrudControllers.Relation;

public class RelatedControllerTest : CrudControllerBaseTest<
    RelatedController,
    RelatedRelationService,
    Related,
    IRelatedRelationRepository,
    UpdateRelatedDto,
    CreateRelatedDto,
    GetRelatedDto,
    ReturnPageDto<GetRelatedDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new RelatedRelationRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IRelatedTypeRepository, RelatedTypeRepository>();
        alternativeServices.AddSingleton<IRelatedTypeService, RelatedTypeService>();

        return new AllServicesInController(new RelatedRelationService(repository), userManager, roleManager);
    }

    protected override ICollection<Related> GetCollectionOfModels(int howMany)
    {
        ICollection<Related> seoAdditions = new List<Related>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<RelatedController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        return new RelatedController(
            allServicesInController.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IRelatedTypeService>()
        );
    }

    protected override void MutationBeforeDtoCreation(CreateRelatedDto createDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var relatedType = GetRelatedTypeModels.GetSample();

        var relatedTypeService = alternativeServices.GetRequiredService<IRelatedTypeService>();

        relatedTypeService.CreateAsync(relatedType, CancellationToken).Wait();

        createDto.RelatedTypeId = relatedType.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateRelatedDto updateDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var relatedType = GetRelatedTypeModels.GetSample();

        var relatedTypeService = alternativeServices.GetRequiredService<IRelatedTypeService>();

        relatedTypeService.CreateAsync(relatedType, CancellationToken).Wait();

        updateDto.RelatedTypeId = relatedType.Id;
    }

    protected override CreateRelatedDto GetCreateDtoSample()
    {
        return GetRelatedModels.GetCreateDtoSample();
    }

    protected override GetRelatedDto GetGetDtoSample()
    {
        return GetRelatedModels.GetGetDtoSample();
    }

    protected override UpdateRelatedDto GetUpdateDtoSample()
    {
        return GetRelatedModels.GetUpdateDtoSample();
    }

    protected override Related GetModelSample()
    {
        return GetRelatedModels.GetSample();
    }
}