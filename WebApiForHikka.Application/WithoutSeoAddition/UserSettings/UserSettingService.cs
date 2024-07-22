using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.WithoutSeoAddition.Providers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.UserSettings;

public class UserSettingService (IUserSettingRepository repository)
    : CrudService<UserSetting, IUserSettingRepository>(repository), IUserSettingService
{
    
}