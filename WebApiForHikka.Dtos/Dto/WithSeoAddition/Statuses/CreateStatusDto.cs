using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Statuses;

[ModelMetadataType(typeof(Status))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Statuses")]
public class CreateStatusDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
}