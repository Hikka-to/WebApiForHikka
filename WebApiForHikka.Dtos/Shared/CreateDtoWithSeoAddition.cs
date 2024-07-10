using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.SeoAdditions;

namespace WebApiForHikka.Dtos.Shared;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Shared/")]
public class CreateDtoWithSeoAddition
{
    public required CreateSeoAdditionDto SeoAddition { get; set; }
}