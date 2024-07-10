using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.Dto.Authorization;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/Authorization")]
public record JwtTokenContentDto
{
    public required string? Email;
    public required string? Id;
    public required string? Role;
}