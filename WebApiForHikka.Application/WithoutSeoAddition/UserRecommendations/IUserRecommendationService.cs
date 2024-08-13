using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.UserRecommendations;

public interface IUserRecommendationService : ICrudService<UserRecommendation>
{
}