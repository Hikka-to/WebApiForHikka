using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.Seasons;

public class SeasonRelationServiceTest : SharedRelationServiceTest<Season, SeasonRelationService, Anime, AnimeGroup>
{
    protected override Season GetSample()
    {
        return GetSeasonModels.GetSample();
    }

    protected override Season GetSampleForUpdate()
    {
        return GetSeasonModels.GetSampleForUpdate();
    }

    protected override SeasonRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new SeasonRelationService(new SeasonRelationRepository(hikkaDbContext));
    }
}