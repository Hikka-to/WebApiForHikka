using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.WebApi.Dto.Users;
public class UserRegistrationDto
{
    [EmailAddress(ErrorMessage = SharedStringConstants.EmailIsntFormatedCorrectlyErrorMessage)]
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
