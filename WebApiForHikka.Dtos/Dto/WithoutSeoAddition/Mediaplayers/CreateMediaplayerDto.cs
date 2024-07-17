using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;

[ModelMetadataType(typeof(Mediaplayer))]
[ExportTsInterface]
public class CreateMediaplayerDto
{
    public required string Name { get; set; }

    public required string Icon { get; set; }
}