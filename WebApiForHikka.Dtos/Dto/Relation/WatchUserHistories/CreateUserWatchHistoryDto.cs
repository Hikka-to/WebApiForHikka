using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.Relation.WatchUserHistories;

[MetadataType(typeof(UserWatchHistory))]
[ExportTsInterface]
public class CreateUserWatchHistoryDto
{
    [EntityValidation<User>] public required Guid UserId { get; set; }
    [EntityValidation<Episode>] public required Guid EpisodeId { get; set; }

    public required int ProgressTime { get; set; }
}