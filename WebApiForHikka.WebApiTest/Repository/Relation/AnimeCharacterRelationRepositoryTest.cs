using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class AnimeCharacterRelationRepositoryTest : SharedRelationRepositoryTest<
    AnimeCharacter,  Anime, Character,
    AnimeCharacterRelationRepository
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

    protected override AnimeCharacterRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new AnimeCharacterRelationRepository(hikkaDbContext);
    }
}
