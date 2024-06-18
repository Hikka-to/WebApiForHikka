using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.ResponseDto;


[ExportTsInterface(OutputDir = "./TS/ResponseDto/")]
public class RegistratedResponseUserDto : ModelDto
{
    public required string Message;

    public required string JwtToken;
}