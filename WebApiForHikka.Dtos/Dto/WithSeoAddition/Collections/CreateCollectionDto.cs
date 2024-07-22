using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Collections;

[ModelMetadataType(typeof(Collection))]
[ExportTsInterface]
public class CreateCollectionDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}