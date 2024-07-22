using Faker;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.RelatedType;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.SharedModels.MyDataFakers;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetAnimeBackdropModels
{
    public static AnimeBackdrop GetSample()
    {
        return new AnimeBackdrop
        {
            Anime = GetAnimeModels.GetSample(),
            Path = "Test",
            Width = 1,
            Height = 1,
            Colors = [1, 2, 3]
        };
    }

    public static AnimeBackdrop GetSampleForUpdate()
    {
        return new AnimeBackdrop
        {
            Anime = GetAnimeModels.GetSampleForUpdate(),
            Path = "Test1",
            Width = 2,
            Height = 2,
            Colors = [4, 5, 6]
        };
    }

    public static CreateAnimeBackdropDto GetCreateSampleDto() 
    {
        return new CreateAnimeBackdropDto()
        {
            AnimeId = Guid.NewGuid(),
            Image = MyDataFaker.GetFakeImage(),
        };
    }

    public static GetAnimeBackdropDto GetGetDtoSample()
    {
        return new GetAnimeBackdropDto
        {
            AnimeId = Guid.NewGuid(),
            Colors = new List<int> { 123123, 2312312, 23132, 213123 },
            Height = 1920,
            ImageUrl = MyDataFaker.GetFakeUrl,
            Width = 1920,
            Id = Guid.NewGuid(),
        };
    }

    public static UpdateAnimeBackdropDto GetUpdateDtoSample()
    {
        return new UpdateAnimeBackdropDto
        {
            AnimeId = Guid.NewGuid(),
            Image = MyDataFaker.GetFakeImage(),
            Id = Guid.NewGuid(),
        };
    }
}