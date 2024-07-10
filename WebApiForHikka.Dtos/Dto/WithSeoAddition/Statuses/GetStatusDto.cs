using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Statuses;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Statuses")]
public class GetStatusDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}