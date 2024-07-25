
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.Relation;

namespace WebApiForHikka.Application.Relation.UserWatchHistories;

public class UserWatchHistoryService(IUserWatchHistoryRepository relationRepository)
    : RelationCrudService<UserWatchHistory, User, Episode, IUserWatchHistoryRepository>(relationRepository), IUserWatchHistoryService;
