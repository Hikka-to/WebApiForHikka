using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.ResponseDto;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/ResponseDto/")]
public class LoginResponseUserDto
{
    public required string Token { get; set; }
}