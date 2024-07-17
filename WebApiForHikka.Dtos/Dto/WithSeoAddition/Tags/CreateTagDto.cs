using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;

[ModelMetadataType(typeof(Tag))]
[ExportTsInterface]
public class CreateTagDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public required string EngName { get; set; }

    public required List<string> Alises { get; set; }

    public required bool IsGenre { get; set; }

    [EntityValidation<Tag>] public Guid? ParentTagId { get; set; }
}