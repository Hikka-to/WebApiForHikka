using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AlternativeNames;

[ModelMetadataType(typeof(AlternativeName))]
[ExportTsInterface]
public class UpdateAlternativeNameDto : ModelDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }

    public required string Name { get; set; }
}