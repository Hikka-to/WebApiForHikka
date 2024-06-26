using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.Animes;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Animes")]
public class CreateAnimeDto : CreateDtoWithSeoAddition
{
    [KindValidation]
    public required Guid KindId { get; set; }
    [StatusValidation]
    public required Guid StatusId { get; set; }
    [PeriodValidation]
    public required Guid PeriodId { get; set; }
    [RestrictedRatingValidation]
    public required Guid RestrictedRatingId { get; set; }
    [SourceValidation]
    public required Guid SourceId { get; set; }

    [StringLength(AnimeNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(AnimeNumberConstants.ImageNameLenght)]
    public string? ImageName { get; set; }

    [StringLength(AnimeNumberConstants.RomajiNameLenght)]
    public string? RomajiName { get; set; }

    [StringLength(AnimeNumberConstants.NativeNameLenght)]
    public required string NativeName { get; set; }

    [StringLength(AnimeNumberConstants.PosterPathLenght)]
    public required string PosterPath { get; set; }

    public required ICollection<int> PosterColors { get; set; }

    [Range(0, float.MaxValue)]
    public required float AvgDuration { get; set; }

    [Range(0, int.MaxValue)]
    public required int HowManyEpisodes { get; set; }

    public required DateTime FirstAirDate { get; set; }
    public required DateTime LastAirDate { get; set; }

    public long? TmdbId { get; set; }
    public long? ShikimoriId { get; set; }

    [Range(AnimeNumberConstants.LowestScore, AnimeNumberConstants.MaxScore)]
    public required float ShikimoriScore { get; set; }

    [Range(AnimeNumberConstants.LowestScore, AnimeNumberConstants.MaxScore)]
    public required float TmdbScore { get; set; }

    [Range(AnimeNumberConstants.LowestScore, AnimeNumberConstants.MaxScore)]
    public required float ImdbScore { get; set; }

    public required bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }

    public required DateTime UpdatedAt { get; set; }
    public required DateTime CreatedAt { get; set; }
}
