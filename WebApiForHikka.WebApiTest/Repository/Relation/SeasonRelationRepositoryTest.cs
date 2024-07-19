using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class SeasonRelationRepositoryTest : SharedRelationRepositoryTest<
    Season, Anime, AnimeGroup,
    SeasonRelationRepository
>
{
    protected override Season GetSample()
    {
        return GetSeasonModels.GetSample();
    }

    protected override Season GetSampleForUpdate()
    {
        return GetSeasonModels.GetSampleForUpdate();
    }

    protected override SeasonRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new SeasonRelationRepository(hikkaDbContext);
    }
}