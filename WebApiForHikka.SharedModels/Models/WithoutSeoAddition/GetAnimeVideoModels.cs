using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public static class GetAnimeVideoModels
{
    public static AnimeVideo GetSample()
    {
        return new AnimeVideo
        {
            AnimeVideoKind = GetAnimeVideoKindModels.GetSample(),
            Name = "Name1",
            Url = "Url1",
            ImageUrl = "ImageUrl1",
            EmbedUrl = "EmbedUrl1"
        };
    }

    public static AnimeVideo GetSampleForUpdate()
    {
        return new AnimeVideo
        {
            AnimeVideoKind = GetAnimeVideoKindModels.GetSampleForUpdate(),
            Name = "Name2",
            Url = "Url2",
            ImageUrl = "ImageUrl2",
            EmbedUrl = "EmbedUrl2"
        };
    }

    public static CreateAnimeVideoDto GetCreateDtoSample()
    {
        return new CreateAnimeVideoDto
        {
            AnimeVideoKindId = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            Url = Lorem.GetFirstWord(),
            ImageUrl = Lorem.GetFirstWord(),
            EmbedUrl = Lorem.GetFirstWord()
        };
    }

    public static GetAnimeVideoDto GetGetDtoSample()
    {
        return new GetAnimeVideoDto
        {
            AnimeVideoKindId = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            Url = Lorem.GetFirstWord(),
            ImageUrl = Lorem.GetFirstWord(),
            EmbedUrl = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }

    public static UpdateAnimeVideoDto GetUpdateDtoSample()
    {
        return new UpdateAnimeVideoDto
        {
            AnimeVideoKindId = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            Url = Lorem.GetFirstWord(),
            ImageUrl = Lorem.GetFirstWord(),
            EmbedUrl = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }
}