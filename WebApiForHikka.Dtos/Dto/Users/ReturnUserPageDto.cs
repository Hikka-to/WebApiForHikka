﻿using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Users;

[ExportTsInterface(OutputDir = "./TS/Dto/Users")]
public class ReturnUserPageDto : ReturnPageDto<GetUserDto>;