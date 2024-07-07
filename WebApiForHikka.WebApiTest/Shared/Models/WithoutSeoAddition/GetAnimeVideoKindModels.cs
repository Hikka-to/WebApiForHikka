using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public class GetAnimeVideoKindModels
{
    public static AnimeVideoKind GetSample() => new()
    {
        Name = "Name",
    };

    public static AnimeVideoKind GetSampleForUpdate() => new()
    {
        Name = "Name1",
    };
    public static CreateAnimeVideoKindDto GetCreateDtoSample() => new()
    {
        Name = Faker.Lorem.GetFirstWord()
    };

    public static GetAnimeVideoKindDto GetGetDtoSample() => new()
    {
        Name = Faker.Lorem.GetFirstWord(),
        Id = Guid.NewGuid()
    };

    public static UpdateAnimeVideoKindDto GetUpdateDtoSample() => new()
    {
        Name = Faker.Lorem.GetFirstWord(),
        Id = Guid.NewGuid()
    };


}
