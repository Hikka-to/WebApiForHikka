using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentLikes;

[ExportTsInterface]
public class GetCommentLikeDto : ModelDto
{
    public required Guid CommentId { get; set; }
    public required Guid UserId { get; set; }
    public bool IsLiked { get; set; }
}