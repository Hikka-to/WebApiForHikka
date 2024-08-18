using WebApiForHikka.Application.Relation.AnimeCharacters;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.AnimeCharacters;

public class AnimeCharacterRelationServiceTest : SharedRelationServiceTest<
    AnimeCharacter,
    AnimeCharacterRelationService,
    Anime,
    Character
>
{
    protected override AnimeCharacter GetSample()
    {
        return GetAnimeCharacterModels.GetSample();
    }

    protected override AnimeCharacter GetSampleForUpdate()
    {
        return GetAnimeCharacterModels.GetSampleForUpdate();
    }

    protected override AnimeCharacterRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new AnimeCharacterRelationService(new AnimeCharacterRelationRepository(hikkaDbContext));
    }
}