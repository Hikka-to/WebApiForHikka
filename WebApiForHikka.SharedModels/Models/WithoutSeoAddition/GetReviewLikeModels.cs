using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ReviewLikes;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetReviewLikeModels
{
    public static ReviewLike GetSample()
    {
        return new ReviewLike
        {
            Review = GetReviewModels.GetSample(),
            User = GetUserModels.GetSample(),
            IsLiked = true
        };
    }

    public static ReviewLike GetSampleForUpdate()
    {
        return new ReviewLike
        {
            Review = GetReviewModels.GetSample(),
            User = GetUserModels.GetSample(),
            IsLiked = false
        };
    }

    public static CreateReviewLikeDto GetCreateSampleDto()
    {
        return new CreateReviewLikeDto
        {
            ReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsLiked = false
        };
    }

    public static GetReviewLikeDto GetGetDtoSample()
    {
        return new GetReviewLikeDto
        {
            Review = GetReviewModels.GetGetDtoSample(),
            User = GetUserModels.GetGetDtoSample(),
            IsLiked = true
        };
    }

    public static UpdateReviewLikeDto GetUpdateDtoSample()
    {
        return new UpdateReviewLikeDto
        {
            ReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsLiked = false,
            Id = Guid.NewGuid()
        };
    }
}