using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.Relation;

namespace WebApiForHikka.Application.Relation.UserWatchHistories;

public interface IUserWatchHistoryService : IRelationCrudService<UserWatchHistory, User, Episode>;
