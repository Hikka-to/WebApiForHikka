using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Reviews;
using WebApiForHikka.SharedModels.Models.Relation;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetReviewModels
{
    public static Review GetSample()
    {
        return new Review
        {
            AnimeRating = GetAnimeRatingModels.GetSample(),
            Name = "SampleReview",
            Body = "This is a sample review body.",
            UpdatedAt = DateTime.Now,
            CreatedAt = DateTime.Now.AddDays(-10),
            RemovedAt = DateTime.Now.AddDays(5)
        };
    }

    public static Review GetSampleForUpdate()
    {
        return new Review
        {
            AnimeRating = GetAnimeRatingModels.GetSample(),
            Name = "UpdatedReview",
            Body = "This is an updated review body.",
            UpdatedAt = DateTime.Now,
            CreatedAt = DateTime.Now.AddDays(-20),
            RemovedAt = DateTime.Now.AddDays(10)
        };
    }

    public static CreateReviewDto GetCreateSampleDto()
    {
        return new CreateReviewDto
        {
            AnimeRatingId = Guid.NewGuid(),
            Name = "NewReview",
            Body = "This is a new review body.",
            UpdatedAt = DateTime.Now,
            CreatedAt = DateTime.Now.AddDays(-5),
            RemovedAt = DateTime.Now.AddDays(15)
        };
    }

    public static GetReviewDto GetGetDtoSample()
    {
        return new GetReviewDto
        {
            AnimeRating = GetAnimeRatingModels.GetGetDtoSample(),
            Name = "DtoReview",
            Body = "This is a review DTO body.",
            UpdatedAt = DateTime.Now,
            CreatedAt = DateTime.Now.AddDays(-30),
            RemovedAt = DateTime.Now.AddDays(20)
        };
    }

    public static UpdateReviewDto GetUpdateDtoSample()
    {
        return new UpdateReviewDto
        {
            AnimeRatingId = Guid.NewGuid(),
            Name = "UpdatedDtoReview",
            Body = "This is an updated review DTO body.",
            UpdatedAt = DateTime.Now,
            CreatedAt = DateTime.Now.AddDays(-40),
            RemovedAt = DateTime.Now.AddDays(25)
        };
    }
}