using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ReviewLikes;

public class UpdateReviewLikeDto : ModelDto
{
    [EntityValidation<Review>] public required Guid ReviewId { get; set; }

    [EntityValidation<User>] public required Guid UserId { get; set; }

    public required bool IsLiked { get; set; }
}