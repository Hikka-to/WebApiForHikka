using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Kinds;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Kinds")]
public class GetKindDto : GetDtoWithSeoAddition
{
    public required string Slug { get; set; }

    public required string Hint { get; set; }
}