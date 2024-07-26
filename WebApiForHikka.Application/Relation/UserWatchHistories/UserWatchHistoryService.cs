using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.UserWatchHistories;

public class UserWatchHistoryService(IUserWatchHistoryRepository relationRepository)
    : RelationCrudService<UserWatchHistory, User, Episode, IUserWatchHistoryRepository>(relationRepository),
        IUserWatchHistoryService;