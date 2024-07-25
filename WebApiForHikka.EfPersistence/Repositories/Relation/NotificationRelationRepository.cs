using WebApiForHikka.Application.Relation.Notifications;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class NotificationRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<Notification, User, Anime>(dbContext), INotificationRelationRepository;
