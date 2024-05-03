using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Constants.Users;

namespace WebApiForHikka.WebApi.Dto.Users;
public class UpdateUserDto
{
    [Required(ErrorMessage = SharedStringConstants.IdIsRequiredErrorMessage)]
    public required Guid Id { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = SharedStringConstants.EmailIsntFormatedCorrectlyErrorMessage)]
    public required string Email { get; set; }

    [Required]
    public required string Role { get; set; }
}
