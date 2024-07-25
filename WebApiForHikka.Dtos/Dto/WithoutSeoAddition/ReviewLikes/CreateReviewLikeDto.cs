using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition;

[ModelMetadataType(typeof(ReviewLike))]
public class CreateReviewLikeDto
{
    [EntityValidation<Review>] public  required Guid ReviewId { get; set; }
    
    [EntityValidation<User>] public  required Guid UserId { get; set; }
    
    public  required bool IsLiked { get; set; }
}