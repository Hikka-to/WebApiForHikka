using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ExternalLinks;

[ModelMetadataType(typeof(ExternalLink))]
[ExportTsInterface]
public class CreateExternalLinkDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }
    public required string Url { get; set; }
}