using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.ReviewLikes;

public class ReviewLikeService(IReviewLikeRepository repository)
    : CrudService< ReviewLike, IReviewLikeRepository>(repository), IReviewLikeService
{
    
}