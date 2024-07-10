using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Kinds;

[ModelMetadataType(typeof(Kind))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Kinds")]
public class UpdateKindDto : UpdateDtoWithSeoAddition
{
    public required string Slug { get; set; }

    public required string Hint { get; set; }
}