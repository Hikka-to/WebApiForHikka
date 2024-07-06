using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public class GetMediaplayerModels
{
    public static Mediaplayer GetSample()
    {
        return new Mediaplayer()
        {
            Icon = "Icon",
            Name = "Name",
        };
    }

    public static Mediaplayer GetSampleForUpdate()
    {
        return new Mediaplayer()
        {
            Icon = "Icon1",
            Name = "Name1",
        };
    }
}
