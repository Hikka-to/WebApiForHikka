using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Periods;

[ModelMetadataType(typeof(Period))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Periods")]
public class CreatePeriodDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
}