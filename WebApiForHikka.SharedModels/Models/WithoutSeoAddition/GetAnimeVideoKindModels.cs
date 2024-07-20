using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetAnimeVideoKindModels
{
    public static AnimeVideoKind GetSample()
    {
        return new AnimeVideoKind
        {
            Name = "Name"
        };
    }

    public static AnimeVideoKind GetSampleForUpdate()
    {
        return new AnimeVideoKind
        {
            Name = "Name1"
        };
    }

    public static CreateAnimeVideoKindDto GetCreateDtoSample()
    {
        return new CreateAnimeVideoKindDto
        {
            Name = Lorem.GetFirstWord()
        };
    }

    public static GetAnimeVideoKindDto GetGetDtoSample()
    {
        return new GetAnimeVideoKindDto
        {
            Name = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }

    public static UpdateAnimeVideoKindDto GetUpdateDtoSample()
    {
        return new UpdateAnimeVideoKindDto
        {
            Name = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }
}