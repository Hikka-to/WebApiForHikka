﻿using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.RelatedTypes;


[ModelMetadataType(typeof(RelatedType))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithoutSeoAddition/RelatedType")]
public class CreateRelatedTypeDto
{
    public required string Name { get; set; }
}
