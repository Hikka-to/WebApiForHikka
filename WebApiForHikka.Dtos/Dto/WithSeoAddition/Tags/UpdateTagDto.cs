using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;

[ModelMetadataType(typeof(Tag))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Tags")]
public class UpdateTagDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public required string EngName { get; set; }

    public required List<string> Alises { get; set; }

    public required bool IsGenre { get; set; }

    [TagValidation] public Guid? ParentTagId { get; set; }
}