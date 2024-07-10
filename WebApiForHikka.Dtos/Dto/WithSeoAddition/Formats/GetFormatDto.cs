using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Formats")]
public class GetFormatDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}