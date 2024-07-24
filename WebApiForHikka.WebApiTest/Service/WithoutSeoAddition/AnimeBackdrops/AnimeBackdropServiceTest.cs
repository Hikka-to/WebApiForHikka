using Moq;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropServiceTest : SharedServiceTest<AnimeBackdrop, AnimeBackdropService>
{
    protected override AnimeBackdrop GetSample()
    {
        return GetAnimeBackdropModels.GetSample();
    }

    protected override AnimeBackdrop GetSampleForUpdate()
    {
        return GetAnimeBackdropModels.GetSampleForUpdate();
    }

    protected override AnimeBackdropService GetService(HikkaDbContext hikkaDbContext)
    {
        var fileHelperMock = new Mock<IFileHelper>();

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        return new AnimeBackdropService(new AnimeBackdropRepository(hikkaDbContext), fileHelperMock.Object);
    }
}