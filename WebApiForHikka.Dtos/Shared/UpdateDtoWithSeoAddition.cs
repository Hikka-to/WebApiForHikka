using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.SeoAddition;

namespace WebApiForHikka.Dtos.Shared;


[ExportTsInterface(OutputDir = "./TS/Shared/")]
public class UpdateDtoWithSeoAddition : ModelDto
{
    public required UpdateSeoAdditionDto SeoAddition { get; set; }
}