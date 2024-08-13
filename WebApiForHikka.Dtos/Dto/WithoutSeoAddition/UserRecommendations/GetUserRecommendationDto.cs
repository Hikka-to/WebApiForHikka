using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserRecommendations;

[ExportTsInterface]
public class GetUserRecommendationDto : ModelDto
{
    public virtual required GetUserDto User { get; set; }

    public virtual required GetAnimeDto Anime { get; set; }

    public required string Description { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required DateTime UpdatedAt { get; set; }
}