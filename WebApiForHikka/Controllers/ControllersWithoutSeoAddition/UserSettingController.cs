using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.UserSettings;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserSettings;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class UserSettingController(
    IUserSettingService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
)
    : CrudController
        <GetUserSettingDto, UpdateUserSettingDto, CreateUserSettingDto, IUserSettingService, UserSetting>(crudService,
            mapper, httpContextAccessor)
{
}