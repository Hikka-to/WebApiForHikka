using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.WatchUserHistories;

[ExportTsInterface]
public class GetUserWatchHistoryDto : ModelDto
{
    public required GetUserDto User { get; set; }
    public required GetEpisodeDto Episode { get; set; }

    public required int ProgressTime { get; set; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }
}