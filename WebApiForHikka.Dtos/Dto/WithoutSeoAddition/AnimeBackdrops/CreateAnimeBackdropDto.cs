using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ModelMetadataType(typeof(AnimeBackdrop))]
public class CreateAnimeBackdropDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }

    public required IFormFile Image { get; set; }
}