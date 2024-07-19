using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;

[ExportTsInterface]
public class GetEpisodeDto : GetDtoWithSeoAddition
{
    public Guid AnimeId { get; set; }

    public required string Name { get; set; }

    public required int Duration { get; set; }

    public required DateTime AirDate { get; set; }

    public required bool IsFiller { get; set; } = false;

    public required DateTime UpdateAt { get; set; }
    public required DateTime CreateAt { get; set; }
}
