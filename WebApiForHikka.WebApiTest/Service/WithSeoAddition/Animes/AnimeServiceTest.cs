using Moq;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;
using WebApiForHikka.WebApi.Helper.FileHelper;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Animes;

public class AnimeServiceTest : SharedServiceTestWithSeoAddition<Anime, AnimeService>
{
    public Anime Anime => GetSample();

    public Anime AnimeForUpdate => GetSampleForUpdate();

    protected override Anime GetSample()
    {
        return GetAnimeModels.GetSample();
    }

    protected override Anime GetSampleForUpdate()
    {
        return GetAnimeModels.GetSampleForUpdate();
    }

    protected override AnimeService GetService(HikkaDbContext hikkaDbContext)
    {

        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        AnimeRepository animeRepository = new(hikkaDbContext);
        AnimeBackdropRepository animebackdropRepository = new(hikkaDbContext);

        AnimeBackdropService animeBackdropService = new(animebackdropRepository, fileHelperMock.Object);
        

        return new AnimeService(animeRepository, animeBackdropService, fileHelperMock.Object);
    }
}