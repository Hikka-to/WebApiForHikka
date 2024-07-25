using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class UserAnimeListRelationRepositoryTest : SharedRelationRepositoryTest<
    UserAnimeList, User, Anime,
    UserAnimeListRelationRepository
>
{
    protected override UserAnimeList GetSample()
    {
        return GetUserAnimeListModels.GetSample();
    }

    protected override UserAnimeList GetSampleForUpdate()
    {
        return GetUserAnimeListModels.GetSampleForUpdate();
    }

    protected override UserAnimeListRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new UserAnimeListRelationRepository(hikkaDbContext);
    }
}