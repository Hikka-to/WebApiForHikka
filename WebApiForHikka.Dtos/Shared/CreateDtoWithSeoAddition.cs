using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.SeoAdditions;

namespace WebApiForHikka.Dtos.Shared;

[ExportTsInterface]
public class CreateDtoWithSeoAddition
{
    public required CreateSeoAdditionDto SeoAddition { get; set; }
}