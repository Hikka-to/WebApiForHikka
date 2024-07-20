using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.ExternalLinks;

public class ExternalLinkRepositoryTest : SharedRepositoryTest<ExternalLink, ExternalLinkRepository>
{
    protected override ExternalLink GetSample()
    {
        return GetExternalLinkModels.GetSample();
    }

    protected override ExternalLink GetSampleForUpdate()
    {
        return GetExternalLinkModels.GetSampleForUpdate();
    }

    protected override ExternalLinkRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new ExternalLinkRepository(hikkaDbContext);
    }
}