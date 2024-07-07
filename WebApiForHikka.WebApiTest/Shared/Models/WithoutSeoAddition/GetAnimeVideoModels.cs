using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public static class GetAnimeVideoModels
{

    public static AnimeVideo GetSample() => new()
    {
        AnimeVideoKind = GetAnimeVideoKindModels.GetSample(),
        Name = "Name1",
        Url = "Url1",
        ImageUrl = "ImageUrl1",
        EmbedUrl = "EmbedUrl1",
    };

    public static AnimeVideo GetSampleForUpdate() => new()
    {
        AnimeVideoKind = GetAnimeVideoKindModels.GetSampleForUpdate(),
        Name = "Name2",
        Url = "Url2",
        ImageUrl = "ImageUrl2",
        EmbedUrl = "EmbedUrl2",
    };

    public static CreateAnimeVideoDto GetCreateDtoSample() => new()
    {
        AnimeVideoKindId = Guid.NewGuid(),
        Name = Faker.Lorem.GetFirstWord(),
        Url = Faker.Lorem.GetFirstWord(),
        ImageUrl = Faker.Lorem.GetFirstWord(),
        EmbedUrl = Faker.Lorem.GetFirstWord(),
    };

    public static GetAnimeVideoDto GetGetDtoSample() => new()
    {
        AnimeVideoKindId = Guid.NewGuid(),
        Name = Faker.Lorem.GetFirstWord(),
        Url = Faker.Lorem.GetFirstWord(),
        ImageUrl = Faker.Lorem.GetFirstWord(),
        EmbedUrl = Faker.Lorem.GetFirstWord(),
        Id = Guid.NewGuid()
    };

    public static UpdateAnimeVideoDto GetUpdateDtoSample() => new()
    {
        AnimeVideoKindId = Guid.NewGuid(),
        Name = Faker.Lorem.GetFirstWord(),
        Url = Faker.Lorem.GetFirstWord(),
        ImageUrl = Faker.Lorem.GetFirstWord(),
        EmbedUrl = Faker.Lorem.GetFirstWord(),
        Id = Guid.NewGuid()
    };

}
