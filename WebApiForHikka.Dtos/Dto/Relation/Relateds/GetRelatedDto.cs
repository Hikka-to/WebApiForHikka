using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.Relateds;

[ExportTsInterface]
public class GetRelatedDto : ModelDto
{
    public required Guid AnimeId { get; set; }
    public required Guid AnimeGroupId { get; set; }
    public required Guid RelatedTypeId { get; set; }
}