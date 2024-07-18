using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;

[ModelMetadataType(typeof(Anime))]
public class UpdateAnimeDto : UpdateDtoWithSeoAddition
{
    [EntityValidation<Kind>] public required Guid KindId { get; set; }

    [EntityValidation<Status>] public required Guid StatusId { get; set; }

    [EntityValidation<Period>] public required Guid PeriodId { get; set; }

    [EntityValidation<RestrictedRating>] public required Guid RestrictedRatingId { get; set; }

    [EntityValidation<Source>] public required Guid SourceId { get; set; }

    [EntityValidation<Tag>] public required List<Guid> Tags { get; set; }
    [EntityValidation<Country>] public required List<Guid> Countries { get; set; }
    [EntityValidation<Dub>] public required List<Guid> Dubs { get; set; }

    public required string Name { get; set; }

    public string? ImageName { get; set; }

    public string? RomajiName { get; set; }

    public required string NativeName { get; set; }

    [FileContentType("image/*")]
    [MaxFileSize(SharedNumberConstatnts.MaxFileSize)]
    public required IFormFile PosterImage { get; set; }

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
}