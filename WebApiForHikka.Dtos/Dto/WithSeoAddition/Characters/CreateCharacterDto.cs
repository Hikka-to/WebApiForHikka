using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;

[ModelMetadataType(typeof(Character))]
public class CreateCharacterDto : CreateDtoWithSeoAddition
{
    public string? Name { get; set; }

    public required string RomajiName { get; set; }

    public required string NativeName { get; set; }

    public string? AlternativeName { get; set; }

    [EntityValidation<Anime>] public required List<Guid> Animes { get; set; }

    [EntityValidation<Tag>] public required List<Guid> Tags { get; set; }

    public string? Description { get; set; }

    [FileContentType("image/*")]
    [MaxFileSize(SharedNumberConstatnts.MaxFileSize)]
    public required IFormFile Image { get; set; }
}