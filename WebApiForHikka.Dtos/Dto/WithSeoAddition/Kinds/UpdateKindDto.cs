using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Kinds;

[ModelMetadataType(typeof(Kind))]
[ExportTsInterface]
public class UpdateKindDto : UpdateDtoWithSeoAddition
{
    public required string Slug { get; set; }

    public required string Hint { get; set; }
}