using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;

[ExportTsInterface]
public class GetAnimeDto : GetDtoWithSeoAddition
{
    public required Guid KindId { get; set; }
    public required Guid StatusId { get; set; }
    public required Guid PeriodId { get; set; }
    public required Guid RestrictedRatingId { get; set; }
    public required Guid SourceId { get; set; }
    public required string Name { get; set; }
    public string? ImageName { get; set; }
    public string? RomajiName { get; set; }
    public required string NativeName { get; set; }
    public required string PosterPath { get; set; }
    public required List<int> PosterColors { get; set; }
    public required float AvgDuration { get; set; }
    public required int HowManyEpisodes { get; set; }
    public required DateTime FirstAirDate { get; set; }
    public required DateTime LastAirDate { get; set; }
    public long? TmdbId { get; set; }
    public long? ShikimoriId { get; set; }
    public required float ShikimoriScore { get; set; }
    public required float TmdbScore { get; set; }
    public required float ImdbScore { get; set; }
    public required bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }

    public required DateTime UpdatedAt { get; set; }
    public required DateTime CreatedAt { get; set; }
}