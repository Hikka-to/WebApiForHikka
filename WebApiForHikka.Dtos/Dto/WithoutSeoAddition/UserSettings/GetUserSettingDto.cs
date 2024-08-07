using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserSettings;

[ExportTsInterface]
public class GetUserSettingDto : ModelDto
{
    public required bool IsAutoNext { get; set; }

    public required bool IsAutoPlay { get; set; }

    public required bool IsAutoSkipIntro { get; set; }

    public required bool IsDub { get; set; }

    public required bool IsRomaji { get; set; }

    public required bool IsPrivateAnimeList { get; set; }
}