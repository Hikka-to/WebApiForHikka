using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.Notifications;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public static class GetNotificationModels
{
    public static Notification GetSample()
    {
        return new Notification
        {
            FirstId = Guid.NewGuid(),
            SecondId = Guid.NewGuid(),
            First = GetUserModels.GetSample(),
            Second = GetAnimeModels.GetSample(),
            CreatedAt = DateTime.Now,
            Resource = GetResourceModels.GetSample()
        };
    }

    public static Notification GetSampleForUpdate()
    {
        return new Notification
        {
            FirstId = Guid.NewGuid(),
            SecondId = Guid.NewGuid(),
            First = GetUserModels.GetSampleForUpdate(),
            Second = GetAnimeModels.GetSampleForUpdate(),
            CreatedAt = DateTime.Now,
            Resource = GetResourceModels.GetSampleForUpdate()
        };
    }

    public static CreateNotificationDto GetCreateDtoSample()
    {
        return new CreateNotificationDto
        {
            AnimeId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
    }

    public static GetNotificationDto GetGetDtoSample()
    {
        return new GetNotificationDto
        { 
            AnimeId = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            ResourceId = Guid.NewGuid(),
            UserId= Guid.NewGuid(),
            Id = Guid.NewGuid()
        };
    }

    public static UpdateNotificationDto GetUpdateDtoSample()
    {
        return new UpdateNotificationDto
        {
            AnimeId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            UserId= Guid.NewGuid(),
            Id = Guid.NewGuid()
        };
    }
}
