﻿using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.Relation.Seasons;

[MetadataType(typeof(Season))]
[ExportTsInterface]
public class CreateSeasonDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }
    [EntityValidation<AnimeGroup>] public required Guid AnimeGroupId { get; set; }
    public required string Name { get; set; }
}