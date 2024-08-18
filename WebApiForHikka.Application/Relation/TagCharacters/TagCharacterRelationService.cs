using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.TagCharacters;

public class TagCharacterRelationService : RelationCrudService<TagCharacter, Tag, Character, ITagCharacterRelationRepository>,
ITagCharacterRelationService
{
    public TagCharacterRelationService(ITagCharacterRelationRepository repository) : base(repository)
    {
    }
}
