using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.ResponseDto;

[ExportTsInterface(OutputDir = "./TS/ResponseDto/")]
public class LoginResponseUserDto
{
    public required string Token { get; set; }
}