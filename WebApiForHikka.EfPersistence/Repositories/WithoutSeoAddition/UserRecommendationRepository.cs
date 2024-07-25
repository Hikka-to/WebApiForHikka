using WebApiForHikka.Application.WithoutSeoAddition.UserRecomendations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class UserRecommendationRepository(HikkaDbContext dbContext)
    : CrudRepository<UserRecommendation>(dbContext), IUserRecommendationRepository
{
    
}