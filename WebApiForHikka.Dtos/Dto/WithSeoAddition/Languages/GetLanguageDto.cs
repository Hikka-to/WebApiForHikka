using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Languages;

[ExportTsInterface]
public class GetLanguageDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
    public required string Locale { get; set; }
    public required string Icon { get; set; }
}