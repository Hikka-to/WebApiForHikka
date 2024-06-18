using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.SeoAddition;

namespace WebApiForHikka.Dtos.Shared;

[ExportTsInterface(OutputDir = "./TS/Shared/")]
public class CreateDtoWithSeoAddition
{
    public required CreateSeoAdditionDto SeoAddition { get; set; }
}