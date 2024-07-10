using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;

[ModelMetadataType(typeof(Mediaplayer))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithoutSeoAddition/Mediaplayers")]
public class CreateMediaplayerDto
{
    public required string Name { get; set; }

    public required string Icon { get; set; }
}