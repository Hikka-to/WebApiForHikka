using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Sources;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Sources;

public class SourceRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Source,
    SourceRepository
    >
{
    protected override SourceRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Source GetSample() => GetSourceModels.GetSample();
    protected override Source GetSampleForUpdate() => GetSourceModels.GetSampleForUpdate();

}