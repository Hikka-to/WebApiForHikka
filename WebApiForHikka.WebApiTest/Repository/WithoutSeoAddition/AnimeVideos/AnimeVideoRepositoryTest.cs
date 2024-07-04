using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeVideos;

public class AnimeVideoRepositoryTest : SharedRepositoryTest<AnimeVideo, AnimeVideoRepository>
{
    protected override AnimeVideo GetSample() => new()
    {
        AnimeVideoKind = new AnimeVideoKindRepositoryTest().Sample,
        Name = "Name1",
        Url = "Url1",
        ImageUrl = "ImageUrl1",
        EmbedUrl = "EmbedUrl1",
    };

    protected override AnimeVideo GetSampleForUpdate() => new()
    {
        AnimeVideoKind = new AnimeVideoKindRepositoryTest().SampleForUpdate,
        Name = "Name2",
        Url = "Url2",
        ImageUrl = "ImageUrl2",
        EmbedUrl = "EmbedUrl2",
    };

    protected override AnimeVideoRepository GetRepository(HikkaDbContext hikkaDbContext) => new(hikkaDbContext);
}
