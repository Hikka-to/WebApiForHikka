using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Users;

namespace WebApiForHikka.Dtos.Dto.Users;
public record UserLoginDto
{
    [EmailAddress]
    public required string Email { get; set; }

    
    [Required]
    public required string Password { get; set; }
}
