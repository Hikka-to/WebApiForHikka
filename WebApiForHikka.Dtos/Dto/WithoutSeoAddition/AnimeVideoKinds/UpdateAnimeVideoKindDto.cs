using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;

[ModelMetadataType(typeof(AnimeVideoKind))]
[ExportTsClass(OutputDir = "./../admin-panel-hikka/models/Dto/WithoutSeoAddition/AnimeVideoKinds")]
public class UpdateAnimeVideoKindDto : ModelDto
{
    public required string Name { get; set; }
}