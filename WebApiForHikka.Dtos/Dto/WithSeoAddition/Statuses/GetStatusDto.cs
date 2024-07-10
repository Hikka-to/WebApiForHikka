using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Statuses;

[ExportTsInterface]
public class GetStatusDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}