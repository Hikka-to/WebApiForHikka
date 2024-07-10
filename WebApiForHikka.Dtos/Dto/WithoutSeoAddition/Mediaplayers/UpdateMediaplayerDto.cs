using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;

[ModelMetadataType(typeof(Mediaplayer))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithoutSeoAddition/Mediaplayers")]
public class UpdateMediaplayerDto : ModelDto
{
    public required string Name { get; set; }

    public required string Icon { get; set; }
}