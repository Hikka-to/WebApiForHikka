using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Formats;

[ModelMetadataType(typeof(Format))]
[ExportTsInterface]
public class CreateFormatDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
}