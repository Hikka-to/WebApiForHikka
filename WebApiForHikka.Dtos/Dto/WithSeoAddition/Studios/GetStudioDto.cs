using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;

[ExportTsInterface]
public class GetStudioDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
    public string? Logo { get; set; }
}