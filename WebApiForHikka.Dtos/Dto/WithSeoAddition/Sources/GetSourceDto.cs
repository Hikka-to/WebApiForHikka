using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Sources;


[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Sources")]
public class GetSourceDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}