using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserRecomendations;

[ExportTsInterface]
public class GetUserRecommendationDto
{
    public virtual required GetUserDto User { get; set; }

    public virtual required GetAnimeDto Anime { get; set; }
    
    public required string Description { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public required DateTime UpdatedAt { get; set; }
}