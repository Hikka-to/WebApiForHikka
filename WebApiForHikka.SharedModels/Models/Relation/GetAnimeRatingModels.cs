using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.AnimeRatings;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public static class GetAnimeRatingModels
{
    public static AnimeRating GetSample()
    {
        return new AnimeRating
        {
            FirstId = default,
            SecondId = default,
            First = GetUserModels.GetSample(),
            Second = GetAnimeModels.GetSampleWithoutManyToMany(),
            CreateAt = default,
            Number = 2,
            UpdateAt = default,
            RewiewId = Guid.NewGuid()
        };
    }

    public static AnimeRating GetSampleForUpdate()
    {
        return new AnimeRating
        {
            FirstId = default,
            SecondId = default,
            First = GetUserModels.GetSampleForUpdate(),
            Second = GetAnimeModels.GetSampleForUpdateWithoutManyToMany(),
            CreateAt = DateTime.Now,
            Number = 3,
            UpdateAt = default,
            RewiewId = Guid.NewGuid()
        };
    }

    public static CreateAnimeRatingDto GetCreateDtoSample()
    {
        return new CreateAnimeRatingDto
        {
            AnimeId = Guid.NewGuid(),
            Number = 1,
            UserId = Guid.NewGuid(),
            ReviewId = Guid.NewGuid()
        };
    }

    public static GetAnimeRatingDto GetGetDtoSample()
    {
        return new GetAnimeRatingDto
        {
            AnimeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Number = 2,
            Id = Guid.NewGuid()
        };
    }

    public static UpdateAnimeRatingDto GetUpdateDtoSample()
    {
        return new UpdateAnimeRatingDto
        {
            AnimeId = Guid.NewGuid(),
            Number = 3,
            UserId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            ReviewId = Guid.NewGuid()
        };
    }
}