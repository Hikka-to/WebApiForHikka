using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Characters;

public class CharacterRepositoryTest : SharedRepositoryTestWithSeoAddition<Character, CharacterRepository>
{
    public Character  Character => GetSample();

    public Character CharacterForUpdate => GetSampleForUpdate();

    protected override CharacterRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CharacterRepository(hikkaDbContext);
    }


    protected override Character GetSample()
    {
        return GetCharacterModels.GetSample();
    }

    protected override Character GetSampleForUpdate()
    {
        return GetCharacterModels.GetSampleForUpdate();
    }
} 
