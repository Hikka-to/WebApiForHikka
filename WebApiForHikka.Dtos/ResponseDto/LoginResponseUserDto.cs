using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.ResponseDto;

[ExportTsInterface]
public class LoginResponseUserDto
{
    public required string Token { get; set; }
}