using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class UserWatchHistoryRepositoryTest : SharedRelationRepositoryTest<
    UserWatchHistory, User, Episode,
    UserWatchHistoryRepository
>
{
    protected override UserWatchHistory GetSample()
    {
        return GetUserWatchHistoryModels.GetSample();
    }

    protected override UserWatchHistory GetSampleForUpdate()
    {
        return GetUserWatchHistoryModels.GetSampleForUpdate();
    }

    protected override UserWatchHistoryRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new UserWatchHistoryRepository(hikkaDbContext);
    }
}