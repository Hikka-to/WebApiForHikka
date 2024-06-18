using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Dubs")]
public class GetDubDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }

    public string? Icon { get; set; }
}
