using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.Notifications;

public class GetNotificationDto : ModelDto
{
    public required Guid UserId { get; set; }
    public required Guid AnimeId { get; set; }
    public required Guid ResourceId { get; set; }

    public required DateTime CreatedAt { get; set; }

}
