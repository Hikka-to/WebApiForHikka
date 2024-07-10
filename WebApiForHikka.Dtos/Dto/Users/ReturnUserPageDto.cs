using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Users;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/Users")]
public class ReturnUserPageDto : ReturnPageDto<GetUserDto>;