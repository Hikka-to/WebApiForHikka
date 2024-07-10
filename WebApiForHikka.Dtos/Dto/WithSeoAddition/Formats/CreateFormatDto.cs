using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

[ModelMetadataType(typeof(Format))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Formats")]
public class CreateFormatDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
}