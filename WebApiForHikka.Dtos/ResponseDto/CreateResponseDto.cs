using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.ResponseDto;

[ExportTsInterface(OutputDir = "./TS/ResponseDto/")]
public class CreateResponseDto
{
    public Guid Id { get; set; }
}