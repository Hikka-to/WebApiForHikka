using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Comments;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetCommentModels
{
    public static Comment GetSample()
    {
        return new Comment
        {
            Body = "Test",
            User = GetUserModels.GetSample(),
            Parent = GetAnimeModels.GetSample(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static Comment GetSampleForUpdate()
    {
        return new Comment
        {
            Body = "Test1",
            User = GetUserModels.GetSampleForUpdate(),
            Parent = GetEpisodeModels.GetSample(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static CreateCommentDto GetCreateDtoSample()
    {
        return new CreateCommentDto
        {
            Body = Lorem.GetFirstWord(),
            UserId = Guid.NewGuid(),
            ParentId = Guid.NewGuid()
        };
    }

    public static UpdateCommentDto GetUpdateDtoSample()
    {
        return new UpdateCommentDto
        {
            Body = Lorem.GetFirstWord(),
            UserId = Guid.NewGuid(),
            ParentId = Guid.NewGuid(),
            Id = Guid.NewGuid()
        };
    }

    public static GetCommentDto GetGetDtoSample()
    {
        return new GetCommentDto
        {
            Body = Lorem.GetFirstWord(),
            User = GetUserModels.GetGetDtoSample(),
            ParentId = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}