using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.Reviews;

public class ReviewService (IReviewRepository repository)
    : CrudService<Review, IReviewRepository>(repository), IReviewService
{ 
    
}