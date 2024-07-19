using Faker;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetEpisodeModels
{
    public static Episode GetSample()
    {
        return new Episode
        {
            AirDate = DateTime.Now,
            CreateAt = DateTime.Now,
            Duration = 23123,
            IsFiller = true,
            UpdateAt = DateTime.Now,
            AnimeId = Guid.NewGuid(),
            Name = "Name",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Episode GetSampleForUpdate()
    {
        return new Episode
        {
            AirDate = DateTime.Now,
            CreateAt = DateTime.Now,
            Duration = 3414,
            IsFiller = false,
            UpdateAt = DateTime.Now,
            AnimeId = Guid.NewGuid(),
            Name = "Name1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateEpisodeDto GetCreateDtoSample()
    {
        return new CreateEpisodeDto
        {
            AirDate = DateTime.Now,
            Name = Lorem.GetFirstWord(),
            Duration = 3414,
            IsFiller = false,
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
            
        };
    }

    public static GetEpisodeDto GetGetDtoSample()
    {
        return new GetEpisodeDto
        {
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            AirDate = DateTime.Now,
            Duration = 3414,
            IsFiller = false,
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now,
            AnimeId = Guid.NewGuid(),
            Id = new Guid()
        };
    }

    public static UpdateEpisodeDto GetUpdateDtoSample()
    {
        return new UpdateEpisodeDto
        {
            AirDate = DateTime.Now,
            Duration = 1234,
            IsFiller = false,
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}
