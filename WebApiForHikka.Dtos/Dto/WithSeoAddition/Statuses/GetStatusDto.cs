using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Status;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Statuses")]
public class GetStatusDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}