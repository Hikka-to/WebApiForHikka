using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.WatchUserHistories;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public class GetUserWatchHistoryModels 
{
    public static UserWatchHistory GetSample()
    {
        return new UserWatchHistory
        {
            FirstId = default,
            SecondId = default,
            First = GetUserModels.GetSample(),
            Second = GetEpisodeModels.GetSample(),
            CreatedAt = DateTime.Now,
            ProgressTime = 0,
            UpdatedAt = DateTime.Now,
        };
    }

    public static UserWatchHistory GetSampleForUpdate()
    {
        return new UserWatchHistory
        {
            FirstId = default,
            SecondId = default,
            First = GetUserModels.GetSampleForUpdate(),
            Second = GetEpisodeModels.GetSampleForUpdate(),
            CreatedAt = DateTime.Now,
            ProgressTime = 0,
            UpdatedAt = DateTime.Now,
        };
    }
    public static CreateUserWatchHistoryDto GetCreateDtoSample()
    {
        return new CreateUserWatchHistoryDto
        {
            EpisodeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProgressTime = 21,
        };
    }

    public static GetUserWatchHistoryDto GetGetDtoSample()
    {
        return new GetUserWatchHistoryDto
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Episode = GetEpisodeModels.GetGetDtoSample(),
            ProgressTime= 21,
            UpdatedAt= DateTime.Now,
            User = GetUserModels.GetGetDtoSample()
        };
    }

    public static UpdateUserWatchHistoryDto GetUpdateDtoSample()
    {
        return new UpdateUserWatchHistoryDto
        {
          
            EpisodeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProgressTime = 231,
            Id = Guid.NewGuid()
        };
    }
}

