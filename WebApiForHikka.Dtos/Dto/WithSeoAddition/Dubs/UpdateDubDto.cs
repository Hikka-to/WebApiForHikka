﻿using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;

[ModelMetadataType(typeof(Dub))]
[ExportTsInterface]
public class UpdateDubDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public string? Icon { get; set; }
}