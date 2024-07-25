using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.Reviews;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Reviews;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class ReviewController(
    ReviewService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
)
    : CrudController
        <GetReviewDto, UpdateReviewDto, CreateReviewDto, ReviewService, Review>(crudService, mapper,
            httpContextAccessor);
