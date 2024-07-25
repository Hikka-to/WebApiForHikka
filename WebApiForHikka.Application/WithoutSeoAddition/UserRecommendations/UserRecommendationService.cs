using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.WithoutSeoAddition.Providers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.UserRecomendations;

public class UserRecommendationService(IUserRecommendationRepository repository)
    : CrudService<UserRecommendation, IUserRecommendationRepository>(repository), IUserRecommendationService
{
    
}