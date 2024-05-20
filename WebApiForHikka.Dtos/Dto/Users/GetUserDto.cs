using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Users;
public class GetUserDto : ModelDto
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Role { get; set; }
}