﻿using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Kinds;

[ModelMetadataType(typeof(Kind))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Kinds")]
public class CreateKindDto : CreateDtoWithSeoAddition
{
    public required string Slug { get; set; }

    public required string Hint { get; set; }
}