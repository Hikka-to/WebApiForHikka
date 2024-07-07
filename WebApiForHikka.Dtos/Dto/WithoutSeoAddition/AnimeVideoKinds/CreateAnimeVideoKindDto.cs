using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;

[ModelMetadataType(typeof(AnimeVideoKind))]
[ExportTsClass(OutputDir = "./TS/Dto/WithoutSeoAddition/AnimeVideoKinds")]
public class CreateAnimeVideoKindDto
{
    public required string Name { get; set; }
}