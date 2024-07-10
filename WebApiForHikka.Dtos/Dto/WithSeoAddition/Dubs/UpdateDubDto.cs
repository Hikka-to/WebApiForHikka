using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;

[ModelMetadataType(typeof(Dub))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Dubs")]
public class UpdateDubDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public string? Icon { get; set; }
}