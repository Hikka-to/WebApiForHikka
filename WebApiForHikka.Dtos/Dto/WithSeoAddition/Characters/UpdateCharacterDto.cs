using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;

[ModelMetadataType(typeof(Character))]
[ExportTsInterface]
public class UpdateCharacterDto : UpdateDtoWithSeoAddition
{
    public string? Name { get; set; }

    public required string RomajiName { get; set; }

    public required string NativeName { get; set; }

    public string? AlternativeName { get; set; }

    [EntityValidation<Anime>]
    public required Guid AnimeId { get; set; }

    [FileContentType("image/*")]
    [MaxFileSize(SharedNumberConstatnts.MaxFileSize)]
    public required IFormFile Image { get; set; }

    public string? Description { get; set; }
}