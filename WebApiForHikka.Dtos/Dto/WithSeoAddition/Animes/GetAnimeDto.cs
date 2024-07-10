using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.Kinds;
using WebApiForHikka.Dtos.Dto.Periods;
using WebApiForHikka.Dtos.Dto.RestrictedRatings;
using WebApiForHikka.Dtos.Dto.Sources;
using WebApiForHikka.Dtos.Dto.Statuses;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Animes")]
public class GetAnimeDto : GetDtoWithSeoAddition
{
    public required GetKindDto Kind { get; set; }
    public required GetStatusDto Status { get; set; }
    public required GetPeriodDto Period { get; set; }
    public required GetRestrictedRatingDto RestrictedRating { get; set; }
    public required GetSourceDto Source { get; set; }

    public required List<GetTagDto> Tags { get; set; }
    public required string Name { get; set; }
    public string? ImageName { get; set; }
    public string? RomajiName { get; set; }
    public required string NativeName { get; set; }
    public required string PosterPathUrl { get; set; }
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