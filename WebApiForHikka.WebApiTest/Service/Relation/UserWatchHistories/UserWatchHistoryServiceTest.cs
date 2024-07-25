using WebApiForHikka.Application.Relation.UserWatchHistories;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;
using WebApiForHikka.WebApi.Controllers.Relation;

namespace WebApiForHikka.Test.Service.Relation.UserWatchHistories;

public class UserWatchHistoryServiceTest : SharedRelationServiceTest<UserWatchHistory, UserWatchHistoryService, User, Episode>
{
    protected override UserWatchHistory GetSample()
    {
        return GetUserWatchHistoryModels.GetSample();
    }

    protected override UserWatchHistory GetSampleForUpdate()
    {
        return GetUserWatchHistoryModels.GetSampleForUpdate();
    }

    protected override UserWatchHistoryService GetService(HikkaDbContext hikkaDbContext)
    {
        return new UserWatchHistoryService(new UserWatchHistoryRepository(hikkaDbContext));
    }
}