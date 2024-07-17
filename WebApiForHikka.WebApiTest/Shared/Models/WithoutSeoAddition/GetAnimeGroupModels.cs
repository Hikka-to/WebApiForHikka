using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeGroups;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public static class GetAnimeGroupModels
{
    public static AnimeGroup GetSample()
    {
        return new AnimeGroup
        {
            Name = "Name"
        };
    }

    public static AnimeGroup GetSampleForUpdate()
    {
        return new AnimeGroup
        {
            Name = "Name1"
        };
    }

    public static CreateAnimeGroupDto GetCreateDtoSample()
    {
        return new CreateAnimeGroupDto
        {
            Name = Lorem.GetFirstWord(),
        };
    }

    public static GetAnimeGroupDto GetGetDtoSample()
    {
        return new GetAnimeGroupDto
        {
            Name = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static AnimeGroup GetModelSample()
    {
        return new AnimeGroup
        {
            Name = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static UpdateAnimeGroupDto GetUpdateDtoSample()
    {
        return new UpdateAnimeGroupDto
        {
            Name = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }
}
