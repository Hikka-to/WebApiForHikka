using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Providers;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetProviderModels
{
    public static Provider GetSample()
    {
        return new Provider
        {
            Anime = GetAnimeModels.GetSample(),
            LogoPath = "test",
            Name = "test",
            Priority = 1
        };
    }

    public static Provider GetSampleForUpdate()
    {
        return new Provider
        {
            Anime = GetAnimeModels.GetSample(),
            LogoPath = "test1",
            Name = "test1",
            Priority = 12
        };
    }

    public static CreateProviderDto GetCreateSampleDto()
    {
        return new CreateProviderDto
        {
            AnimeId = Guid.NewGuid(),
            LogoPath = "test3",
            Name = "test5",
            Priority = 1
        };
    }

    public static GetProviderDto GetGetDtoSample()
    {
        return new GetProviderDto
        {
            Anime = GetAnimeModels.GetGetDtoSample(),
            LogoPath = "test",
            Name = "test",
            Priority = 1,
            Id = Guid.NewGuid()
        };
    }

    public static UpdateProviderDto GetUpdateDtoSample()
    {
        return new UpdateProviderDto
        {
            AnimeId = Guid.NewGuid(),
            LogoPath = "test34",
            Name = "test45",
            Priority = 1,
            Id = Guid.NewGuid()
        };
    }
}