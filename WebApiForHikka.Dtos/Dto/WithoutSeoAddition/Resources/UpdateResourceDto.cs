using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Resources;


[ModelMetadataType(typeof(Resource))]
[ExportTsInterface]
public class UpdateResourceDto : ModelDto
{
    public required string Slug { get; set; }   
}
