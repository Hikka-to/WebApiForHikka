using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Formats;

[ExportTsInterface]
public class GetFormatDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}