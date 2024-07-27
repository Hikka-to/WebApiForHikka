using AutoMapper;
using WebApiForHikka.Application.Relation.UserWatchHistories;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.WatchUserHistories;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.Relation;

public class UserWatchHistoryController(
    IUserWatchHistoryService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetUserWatchHistoryDto,
        UpdateUserWatchHistoryDto,
        CreateUserWatchHistoryDto,
        IUserWatchHistoryService,
        UserWatchHistory
    >(crudRelationService, mapper, httpContextAccessor);