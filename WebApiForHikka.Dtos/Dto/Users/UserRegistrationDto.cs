using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.Users;

public class UserRegistrationDto
{
    [StringLength(UserNumberConstants.NameLength, ErrorMessage = UserStringConstants.NameIsTooLongErrorMessage)]
    [UserNameAlreadyExist(ErrorMessage = UserStringConstants.UserNameAlreadyExistErrorMessage)]
    [Required]
    public required string UserName { get; set; }

    [EmailAddress(ErrorMessage = ControllerStringConstants.EmailIsntFormatedCorrectlyErrorMessage)]
    [EmailIsAlreadyExist(ErrorMessage = UserStringConstants.UserEmailAlreadyExistErrorMessage)]
    [Required]
    public required string Email { get; set; }

    [Required]
    [RegularExpression(UserStringConstants.SimplePasswordRegExpression,
        ErrorMessage = UserStringConstants.SimplePasswordErrorMessage)]
    public required string Password { get; set; }

    [Required]
    [RoleValidation(ErrorMessage = UserStringConstants.RoleDoesntExist)]
    public required string Role { get; set; }
}