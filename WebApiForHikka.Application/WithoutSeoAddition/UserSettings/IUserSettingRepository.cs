﻿using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.UserSettings;

public interface IUserSettingRepository : ICrudRepository<UserSetting>;