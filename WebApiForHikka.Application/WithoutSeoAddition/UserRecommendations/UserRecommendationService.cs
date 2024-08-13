using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.UserRecommendations;

public class UserRecommendationService(IUserRecommendationRepository repository)
    : CrudService<UserRecommendation, IUserRecommendationRepository>(repository), IUserRecommendationService
{
}