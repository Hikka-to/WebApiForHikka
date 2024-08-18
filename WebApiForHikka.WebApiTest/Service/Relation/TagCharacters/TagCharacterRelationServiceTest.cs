using WebApiForHikka.Application.Relation.TagAnimes;
using WebApiForHikka.Application.Relation.TagCharacters;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.TagCharacters;

public class TagCharacterRelationServiceTest: SharedRelationServiceTest<
    TagCharacter,
    TagCharacterRelationService,
    Tag,
    Character
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

    protected override TagCharacterRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new TagCharacterRelationService(new TagCharacterRelationRepository(hikkaDbContext));
    }
}