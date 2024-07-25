using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.AnimeRatings;

[ModelMetadataType(typeof(AnimeRating))]
[ExportTsInterface]
public class UpdateAnimeRatingDto : ModelDto
{
    [EntityValidation<Review>] public required Guid ReviewId { get; set; }
    [EntityValidation<User>] public required Guid UserId { get; set; }
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }

    public required int Number { get; set; }
}