using WebApiForHikka.Application.Relation.UserWatchHistories;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class UserWatchHistoryRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<UserWatchHistory, User, Episode>(dbContext), IUserWatchHistoryRepository;