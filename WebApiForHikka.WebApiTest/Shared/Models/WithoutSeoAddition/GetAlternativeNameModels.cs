using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public class GetAlternativeNameModels
{
    public static AlternativeName GetSample()
    {
        return new AlternativeName
        {
            Anime = GetAnimeModels.GetSample(),
            Name = "Test"
        };
    }

    public static AlternativeName GetSampleForUpdate()
    {
        return new AlternativeName
        {
            Anime = GetAnimeModels.GetSampleForUpdate(),
            Name = "Test1"
        };
    }

    public static CreateAlternativeNameDto GetCreateDtoSample()
    {
        return new CreateAlternativeNameDto
        {
            AnimeId = Guid.NewGuid(),
            Name = Lorem.GetFirstWord()
        };
    }

    public static GetAlternativeNameDto GetGetDtoSample()
    {
        return new GetAlternativeNameDto
        {
            AnimeId = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }

    public static UpdateAlternativeNameDto GetUpdateDtoSample()
    {
        return new UpdateAlternativeNameDto
        {
            AnimeId = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }
}