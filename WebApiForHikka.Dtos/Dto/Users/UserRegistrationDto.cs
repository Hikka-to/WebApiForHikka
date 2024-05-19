using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.Users;
public class UserRegistrationDto
{
    [EmailAddress(ErrorMessage = ControllerStringConstants.EmailIsntFormatedCorrectlyErrorMessage)]
    [EmailIsAlreadyExist(ErrorMessage = UserStringConstants.UserIsAlreadyExistErrorMessage)]
    [Required]
    public required string Email { get; set; }

    [Required]
    [RegularExpression(UserStringConstants.SimplePasswordRegExpression, ErrorMessage = UserStringConstants.SimplePasswordErrorMessage)]
    public required string Password { get; set; }

    [Required]
    [RoleValidation(ErrorMessage = UserStringConstants.RoleDoesntExist)]
    public required string Role { get; set; }
}
