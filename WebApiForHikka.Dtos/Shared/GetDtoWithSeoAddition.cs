using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.SeoAdditions;

namespace WebApiForHikka.Dtos.Shared;

[ExportTsInterface]
public class GetDtoWithSeoAddition : ModelDto
{
    public required GetSeoAdditionDto SeoAddition { get; set; }
}