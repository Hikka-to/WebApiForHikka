﻿using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;

[ModelMetadataType(typeof(Studio))]
[ExportTsInterface]
public class CreateStudioDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public string? Logo { get; set; }
}