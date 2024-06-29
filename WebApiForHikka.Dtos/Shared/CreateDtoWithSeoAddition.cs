using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.SeoAdditions;

namespace WebApiForHikka.Dtos.Shared;

[ExportTsInterface(OutputDir = "./TS/Shared/")]
public class CreateDtoWithSeoAddition
{
    public required CreateSeoAdditionDto SeoAddition { get; set; }
}