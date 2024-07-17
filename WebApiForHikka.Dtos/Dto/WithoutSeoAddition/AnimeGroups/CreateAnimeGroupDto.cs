using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeGroups;


[ModelMetadataType(typeof(AnimeGroup))]
[ExportTsInterface]
public class CreateAnimeGroupDto
{
    public required string Name { get; set; }
}
