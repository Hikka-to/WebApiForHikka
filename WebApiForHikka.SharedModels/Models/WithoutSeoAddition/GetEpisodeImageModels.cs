using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Episodes;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.SharedModels.MyDataFakers;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public static class GetEpisodeImageModels
{
    public static EpisodeImage GetSample()
    {
        return new EpisodeImage
        {
            Episode = GetEpisodeModels.GetSample(),
            Path = "Test",
            Width = 1,
            Height = 1,
            Colors = [1, 2, 3]
        };
    }

    public static EpisodeImage GetSampleForUpdate()
    {
        return new EpisodeImage
        {
            Episode = GetEpisodeModels.GetSample(),
            Path = "Test1",
            Width = 2,
            Height = 2,
            Colors = [4, 5, 6]
        };
    }

    public static CreateEpisodeImageDto GetCreateSampleDto()
    {
        return new CreateEpisodeImageDto()
        {
            EpisodeId = Guid.NewGuid(),
            Image = MyDataFaker.GetFakeImage(),
        };
    }

    public static GetEpisodeImageDto GetGetDtoSample()
    {
        return new GetEpisodeImageDto
        {
            EpisodeId = Guid.NewGuid(),
            Colors = new List<int> { 123123, 2312312, 23132, 213123 },
            Height = 1920,
            ImageUrl = MyDataFaker.GetFakeUrl,
            Width = 1920,
            Id = Guid.NewGuid(),
        };
    }

    public static UpdateEpisodeImageDto GetUpdateDtoSample()
    {
        return new UpdateEpisodeImageDto
        {
            EpisodeId = Guid.NewGuid(),
            Image = MyDataFaker.GetFakeImage(),
            Id = Guid.NewGuid(),
        };
    }
}
