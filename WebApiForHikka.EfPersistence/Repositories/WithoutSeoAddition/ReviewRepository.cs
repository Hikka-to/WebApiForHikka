using WebApiForHikka.Application.WithoutSeoAddition.Reviews;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class ReviewRepository(HikkaDbContext dbContext)
    : CrudRepository<Review>(dbContext), IReviewRepository
{
    
}