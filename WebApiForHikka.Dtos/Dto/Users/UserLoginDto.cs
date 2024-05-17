using System.ComponentModel.DataAnnotations;

namespace WebApiForHikka.Dtos.Dto.Users;
public record UserLoginDto
{
    [EmailAddress]
    public required string Email { get; set; }

    
    [Required]
    public required string Password { get; set; }
}
