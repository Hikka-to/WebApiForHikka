using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.RestrictedRatings;

[ModelMetadataType(typeof(RestrictedRating))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/RestrictedRatings")]
public class UpdateRestrictedRatingDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }
    public required int Value { get; set; }

    public required string Hint { get; set; }

    public string? Icon { get; set; }
}