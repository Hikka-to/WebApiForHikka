using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Constants.Users;

namespace WebApiForHikka.WebApi.Dto.Users;
public class UserRegistrationDto
{
    [EmailAddress(ErrorMessage = SharedStringConstants.EmailIsntFormatedCorrectlyErrorMessage)]
    [Required]
    public required string Email { get; set; }

    [Required]
    [RegularExpression(UserStringConstants.SimplePasswordRegExpression, ErrorMessage = UserStringConstants.SimplePasswordErrorMessage)]
    public required string Password { get; set; }

    [Required]
    public required string Role { get; set; }
}
