using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;

[ExportTsInterface]
public class GetDubDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }

    public string? Icon { get; set; }
}