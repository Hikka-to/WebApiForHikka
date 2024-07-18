using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ExternalLinks;

[ModelMetadataType(typeof(ExternalLink))]
[ExportTsInterface]
public class UpdateExternalLinkDto : ModelDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }
    public required string Url { get; set; }
}