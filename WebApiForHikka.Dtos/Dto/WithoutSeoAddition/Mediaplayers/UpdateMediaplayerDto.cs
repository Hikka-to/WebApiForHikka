using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;

[ModelMetadataType(typeof(Mediaplayer))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithoutSeoAddition/Mediaplayers")]
public class UpdateMediaplayerDto : ModelDto
{
    public required string Name { get; set; }

    public required string Icon { get; set; }
}