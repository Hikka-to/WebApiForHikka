using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Comments;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReports;

[ExportTsInterface]
public class GetCommentReportDto : ModelDto
{
    public required GetCommentDto Comment { get; set; }
    public required GetUserDto User { get; set; }
    public required GetCommentReportTypeDto CommentReportType { get; set; }
    public required string Body { get; set; }
}