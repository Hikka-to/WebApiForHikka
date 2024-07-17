﻿using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.ResponseDto;

[ExportTsInterface]
public class RegistratedResponseUserDto : ModelDto
{
    public required string Message { get; set; }

    public required string JwtToken { get; set; }
}