using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserSettings;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetUserSettingModels
{
    public static UserSetting GetSample()
    {
        return new UserSetting
        {
            IsAutoNext = true,           
            IsAutoPlay = false,        
            IsAutoSkipIntro = true,     
            IsDub = false,               
            IsRomaji = true,           
            IsPrivateAnimeList = false 
        };
    }
    
    public static UserSetting GetSampleForUpdate()
    {
        return new UserSetting
        {
            IsAutoNext = true,           
            IsAutoPlay = false,        
            IsAutoSkipIntro = true,     
            IsDub = true,               
            IsRomaji = true,           
            IsPrivateAnimeList = false 
        };
    }

    public static CreateUserSettingDto GetCreateSampleDto()
    {
        return new CreateUserSettingDto()
        {
            UserId = Guid.NewGuid(),
            IsAutoNext = true,           
            IsAutoPlay = false,        
            IsAutoSkipIntro = true,     
            IsDub = false,               
            IsRomaji = true,           
            IsPrivateAnimeList = true 
        };
    }

    public static GetUserSettingDto GetGetDtoSample()
    {
        return new GetUserSettingDto
        {
            IsAutoNext = false,           
            IsAutoPlay = false,        
            IsAutoSkipIntro = false,     
            IsDub = false,               
            IsRomaji = true,           
            IsPrivateAnimeList = true 
        };
    }

    public static UpdateUserSettingDto GetUpdateDtoSample()
    {
        return new UpdateUserSettingDto
        {
            UserId = Guid.NewGuid(),
            IsAutoNext = false,           
            IsAutoPlay = false,        
            IsAutoSkipIntro = false,     
            IsDub = false,               
            IsRomaji = false,           
            IsPrivateAnimeList = true 
        };
    }
}