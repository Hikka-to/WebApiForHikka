using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Sources;

[ExportTsInterface]
public class GetSourceDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}