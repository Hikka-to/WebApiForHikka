using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetExternalLinkModels
{
    public static ExternalLink GetSample()
    {
        return new ExternalLink
        {
            Anime = GetAnimeModels.GetSample(),
            Url = "Url"
        };
    }

    public static ExternalLink GetSampleForUpdate()
    {
        return new ExternalLink
        {
            Anime = GetAnimeModels.GetSampleForUpdate(),
            Url = "Url1"
        };
    }

    public static CreateExternalLinkDto GetCreateDtoSample()
    {
        return new CreateExternalLinkDto
        {
            AnimeId = Guid.NewGuid(),
            Url = Lorem.GetFirstWord()
        };
    }

    public static GetExternalLinkDto GetGetDtoSample()
    {
        return new GetExternalLinkDto
        {
            AnimeId = Guid.NewGuid(),
            Url = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }

    public static UpdateExternalLinkDto GetUpdateDtoSample()
    {
        return new UpdateExternalLinkDto
        {
            AnimeId = Guid.NewGuid(),
            Url = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }
}