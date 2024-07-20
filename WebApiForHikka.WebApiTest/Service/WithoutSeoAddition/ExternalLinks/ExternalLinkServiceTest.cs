using WebApiForHikka.Application.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.ExternalLinks;

public class ExternalLinkServiceTest : SharedServiceTest<ExternalLink, ExternalLinkService>
{
    protected override ExternalLink GetSample()
    {
        return GetExternalLinkModels.GetSample();
    }

    protected override ExternalLink GetSampleForUpdate()
    {
        return GetExternalLinkModels.GetSampleForUpdate();
    }

    protected override ExternalLinkService GetService(HikkaDbContext hikkaDbContext)
    {
        return new ExternalLinkService(new ExternalLinkRepository(hikkaDbContext));
    }
}