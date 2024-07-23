using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Sources;

public class SourceRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Source,
    SourceRepository
>
{
    protected override SourceRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new SourceRepository(hikkaDbContext);
    }

    protected override Source GetSample()
    {
        return GetSourceModels.GetSample();
    }

    protected override Source GetSampleForUpdate()
    {
        return GetSourceModels.GetSampleForUpdate();
    }
}