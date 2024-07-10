using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Periods;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Periods")]
public class GetPeriodDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}