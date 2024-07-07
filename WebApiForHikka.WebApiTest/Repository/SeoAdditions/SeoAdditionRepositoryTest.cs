using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAdditions;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.SeoAdditions;
public class SeoAdditionRepositoryTest : SharedRepositoryTest<SeoAddition, SeoAdditionRepository>
{
    protected override SeoAdditionRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override SeoAddition GetSample() => GetSeoAdditionModels.GetSample();
    protected override SeoAddition GetSampleForUpdate() => GetSeoAdditionModels.GetSampleForUpdate();

}