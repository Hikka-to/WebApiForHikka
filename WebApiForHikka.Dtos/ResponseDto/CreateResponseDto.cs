using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.ResponseDto;

[ExportTsInterface]
public class CreateResponseDto
{
    public Guid Id { get; set; }
}