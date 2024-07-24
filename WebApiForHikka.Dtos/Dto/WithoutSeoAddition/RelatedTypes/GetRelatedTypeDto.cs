using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.RelatedTypes;

[ExportTsInterface]
public class GetRelatedTypeDto : ModelDto
{
    public required string Name { get; set; }
}