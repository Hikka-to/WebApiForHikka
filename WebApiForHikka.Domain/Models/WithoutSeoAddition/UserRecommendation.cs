using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.UserRecommendations;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class UserRecommendation : Model
{
    public virtual required User User { get; set; }

    public virtual required Anime Anime { get; set; }

    [StringLength(UserRecommendationNumberConstants.DescriptionLength)]
    public required string Description { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required DateTime UpdatedAt { get; set; }
}