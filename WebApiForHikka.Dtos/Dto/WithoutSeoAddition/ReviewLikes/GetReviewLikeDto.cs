using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Reviews;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ReviewLikes;

[ExportTsInterface]
public class GetReviewLikeDto
{
    public required GetReviewDto Review { get; set; }

    public required GetUserDto User { get; set; }

    public required bool IsLiked { get; set; }
}