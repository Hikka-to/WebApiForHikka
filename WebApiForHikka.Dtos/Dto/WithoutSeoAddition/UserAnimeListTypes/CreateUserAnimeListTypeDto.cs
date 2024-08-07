using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserAnimeListTypes;

[ModelMetadataType(typeof(UserAnimeListType))]
public class CreateUserAnimeListTypeDto
{
    public required string Slug { get; set; }

    public required string Name { get; set; }
}