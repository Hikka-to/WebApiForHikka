using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Periods;

[ExportTsInterface]
public class GetPeriodDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}