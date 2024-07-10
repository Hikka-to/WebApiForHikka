using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Statuses;

[ModelMetadataType(typeof(Status))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Statuses")]
public class UpdateStatusDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }
}