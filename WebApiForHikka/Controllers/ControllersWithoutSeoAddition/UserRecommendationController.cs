using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.UserRecomendations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserRecomendations;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class UserRecommendationController (
    UserRecommendationService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
)
    : CrudController
        <GetUserRecommendationDto, UpdateUserRecommendationDto, CreateUserRecommendationDto, UserRecommendationService, UserRecommendation>(crudService, mapper,
            httpContextAccessor);