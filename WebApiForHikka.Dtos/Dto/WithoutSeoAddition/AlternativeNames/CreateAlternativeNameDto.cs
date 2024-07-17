using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AlternativeNames;

[ModelMetadataType(typeof(AlternativeName))]
[ExportTsInterface]
public class CreateAlternativeNameDto
{
    [AnimeValidation] public required Guid AnimeId { get; set; }

    public required string Name { get; set; }
}