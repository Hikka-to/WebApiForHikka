using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.EmojiGroups;

[ExportTsInterface]
[ModelMetadataType(typeof(EmojiGroup))]
public class CreateEmojiGroupDto
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
}