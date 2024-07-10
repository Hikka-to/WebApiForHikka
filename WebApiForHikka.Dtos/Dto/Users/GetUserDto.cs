using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Users;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/Users")]
public class GetUserDto : ModelDto
{
    public required string Email { get; set; }


    public required string[] Roles { get; set; }
}