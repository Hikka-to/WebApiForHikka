
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.WebApi.Dto.Users;
public class GetUserDto
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Role { get; set; }

    public required Guid Id { get; set; }
}
