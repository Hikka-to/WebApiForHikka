using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Comments;

[ExportTsInterface]
public class GetCommentDto : ModelDto
{
    public required string Body { get; set; }
    public required GetUserDto User { get; set; }
    public required Guid ParentId { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
}