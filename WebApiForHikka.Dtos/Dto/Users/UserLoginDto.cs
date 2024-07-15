using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.Dto.Users;

[ExportTsInterface]
public record UserLoginDto
{
    [EmailAddress] public required string Email { get; set; }

    [Required] public required string Password { get; set; }
}