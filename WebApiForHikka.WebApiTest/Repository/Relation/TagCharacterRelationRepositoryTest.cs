using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class TagCharacterRelationRepositoryTest : SharedRelationRepositoryTest<
    TagCharacter, Tag, Character,
    TagCharacterRelationRepository
>
{
    protected override TagCharacter GetSample()
    {
        return GetTagCharacterModels.GetSample();
    }

    protected override TagCharacter GetSampleForUpdate()
    {
        return GetTagCharacterModels.GetSampleForUpdate();
    }

    protected override TagCharacterRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new TagCharacterRelationRepository(hikkaDbContext);
    }
}