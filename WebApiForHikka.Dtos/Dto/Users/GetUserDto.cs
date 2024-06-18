using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Users;

[ExportTsInterface(OutputDir = "./TS/Dto/Users")]
public class GetUserDto : ModelDto
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Role { get; set; }
}