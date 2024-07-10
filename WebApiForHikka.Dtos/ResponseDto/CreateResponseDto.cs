using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.ResponseDto;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/ResponseDto/")]
public class CreateResponseDto
{
    public Guid Id { get; set; }
}