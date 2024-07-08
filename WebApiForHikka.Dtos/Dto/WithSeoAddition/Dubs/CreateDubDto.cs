using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;

[ModelMetadataType(typeof(Dub))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Dubs")]
public class CreateDubDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public string? Icon { get; set; }
}