using WebApiForHikka.Application.WithoutSeoAddition.ReviewLikes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class ReviewLikeRepository(HikkaDbContext dbContext)
    : CrudRepository<ReviewLike>(dbContext), IReviewLikeRepository;
