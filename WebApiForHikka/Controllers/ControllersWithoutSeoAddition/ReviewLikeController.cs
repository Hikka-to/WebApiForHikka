using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.ReviewLikes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class ReviewLikeController(
    ReviewLikeService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
)
    : CrudController
        <GetReviewLikeDto, UpdateReviewLikeDto, CreateReviewLikeDto, ReviewLikeService, ReviewLike>(crudService, mapper,
            httpContextAccessor);
