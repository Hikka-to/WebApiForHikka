using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReportTypes;

[ExportTsInterface]
public class GetCommentReportTypeDto : ModelDto
{
    public required string Slug { get; set; }
}