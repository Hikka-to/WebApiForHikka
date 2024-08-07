using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.UserWatchHistories;

public interface IUserWatchHistoryService : IRelationCrudService<UserWatchHistory, User, Episode>;