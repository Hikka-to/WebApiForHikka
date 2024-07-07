using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public class GetMediaplayerModels
{
    public static Mediaplayer GetSample()
    {
        return new Mediaplayer
        {
            Icon = "Icon",
            Name = "Name"
        };
    }

    public static Mediaplayer GetSampleForUpdate()
    {
        return new Mediaplayer
        {
            Icon = "Icon1",
            Name = "Name1"
        };
    }

    public static CreateMediaplayerDto GetCreateDtoSample()
    {
        return new CreateMediaplayerDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord()
        };
    }

    public static GetMediaplayerDto GetGetDtoSample()
    {
        return new GetMediaplayerDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static Mediaplayer GetModelSample()
    {
        return new Mediaplayer
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static UpdateMediaplayerDto GetUpdateDtoSample()
    {
        return new UpdateMediaplayerDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }
}