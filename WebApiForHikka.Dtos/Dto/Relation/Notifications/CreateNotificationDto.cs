using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.Relation.Notifications;

[ExportTsInterface]
public class CreateNotificationDto
{
    [EntityValidation<User>] public required Guid UserId { get; set; }
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }
    [EntityValidation<Resource>] public required Guid ResourceId { get; set; }
}