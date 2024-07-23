using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserSettings;

public class UpdateUserSettingDto : ModelDto
{
    [EntityValidation<User>] public required Guid UserId { get; set; }
    public required bool IsAutoNext { get; set; }
    
    public required bool IsAutoPlay { get; set; }
    
    public required bool IsAutoSkipIntro { get; set; }
    
    public required bool IsDub { get; set; }
    
    public required bool IsRomaji { get; set; }
    
    public required bool IsPrivateAnimeList { get; set; }
}