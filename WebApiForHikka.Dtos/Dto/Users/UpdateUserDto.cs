using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserSettings;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.Users;

[ModelMetadataType(typeof(User))]
public class UpdateUserDto
{
    public required UpdateUserSettingDto UserSetting { get; set; }

    [Required(ErrorMessage = ControllerStringConstants.IdIsRequiredErrorMessage)]
    public required Guid Id { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = ControllerStringConstants.EmailIsntFormatedCorrectlyErrorMessage)]
    public required string Email { get; set; }

    [Required]
    [RoleValidation(ErrorMessage = UserStringConstants.RoleDoesntExist)]
    public required string Role { get; set; }

    public required string Name { get; set; }

    [FileContentType("image/*")]
    [MaxFileSize(SharedNumberConstatnts.MaxFileSize)]
    public IFormFile? AvatarImage { get; set; }

    [FileContentType("image/*")]
    [MaxFileSize(SharedNumberConstatnts.MaxFileSize)]
    public IFormFile? BackdropImage { get; set; }

    public string? Description { get; set; }

    public string? StatusText { get; set; }

    public required string StatusIcon { get; set; }

    public bool AllowAdult { get; set; }

    public DateTime LastSeenAt { get; set; }
}