using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeGroups;


[ModelMetadataType(typeof(AnimeGroup))]
[ExportTsInterface]
public class UpdateAnimeGroupDto : ModelDto
{
    public required string Name { get; set; }
}
