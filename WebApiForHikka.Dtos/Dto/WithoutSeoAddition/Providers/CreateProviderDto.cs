using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Providers;

[ModelMetadataType(typeof(Provider))]
public class CreateProviderDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }

    public required string LogoPath { get; set; }

    public required string Name { get; set; }

    public required int Priority { get; set; }
}