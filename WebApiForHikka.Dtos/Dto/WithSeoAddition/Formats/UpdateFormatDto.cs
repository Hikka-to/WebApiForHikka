﻿using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

[ModelMetadataType(typeof(Format))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Formats")]
public class UpdateFormatDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }
}