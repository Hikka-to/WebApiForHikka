using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class NotificationRepositoryTest : SharedRelationRepositoryTest<
    Notification, User, Anime,
    NotificationRelationRepository
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

    protected override NotificationRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new NotificationRelationRepository(hikkaDbContext);
    }
}