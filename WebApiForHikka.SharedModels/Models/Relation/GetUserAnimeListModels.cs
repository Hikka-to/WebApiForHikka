using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.UserAnimeLists;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public class GetUserAnimeListModels
{
    public static UserAnimeList GetSample()
    {
        return new UserAnimeList
        {
            UserAnimeListType = GetUserAnimeListTypeModels.GetSample(),
            IsFavorite = true,
            
            FirstId = Guid.NewGuid(),
            SecondId = Guid.NewGuid(),
            
            First = GetUserModels.GetSample(),
            Second = GetAnimeModels.GetSample()
            
            
        };
    }

    public static UserAnimeList GetSampleForUpdate()
    {
        return new UserAnimeList
        {
            UserAnimeListType = GetUserAnimeListTypeModels.GetSample(),
            IsFavorite = false,
            
            FirstId = Guid.NewGuid(),
            SecondId = Guid.NewGuid(),
            
            First = GetUserModels.GetSample(),
            Second = GetAnimeModels.GetSample()
        };
    }

    public static CreateUserAnimeListDto GetCreateDtoSample()
    {
        return new CreateUserAnimeListDto
        {
            IsFavorite = false,
            UserAnimeListTypeId = Guid.NewGuid()
        };
    }

    public static GetUserAnimeListDto GetGetDtoSample()
    {
        return new GetUserAnimeListDto
        { 
            UserAnimeListType = GetUserAnimeListTypeModels.GetSample(),
            IsFavorite = false,
            
            User = GetUserModels.GetGetDtoSample(),
            Anime = GetAnimeModels.GetGetDtoSample()
            
        };
    }

    public static UpdateUserAnimeListDto GetUpdateDtoSample()
    {
        return new UpdateUserAnimeListDto
        {
            UserId = Guid.NewGuid(),
            AnimeId = Guid.NewGuid(),
            
            IsFavorite = false,
            
            Id = Guid.NewGuid()
        };
    }
}