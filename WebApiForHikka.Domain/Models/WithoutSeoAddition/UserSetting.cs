namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class UserSetting : Model
{
    public required bool IsAutoNext { get; set; }

    public required bool IsAutoPlay { get; set; }

    public required bool IsAutoSkipIntro { get; set; }

    public required bool IsDub { get; set; }

    public required bool IsRomaji { get; set; }

    public required bool IsPrivateAnimeList { get; set; }
}