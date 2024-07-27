using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Resources;

[ModelMetadataType(typeof(Resource))]
[ExportTsInterface]
public class CreateResourceDto
{
    public required string Slug { get; set; }
}