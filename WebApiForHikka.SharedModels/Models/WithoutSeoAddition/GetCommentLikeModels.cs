using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentLikes;
using Boolean = Faker.Boolean;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetCommentLikeModels
{
    public static CommentLike GetSample()
    {
        return new CommentLike
        {
            Comment = GetCommentModels.GetSample(),
            User = GetUserModels.GetSample(),
            IsLiked = true
        };
    }

    public static CommentLike GetSampleForUpdate()
    {
        return new CommentLike
        {
            Comment = GetCommentModels.GetSampleForUpdate(),
            User = GetUserModels.GetSampleForUpdate(),
            IsLiked = false
        };
    }

    public static CreateCommentLikeDto GetCreateDtoSample()
    {
        return new CreateCommentLikeDto
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsLiked = Boolean.Random()
        };
    }

    public static UpdateCommentLikeDto GetUpdateDtoSample()
    {
        return new UpdateCommentLikeDto
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsLiked = Boolean.Random(),
            Id = Guid.NewGuid()
        };
    }

    public static GetCommentLikeDto GetGetDtoSample()
    {
        return new GetCommentLikeDto
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsLiked = Boolean.Random(),
            Id = Guid.NewGuid()
        };
    }
}