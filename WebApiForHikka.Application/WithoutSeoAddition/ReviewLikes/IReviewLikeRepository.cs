using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.ReviewLikes;

public interface IReviewLikeRepository : ICrudRepository<ReviewLike>
{
}