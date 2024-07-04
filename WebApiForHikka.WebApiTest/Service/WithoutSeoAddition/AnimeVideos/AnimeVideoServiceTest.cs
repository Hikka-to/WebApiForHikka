using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeVideos;

public class AnimeVideoServiceTest : SharedServiceTest<AnimeVideo, AnimeVideoService>
{
    protected override AnimeVideo GetSample() => new()
    {
        AnimeVideoKind = new AnimeVideoKindServiceTest().Sample,
        Name = "Name1",
        Url = "Url1",
        ImageUrl = "ImageUrl1",
        EmbedUrl = "EmbedUrl1",
    };

    protected override AnimeVideo GetSampleForUpdate() => new()
    {
        AnimeVideoKind = new AnimeVideoKindServiceTest().SampleForUpdate,
        Name = "Name2",
        Url = "Url2",
        ImageUrl = "ImageUrl2",
        EmbedUrl = "EmbedUrl2",
    };

    protected override AnimeVideoService GetService(HikkaDbContext hikkaDbContext) => new(new AnimeVideoRepository(hikkaDbContext));
}
