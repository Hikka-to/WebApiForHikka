﻿using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Sources;

[ModelMetadataType(typeof(Source))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Sources")]
public class UpdateSourceDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }
}