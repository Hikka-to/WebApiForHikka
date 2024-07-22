using WebApiForHikka.Application.WithoutSeoAddition.UserSettings;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class UserSettingRepository(HikkaDbContext dbContext)
    : CrudRepository<UserSetting>(dbContext), IUserSettingRepository
{
    
}