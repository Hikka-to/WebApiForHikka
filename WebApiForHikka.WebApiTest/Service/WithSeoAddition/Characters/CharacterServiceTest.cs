using Moq;
using WebApiForHikka.Application.WithSeoAddition.Characters;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Characters;

public class CharacterServiceTest: SharedServiceTestWithSeoAddition<Character, CharacterService>
{
    public Character Character => GetSample();

    public Character CharacterForUpdate => GetSampleForUpdate();

    protected override Character GetSample()
    {
        return GetCharacterModels.GetSample();
    }

    protected override Character GetSampleForUpdate()
    {
        return GetCharacterModels.GetSampleForUpdate();
    }

    protected override CharacterService GetService(HikkaDbContext hikkaDbContext)
    {
        var fileHelperMock = new Mock<IFileHelper>();

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        CharacterRepository characterRepository = new(hikkaDbContext);



        return new CharacterService(characterRepository, fileHelperMock.Object);
    }
}