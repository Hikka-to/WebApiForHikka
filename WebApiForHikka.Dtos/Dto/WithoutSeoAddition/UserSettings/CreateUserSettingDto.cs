using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserSettings;

[ModelMetadataType(typeof(UserSetting))]
public class CreateUserSettingDto
{
    [EntityValidation<User>] public virtual required Guid UserId { get; set; }

    public required bool IsAutoNext { get; set; }

    public required bool IsAutoPlay { get; set; }

    public required bool IsAutoSkipIntro { get; set; }

    public required bool IsDub { get; set; }

    public required bool IsRomaji { get; set; }

    public required bool IsPrivateAnimeList { get; set; }
}