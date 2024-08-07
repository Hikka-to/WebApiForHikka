using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.Notifications;

public class NotificationRelationService(INotificationRelationRepository notificationRelationRepository)
    : RelationCrudService<Notification, User, Anime, INotificationRelationRepository>(
        notificationRelationRepository), INotificationRelationService;