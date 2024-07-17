using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.RestrictedRatings;

[ModelMetadataType(typeof(RestrictedRating))]
[ExportTsInterface]
public class CreateRestrictedRatingDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
    public required int Value { get; set; }

    public required string Hint { get; set; }

    public string? Icon { get; set; }
}