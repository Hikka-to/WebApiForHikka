using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Application.Relation.UserAnimeLists;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.UserAnimeLists;

public class UserAnimeListServiceTest : SharedRelationServiceTest<UserAnimeList,UserAnimeListRelationService, User, Anime>
{
    protected override UserAnimeList GetSample()
    {
        return GetUserAnimeListModels.GetSample();
    }

    protected override UserAnimeList GetSampleForUpdate()
    {
        return GetUserAnimeListModels.GetSampleForUpdate();
    }

    protected override UserAnimeListRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new UserAnimeListRelationService(new UserAnimeListRelationRepository(hikkaDbContext));
    }
}