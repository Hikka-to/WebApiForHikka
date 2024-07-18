using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.Relateds;

[ModelMetadataType(typeof(Related))]
[ExportTsInterface]
public class UpdateRelatedDto : ModelDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }
    [EntityValidation<AnimeGroup>] public required Guid AnimeGroupId { get; set; }
    [EntityValidation<RelatedType>] public required Guid RelatedTypeId { get; set; }
}