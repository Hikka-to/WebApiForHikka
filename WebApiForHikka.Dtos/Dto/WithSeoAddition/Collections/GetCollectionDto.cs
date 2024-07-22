using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Collections;

[ExportTsInterface]
public class GetCollectionDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}