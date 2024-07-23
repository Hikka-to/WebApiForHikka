using Moq;
using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;
using WebApiForHikka.WebApi.Helper.FileHelper;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Episodes;

public class EpisodeServiceTest : SharedServiceTestWithSeoAddition<Episode, EpisodeService>
{
    protected override Episode GetSample()
    {
        return GetEpisodeModels.GetSample();
    }

    protected override Episode GetSampleForUpdate()
    {
        return GetEpisodeModels.GetSampleForUpdate();
    }

    protected override EpisodeService GetService(HikkaDbContext hikkaDbContext)
    {
        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        return new EpisodeService(new EpisodeRepository(hikkaDbContext), new EpisodeImageService(new EpisodeImageRepository(hikkaDbContext) ,fileHelperMock.Object));
    }
}