using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.Relation.Seasons;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public static class GetSeasonModels
{
    public static Season GetSample()
    {
        return new Season
        {
            FirstId = default,
            SecondId = default,
            Second = GetAnimeGroupModels.GetSample(),
            First = GetAnimeModels.GetSampleWithoutManyToMany(),
            Name = "Test"
        };
    }

    public static Season GetSampleForUpdate()
    {
        return new Season
        {
            FirstId = default,
            SecondId = default,
            Second = GetAnimeGroupModels.GetSampleForUpdate(),
            First = GetAnimeModels.GetSampleForUpdateWithoutManyToMany(),
            Name = "Test1"
        };
    }

    public static CreateSeasonDto GetCreateDtoSample()
    {
        return new CreateSeasonDto
        {
            AnimeGroupId = Guid.NewGuid(),
            AnimeId = Guid.NewGuid(),
            Name = Lorem.GetFirstWord()
        };
    }

    public static GetSeasonDto GetGetDtoSample()
    {
        return new GetSeasonDto
        {
            Anime = GetAnimeModels.GetGetDtoSample(),
            AnimeGroup = GetAnimeGroupModels.GetGetDtoSample(),
            Name = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }

    public static UpdateSeasonDto GetUpdateDtoSample()
    {
        return new UpdateSeasonDto
        {
            AnimeGroupId = Guid.NewGuid(),
            AnimeId = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }
}