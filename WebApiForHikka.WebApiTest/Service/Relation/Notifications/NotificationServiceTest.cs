using WebApiForHikka.Application.Relation.Notifications;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.Notifications;

public class NotificationServiceTest : SharedRelationServiceTest<
    Notification,
    NotificationRelationService,
    User,
    Anime
>
{
    protected override Notification GetSample()
    {
        return GetNotificationModels.GetSample();
    }

    protected override Notification GetSampleForUpdate()
    {
        return GetNotificationModels.GetSampleForUpdate();
    }

    protected override NotificationRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new NotificationRelationService(new NotificationRelationRepository(hikkaDbContext));
    }
}