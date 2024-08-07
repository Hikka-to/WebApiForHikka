using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.Notifications;
using WebApiForHikka.Application.WithoutSeoAddition.Resources;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.Notifications;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.Relation;

namespace WebApiForHikka.Test.Controllers.CrudControllers.Relation;

public class NotificationControllerTest : CrudControllerBaseTest<
    NotificationController,
    NotificationRelationService,
    Notification,
    INotificationRelationRepository,
    UpdateNotificationDto,
    CreateNotificationDto,
    GetNotificationDto,
    ReturnPageDto<GetNotificationDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new NotificationRelationRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);

        alternativeServices.AddSingleton<IResourceRepository, ResourceRepository>();
        alternativeServices.AddSingleton<IResourceService, ResourceService>();

        return new AllServicesInController(new NotificationRelationService(repository), userManager, roleManager);
    }

    protected override ICollection<Notification> GetCollectionOfModels(int howMany)
    {
        ICollection<Notification> seoAdditions = new List<Notification>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<NotificationController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        return new NotificationController(
            allServicesInController.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IResourceService>()
        );
    }

    protected override void MutationBeforeDtoCreation(CreateNotificationDto createDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
    }

    protected override void MutationBeforeDtoUpdate(UpdateNotificationDto updateDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
    }

    protected override CreateNotificationDto GetCreateDtoSample()
    {
        return GetNotificationModels.GetCreateDtoSample();
    }

    protected override GetNotificationDto GetGetDtoSample()
    {
        return GetNotificationModels.GetGetDtoSample();
    }

    protected override UpdateNotificationDto GetUpdateDtoSample()
    {
        return GetNotificationModels.GetUpdateDtoSample();
    }

    protected override Notification GetModelSample()
    {
        return GetNotificationModels.GetSample();
    }
}