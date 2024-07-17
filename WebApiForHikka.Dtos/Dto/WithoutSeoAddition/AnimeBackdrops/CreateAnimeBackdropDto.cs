using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ModelMetadataType(typeof(AnimeBackdrop))]
public class CreateAnimeBackdropDto
{
    [AnimeValidation] public required Guid AnimeId { get; set; }

    public required IFormFile Image { get; set; }
}