using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Formats")]
public class GetFormatDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}