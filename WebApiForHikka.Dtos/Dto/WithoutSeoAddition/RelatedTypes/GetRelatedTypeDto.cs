using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.RelatedTypes;


[ExportTsInterface]
public class GetRelatedTypeDto : ModelDto
{
    public required string Name { get; set; }
}