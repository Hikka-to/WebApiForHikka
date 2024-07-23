using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserAnimeListTypes;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetUserAnimeListTypeModels
{
    public static UserAnimeListType GetSample()
    {
        return new UserAnimeListType
        {
           Slug = "test",
           Name = "test"
        };
    }

    public static UserAnimeListType GetSampleForUpdate()
    {
        return new UserAnimeListType
        {
            Slug = "test1",
            Name = "test1"
        };
    }

    public static CreateUserAnimeListTypeDto GetCreateSampleDto()
    {
        return new CreateUserAnimeListTypeDto()
        {
            Slug = "test2",
            Name = "test2"
        };
    }

    public static GetUserAnimeListTypeDto GetGetDtoSample()
    {
        return new GetUserAnimeListTypeDto
        {
            Slug = "test3",
            Name = "test3",
            Id = Guid.NewGuid()
        };
    }

    public static UpdateUserAnimeListTypeDto GetUpdateDtoSample()
    {
        return new UpdateUserAnimeListTypeDto
        {
            Slug = "test4",
            Name = "test4",
            Id = Guid.NewGuid()
        };
    }
}