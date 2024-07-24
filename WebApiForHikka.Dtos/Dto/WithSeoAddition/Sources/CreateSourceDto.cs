using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Sources;

[ModelMetadataType(typeof(Source))]
[ExportTsInterface]
public class CreateSourceDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
}