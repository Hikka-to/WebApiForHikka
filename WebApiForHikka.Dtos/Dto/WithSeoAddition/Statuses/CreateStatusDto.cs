﻿using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Statuses;

[ModelMetadataType(typeof(Status))]
[ExportTsInterface]
public class CreateStatusDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
}