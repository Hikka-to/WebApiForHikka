using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserRecomendations;

[ModelMetadataType(typeof(UserRecommendation))]

public class CreateUserRecommendationDto
{
    [EntityValidation<User>] public required Guid UserId { get; set; }

    [EntityValidation<Anime>]public required Guid AnimeId { get; set; }
    
    public required string Description { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public required DateTime UpdatedAt { get; set; }
}

