using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Periods;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Periods")]
public class GetPeriodDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}