using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.UserRecomendations;

public interface IUserRecommendationRepository : ICrudRepository<UserRecommendation>
{
    
}