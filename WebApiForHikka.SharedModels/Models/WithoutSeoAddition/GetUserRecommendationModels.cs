using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserRecommendations;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetUserRecommendationModels
{
    public static UserRecommendation GetSample()
    {
        return new UserRecommendation
        {
            Anime = GetAnimeModels.GetSample(),
            User = GetUserModels.GetSample(),
            Description = "Sample user recommendation",
            CreatedAt = DateTime.Now.AddDays(-30),
            UpdatedAt = DateTime.Now.AddDays(-25)
        };
    }

    public static UserRecommendation GetSampleForUpdate()
    {
        return new UserRecommendation
        {
            Anime = GetAnimeModels.GetSample(),
            User = GetUserModels.GetSample(),
            Description = "Updated user recommendation",
            CreatedAt = DateTime.Now.AddDays(-20),
            UpdatedAt = DateTime.Now.AddDays(-15)
        };
    }

    public static CreateUserRecommendationDto GetCreateSampleDto()
    {
        return new CreateUserRecommendationDto
        {
            AnimeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Description = "New user recommendation",
            CreatedAt = DateTime.Now.AddDays(-10),
            UpdatedAt = DateTime.Now.AddDays(-10)
        };
    }

    public static GetUserRecommendationDto GetGetDtoSample()
    {
        return new GetUserRecommendationDto
        {
            Anime = GetAnimeModels.GetGetDtoSample(),
            User = GetUserModels.GetGetDtoSample(),
            Description = "Retrieved user recommendation",
            CreatedAt = DateTime.Now.AddDays(-5),
            UpdatedAt = DateTime.Now.AddDays(-3)
        };
    }

    public static UpdateUserRecommendationDto GetUpdateDtoSample()
    {
        return new UpdateUserRecommendationDto
        {
            AnimeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Description = "User recommendation to be updated",
            CreatedAt = DateTime.Now.AddDays(-2),
            UpdatedAt = DateTime.Now.AddDays(-1)
        };
    }
}